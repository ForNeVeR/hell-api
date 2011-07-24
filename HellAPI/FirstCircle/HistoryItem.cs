using System;
using System.Runtime.InteropServices;
using Hell.LastCircle.Database;
using Hell.LastCircle.System;

namespace Hell.FirstCircle
{
    /// <summary>
    /// Event from Miranda history.
    /// </summary>
    public class HistoryItem
    {
        /// <summary>
        /// Event type.
        /// </summary>
        public enum HistoryItemType : ushort
        {
            /// <summary>
            /// Regular message.
            /// </summary>
            Message = DBEventInfo.EVENTTYPE_MESSAGE,

            /// <summary>
            /// URL.
            /// </summary>
            Url = DBEventInfo.EVENTTYPE_URL,

            /// <summary>
            /// Contacts data.
            /// </summary>
            Contacts = DBEventInfo.EVENTTYPE_CONTACTS,

            /// <summary>
            /// "Contact added you" event.
            /// </summary>
            Added = DBEventInfo.EVENTTYPE_ADDED,

            /// <summary>
            /// Authentication request.
            /// </summary>
            AuthRequest = DBEventInfo.EVENTTYPE_AUTHREQUEST,

            /// <summary>
            /// File.
            /// </summary>
            File = DBEventInfo.EVENTTYPE_FILE
        }

        #region Data fields and properties

        /// <summary>
        /// Contact to whom event is assigned.
        /// </summary>
        public Contact Contact { get; set; }

        /// <summary>
        /// Type of this event.
        /// </summary>
        public HistoryItemType Type { get; private set; }

        /// <summary>
        /// Text of message (if this event is message).
        /// </summary>
        public string MessageText { get; set; }

        /// <summary>
        /// Date and time of an event.
        /// </summary>
        public DateTime DateTime { get; set; }

        #endregion

        #region Equality stuff

        /// <summary>
        /// Compares this object with other. Overrides Object method.
        /// </summary>
        /// <param name="obj">
        /// Object to be compared to.
        /// </param>
        /// <returns>
        /// true if objects are equal.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(HistoryItem)) return false;
            return Equals((HistoryItem)obj);
        }

        /// <summary>
        /// Compares this history item with other.
        /// </summary>
        /// <param name="other">
        /// History item to be compared to
        /// </param>
        /// <returns>
        /// true if objects are equal.
        /// </returns>
        public bool Equals(HistoryItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Contact, Contact) &&
                   Equals(other.Type, Type) &&
                   Equals(other.MessageText, MessageText) &&
                   other.DateTime.Equals(DateTime);
        }

        /// <summary>
        /// Calculates hash code for object.
        /// </summary>
        /// <returns>
        /// Value of hash code.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Contact != null ? Contact.GetHashCode() : 0);
                result = (result * 397) ^ Type.GetHashCode();
                result = (result * 397) ^
                         (MessageText != null ? MessageText.GetHashCode() : 0);
                result = (result * 397) ^ DateTime.GetHashCode();
                return result;
            }
        }

        /// <summary>
        /// An operator tests for equality of two objects.
        /// </summary>
        public static bool operator ==(HistoryItem left, HistoryItem right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// An operator tests for inequality of two objects.
        /// </summary>
        public static bool operator !=(HistoryItem left, HistoryItem right)
        {
            return !Equals(left, right);
        }

        #endregion

        /// <summary>
        /// Creates new in-memory history item.
        /// </summary>
        /// <param name="type">
        /// Type of created item.
        /// </param>
        public HistoryItem(HistoryItemType type)
        {
            Type = type;
        }

        /// <summary>
        /// Creates instance of history item and loads its content from
        /// database.
        /// </summary>
        /// <param name="pluginLink">
        /// Object containing Miranda services.
        /// </param>
        /// <param name="hEvent">
        /// Miranda event handle.
        /// </param>
        internal static HistoryItem Load(PluginLink pluginLink, Contact contact,
            IntPtr hEvent)
        {
            if (hEvent == IntPtr.Zero)
                throw new ArgumentException("hEvent cannot be zero.");

            // Miranda interface for freeing strings:
            var mmi = MMInterface.GetMMI(pluginLink);

            using (var pDbEventInfo = new AutoPtr(Marshal.AllocHGlobal(
                Marshal.SizeOf(typeof(DBEventInfo)))))
            {
                var result = pluginLink.CallService("DB/Event/Get", hEvent,
                                                    pDbEventInfo);
                if (result != IntPtr.Zero)
                    throw new DatabaseException();

                var eventInfo =
                    (DBEventInfo)
                    Marshal.PtrToStructure(pDbEventInfo, typeof (DBEventInfo));

                var historyItem =
                    new HistoryItem((HistoryItemType) eventInfo.eventType);
                if (historyItem.Type == HistoryItemType.Message)
                {
                    // Get message text:
                    using (var pDbEventGetText = new AutoPtr(
                        Marshal.AllocHGlobal(Marshal.SizeOf(
                        typeof(DBEventGetText)))))
                    {
                        var getText = new DBEventGetText();
                        getText.dbei = pDbEventInfo;
                        getText.datatype = Utils.DBVT_WCHAR;

                        Marshal.StructureToPtr(getText, pDbEventGetText, false);

                        IntPtr pString = pluginLink.CallService(
                            "DB/Event/GetText", IntPtr.Zero, pDbEventGetText);
                        mmi.mmi_free(pString);

                        var message = Marshal.PtrToStringUni(pString);
                        historyItem.MessageText = message;
                    }
                }

                historyItem.Contact = contact;
                historyItem.DateTime = new DateTime(1970, 1, 1)
                    .AddSeconds(eventInfo.timestamp);

                return historyItem;
            }
        }

        // TODO: Needs methods for saving to database. Also XML (de)serializing
        // for transporting.
    }
}
