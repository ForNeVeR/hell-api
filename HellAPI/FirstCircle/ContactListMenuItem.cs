using System;
using System.Runtime.InteropServices;
using Hell.LastCircle.CList;

namespace Hell.FirstCircle
{
	/// <summary>
	/// Class for creating item in Miranda IM contact list menu.
	/// </summary>
	public class ContactListMenuItem : IDisposable
	{
		/// <summary>
		/// Delegate for miranda service.
		/// </summary>
		private readonly MirandaService _service;

		/// <summary>
		/// Action to be called on menu item click.
		/// </summary>
		private readonly Action _action;

		/// <summary>
		/// Creates a service named <paramref name="serviceName"/> and creates an item in Miranda IM contact list.
		/// </summary>
		/// <param name="serviceName">Unique name of service to be created.</param>
		/// <param name="menuItemText">Text of menu item.</param>
		/// <param name="action">Method to be called when user selects menu item.</param>
		public ContactListMenuItem(string serviceName, string menuItemText, Action action)
		{
			_action = action;
			_service = Service;
			Plugin.m_CreateServiceFunction(serviceName, _service);

			var cListMenuItem = new CListMenuItem
			{
				position = -0x7FFFFFFF,
				flags = 0,
				name = menuItemText,
				service = serviceName
			};

			using (var pCListMenuItem = new AutoPtr(Marshal.AllocHGlobal(Marshal.SizeOf(typeof (CListMenuItem)))))
			{
				Marshal.StructureToPtr(cListMenuItem, pCListMenuItem, false);
				Plugin.m_CallService("CList/AddMainMenuItem", IntPtr.Zero, pCListMenuItem);
			}
		}

		/// <summary>
		/// Disposes this object.
		/// </summary>
		public void Dispose()
		{
			// TODO: Delete item from main menu.
		}

		/// <summary>
		/// Miranda service method.
		/// </summary>
		/// <returns>Always returns a null pointer.</returns>
		private IntPtr Service(IntPtr wParam, IntPtr lParam)
		{
			_action();
			return IntPtr.Zero;
		}
	}
}