using System.Runtime.InteropServices;

namespace Hell.LastCircle.Database
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	internal class DBContactWriteSetting
	{
		/// <summary>
		/// name of the module that wrote the setting to set
		/// </summary>
		public string szModule;

		/// <summary>
		/// name of the setting to set
		/// </summary>
		public string szSetting;

		/// <summary>
		/// variant containing the value to set
		/// </summary>
		public DBVariant value;
	}
}