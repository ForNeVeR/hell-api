using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Hell.LastCircle.CList;

namespace Hell
{
	/// <summary>
	/// Test managed Miranda plugin.
	/// </summary>
	// Every plugin main class must be marked with MirandaPlugin attribute and
	// be derived from abstract Hell.Plugin class.
	[MirandaPlugin]
	public class TestPlugin : Plugin
	{
		/// <summary>
		/// ALWAYS remember to save delegates to your methods that can be
		/// called from Miranda. If you forget to do that, delegate will be
		/// garbage collected and method call will fail.
		/// </summary>
		private MirandaService menuCommand;

		/// <summary>
		/// Plugin object constructor.
		/// </summary>
		public TestPlugin()
		{
			menuCommand = PluginMenuCommand;
		}

		/// <summary>
		/// Load method will be called on plugin load.
		/// </summary>
		protected override void Load()
		{
			this.CreateServiceFunction("TestPlug/MenuCommand",
				menuCommand);

			var mi = new CListMenuItem();
			mi.position = -0x7FFFFFFF;
			mi.flags = 0;
			// TODO: Load icon:
			// mi.hIcon = LoadSkinnedIcon(SKINICON_OTHER_MIRANDA);
			mi.name = "&Test Plugin...";
			mi.service = "TestPlug/MenuCommand";

			// You can use raw IntPtr instead of AutoPtr here, but then do not
			// forget to call Marshal.FreeHGlobal to prevent memory leaks.
			using (var pClistMenuItem = new AutoPtr(
				Marshal.AllocHGlobal(Marshal.SizeOf(typeof (CListMenuItem)))))
			{
				Marshal.StructureToPtr(mi, pClistMenuItem, false);
				this.CallService("CList/AddMainMenuItem", IntPtr.Zero,
					pClistMenuItem);
			}
		}

		/// <summary>
		/// Unload method will be called on plugin unloading (for example, on
		/// Miranda exit).
		/// </summary>
		public override void Unload()
		{
			// TODO: Delete menu item, unhook event.
		}

		/// <summary>
		/// This method woll be called when user selects "Test Plugin..." menu
		/// item.
		/// </summary>
		private IntPtr PluginMenuCommand(IntPtr wParam, IntPtr lParam)
		{
			MessageBox.Show("Hello world from TestPlugin!");
			return IntPtr.Zero;
		}
	}
}