﻿using System;
using System.Runtime.InteropServices;
using Hell.LastCircle.Contacts;
using Hell.LastCircle.Database;
using System.Collections.Generic;

namespace Hell.FirstCircle
{
	/// <summary>
	/// Class representing Miranda contact.
	/// </summary>
	public class Contact : IDisposable
	{
		public enum Status
		{
			Offline = 40071,
			Online = 40072,
			Away = 40073,
			DoNotDisturb = 40074,
			NA = 40075,
			Occupied = 40076,
			FreeChat = 40077,
			Invisible = 40078,
			OnThePhone = 40079,
			OutToLunch = 40080
		}

		public delegate void StatusChangedEventHandler(Contact sender,
			Status newStatus);

		/// <summary>
		/// This event is raised on contact status changes.
		/// </summary>
		public event StatusChangedEventHandler StatusChanged;

		#region Static methods

		/// <summary>
		/// Returns full list of contacts stored in the database.
		/// </summary>
		public static IEnumerable<Contact> Enumerate()
		{
			var result = new List<Contact>();
			IntPtr hContact = Plugin.m_CallService(
				"DB/Contact/FindFirst",
				IntPtr.Zero,
				IntPtr.Zero);
			while (hContact != IntPtr.Zero)
			{
				result.Add(new Contact(hContact));
				hContact = Plugin.m_CallService(
					"DB/Contact/FindNext",
					hContact,
					IntPtr.Zero);
			}

			return result;
		}

		#endregion

		#region Data fields and properties

		internal IntPtr hContact;
		private MirandaHook contactSettingChangedHook;
		private IntPtr hContactSettingChangedHook;

		private bool disposed;

		/// <summary>
		/// Nickname of contact from database.
		/// </summary>
		public string Nickname
		{
			get { return GetContactField(ContactInfo.CNF_NICK); }
		}

		/// <summary>
		/// Internal protocol name of contact from database.
		/// </summary>
		public string Protocol
		{
			get
			{
				// Create ContactInfo object:
				var info = new ContactInfo();
				info.hContact = hContact;

				// Copy ContactInfo to unmanaged memory:
				using (var pContactInfo = new AutoPtr(
					Marshal.AllocHGlobal(Marshal.SizeOf(typeof (ContactInfo)))))
				{
					Marshal.StructureToPtr(info, pContactInfo, false);

					IntPtr result = Plugin.m_CallService(
						"Miranda/Contact/GetContactInfo",
						IntPtr.Zero,
						pContactInfo);

					info = (ContactInfo)Marshal.PtrToStructure(pContactInfo,
						typeof (ContactInfo));
				}

				return info.szProto;
			}
		}

		/// <summary>
		/// Unique user Id.
		/// </summary>
		public string Uid
		{
			get { return GetContactField(ContactInfo.CNF_UNIQUEID); }
		}

		#endregion

		#region Equality stuff

		public override bool Equals(object obj)
		{
			if (obj is Contact)
				return hContact == (obj as Contact).hContact;
			else
				return false;
		}

		public bool Equals(Contact other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.hContact.Equals(hContact); // &&
			//Equals(other.pluginLink, pluginLink);
		}

		/// <summary>
		/// Hash code is calculated uisng protocol name and user id because it
		/// may be shared with another Miranda instance.
		/// </summary>
		public override int GetHashCode()
		{
			unchecked
			{
				return (Protocol.GetHashCode() * 397) ^ Uid.GetHashCode();
			}
		}

		public static bool operator ==(Contact c1, Contact c2)
		{
			return c1.Equals(c2);
		}

		public static bool operator !=(Contact c1, Contact c2)
		{
			return !c1.Equals(c2);
		}

		#endregion

		/// <summary>
		/// Object constructor.
		/// </summary>
		/// <param name="hContact">
		/// Handle used for various Miranda contact manipulations.
		/// </param>
		public Contact(IntPtr hContact)
		{
			this.hContact = hContact;

			contactSettingChangedHook = ContactSettingChanged;
			hContactSettingChangedHook = Plugin.m_HookEvent(
				"DB/Contact/SettingChanged",
				contactSettingChangedHook);
		}

		/// <summary>
		/// Sends simple message to contact.
		/// </summary>
		/// <param name="message">
		/// Message string to send.
		/// </param>
		public void SendMessage(string message)
		{
			var eventInfo = new DBEventInfo();
			eventInfo.eventType = DBEventInfo.EVENTTYPE_MESSAGE;
			eventInfo.flags = DBEventInfo.DBEF_SENT;
			eventInfo.szModule = Protocol;
			eventInfo.timestamp = (int)(DateTime.Now.ToUniversalTime() -
			                            new DateTime(1970, 1, 1)).TotalSeconds;

			using (var pString =
				new AutoPtr(Marshal.StringToHGlobalAnsi(message)))
			using (var pDBEventInfo = new AutoPtr(
				Marshal.AllocHGlobal(Marshal.SizeOf(typeof (DBEventInfo)))))
			{
				eventInfo.cbBlob = (uint)message.Length + 1;
				eventInfo.pBlob = pString;

				Marshal.StructureToPtr(eventInfo, pDBEventInfo, false);

				Plugin.m_CallService("DB/Event/Add", hContact, pDBEventInfo);
				Plugin.m_CallContactService(hContact, "/SendMsg", IntPtr.Zero, pString);
			}
		}

		/// <summary>
		/// Method for enumeration of contact history items.
		/// </summary>
		/// <returns>
		/// Enumerator for this contact history.
		/// </returns>
		public IEnumerable<HistoryItem> GetHistory()
		{
			var hEvent = Plugin.m_CallService("DB/Event/FindFirst", hContact,
				IntPtr.Zero);
			while (hEvent != IntPtr.Zero)
			{
				var item = HistoryItem.Load(this, hEvent);
				yield return item;
				hEvent = Plugin.m_CallService("DB/Event/FindNext", hEvent,
					IntPtr.Zero);
			}
			yield break;
		}

		/// <summary>
		/// Gets contact info string from database.
		/// </summary>
		/// <param name="fieldFlag">
		/// Flag from ContactInfo.CNF_* list.
		/// </param>
		/// <returns>
		/// String from database interpreted as ANSI.
		/// </returns>
		private string GetContactField(byte fieldFlag)
		{
			// Create ContactInfo object:
			var info = new ContactInfo();
			info.hContact = hContact;
			info.dwFlag = fieldFlag;

			string fieldData = null;

			using (var pContactInfo = new AutoPtr(
				Marshal.AllocHGlobal(Marshal.SizeOf(typeof (ContactInfo)))))
			{
				// Copy ContactInfo to unmanaged memory:
				Marshal.StructureToPtr(info, pContactInfo, false);

				var result = Plugin.m_CallService(
					"Miranda/Contact/GetContactInfo",
					IntPtr.Zero, pContactInfo);

				if (result == IntPtr.Zero)
				{
					info = (ContactInfo)Marshal.PtrToStructure(pContactInfo,
						typeof (
							ContactInfo));
					fieldData = Marshal.PtrToStringAnsi(info.value.pszVal);
				}
			}

			return fieldData;
		}

		/// <summary>
		/// This method is called on contact status change.
		/// </summary>
		/// <param name="wParam">Handle of chagned contact.</param>
		/// <param name="lParam">
		/// Pointer to DBContactWriteSetting structure.
		/// </param>
		/// <returns>Always zero.</returns>
		private int ContactSettingChanged(IntPtr wParam, IntPtr lParam)
		{
			IntPtr changedHContact = wParam;
			if (changedHContact == hContact)
			{
				IntPtr pDBContactWriteSetting = lParam;
				var setting = new DBContactWriteSetting();
				Marshal.PtrToStructure(pDBContactWriteSetting, setting);

				// TODO: Check this setting name
				if (setting.szSetting == "Status" &&
				    setting.szModule == Protocol)
				{
					var newStatus = (Status)setting.value.Value.wVal;
					if (StatusChanged != null)
						StatusChanged(this, newStatus);
				}
			}
			return 0;
		}

		public void Dispose()
		{
			disposed = true;
			Plugin.m_UnhookEvent(hContactSettingChangedHook);
		}

		~Contact()
		{
			if (!disposed)
				Dispose();
		}
	}
}