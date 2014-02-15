namespace Hell.LastCircle.WinAPI
{
	/// <summary>
	/// Class containing various WinAPI constants.
	/// </summary>
	public static class Constants
	{
		public const int WM_INITDIALOG = 0x0110;
		public const int WM_NOTIFY = 0x004E;

		public const int WS_CHILD = 0x40000000;
		public const int WS_VISIBLE = 0x10000000;

		private static readonly uint PSN_FIRST;
		public static readonly uint PSN_APPLY;
		public static readonly uint PSN_RESET;

		static Constants()
		{
			unchecked
			{
				PSN_FIRST = (uint)-200;
				PSN_APPLY = PSN_FIRST - 2;
				PSN_RESET = PSN_FIRST - 3;
			}
		}

		private const uint WM_USER = 0x400;
		public const uint PSM_CHANGED = WM_USER + 104;
	}
}