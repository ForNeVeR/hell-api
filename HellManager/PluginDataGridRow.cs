using System;

namespace Hell
{
	internal class PluginDataGridRow
	{
		public enum State
		{
			Loaded,
			AboutToLoad,
			Unloaded,
			AboutToUnload
		}

		internal Type Type;
		internal State LoadState;

		public PluginDataGridRow(Type type, State state)
		{
			this.Type = type;
			this.LoadState = state;
		}

		public string TypeName
		{
			get { return Type.FullName; }
		}

		public string Status
		{
			get
			{
				switch (LoadState)
				{
					case State.Loaded:
						return "Loaded";
					case State.Unloaded:
						return "Unloaded";
					case State.AboutToLoad:
						return "About to load";
					case State.AboutToUnload:
						return "About to unload";
					default:
						return null;
				}
			}
		}
	}
}