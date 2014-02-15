using Hell;
using Hell.FirstCircle;

namespace Styx
{
	/// <summary>
	/// Styx is plugin for history synchronizing between multiple Miranda IM
	/// instances.
	/// </summary>
	[MirandaPlugin]
	public class StyxPlugin : Plugin
	{
		private ContactListMenuItem _menuItem;

		/// <summary>
		/// This method will be called by adapter system when all preparations
		/// for plugin loading done.
		/// </summary>
		protected override void Load()
		{
			_menuItem = new ContactListMenuItem("Hell.Styx/SyncHistoryCommand", "Synchronize history...",
				() =>
				{
					var window = new ControlWindow();
					window.Show();
				});
		}

		/// <summary>
		/// This method called on plugin unloading (for example, on Miranda
		/// exit).
		/// </summary>
		public override void Unload()
		{
			_menuItem.Dispose();
		}
	}
}