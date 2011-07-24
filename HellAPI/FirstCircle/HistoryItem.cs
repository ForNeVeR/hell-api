﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public enum HistoryItemType
        {
            /// <summary>
            /// Regular message.
            /// </summary>
            Message,

            /// <summary>
            /// URL.
            /// </summary>
            Url,

            /// <summary>
            /// Contacts data.
            /// </summary>
            Contacts,

            /// <summary>
            /// "Contact added you" event.
            /// </summary>
            Added,

            /// <summary>
            /// Authentication request.
            /// </summary>
            AuthRequest,

            /// <summary>
            /// File.
            /// </summary>
            File
        }

        #region Data fields and properties

        /// <summary>
        /// Contact to whom event is assigned.
        /// </summary>
        public Contact Contact { get; set; }

        /// <summary>
        /// Module name of event.
        /// </summary>
        public string ModuleName { get; set; }

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
                   Equals(other.ModuleName, ModuleName) &&
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
                result = (result * 397) ^
                         (ModuleName != null ? ModuleName.GetHashCode() : 0);
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

        // TODO: Needs methods for saving / loading from / to database. Also
        // XML (de)serializing for transporting.
    }
}