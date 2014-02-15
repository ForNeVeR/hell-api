using System;
using System.Collections.Generic;
using System.Linq;
using Hell.FirstCircle;

namespace Hell.Gateman
{
	/// <summary>
	/// Plugin for logging users online time.
	/// </summary>
	[MirandaPluginAttribute]
	public class Gateman : Plugin
	{
		private IList<Contact> _contacts;

		/// <summary>
		/// A data storage for this plugin instance.
		/// </summary>
		private Storage _storage;

		/// <summary>
		/// A method called for plugin loading.
		/// </summary>
		protected override void Load()
		{
			// TODO: Handle contact creation and deletion.
			_contacts = Contact.Enumerate().ToList();
			foreach (Contact contact in _contacts)
			{
				contact.StatusChanged += contact_StatusChangedEvent;
			}

			// TODO: Read file name from settings.
			_storage = new Storage("Gateman.sqlite");
		}

		/// <summary>
		/// A method called when plugin engine desided to stop this plugin.
		/// </summary>
		public override void Unload()
		{
			_storage.Dispose();
			foreach (Contact contact in _contacts)
			{
				contact.Dispose();
			}
		}

		/// <summary>
		/// An event handler called when some contact status changed.
		/// </summary>
		private void contact_StatusChangedEvent(Contact sender, Contact.Status newStatus)
		{
			_storage.StoreStatus(sender, newStatus, DateTime.Now);
		}
	}
}