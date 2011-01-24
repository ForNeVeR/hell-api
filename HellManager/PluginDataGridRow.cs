using System;

namespace Hell
{
    class PluginDataGridRow
    {
        public enum State
        {
            Loaded,
            AboutToLoad,
            Unloaded,
            AboutToUnload
        }

        private Type type;
        public State LoadState { get; set; }

        public PluginDataGridRow(Type type, State state)
        {
            this.type = type;
            this.LoadState = state;
        }
        
        public string TypeName
        {
            get
            {
                return type.FullName;
            }
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
