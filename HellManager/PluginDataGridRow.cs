/*
 * Copyright 2010-2011 ForNeVeR.
 *
 * This file is part of Hell API.
 *
 * Hell API is free software: you can redistribute it and/or modify it under
 * the terms of the GNU Lesser General Public License as published by the Free
 * Software Foundation, either version 3 of the License, or (at your option)
 * any later version.
 *
 * Hell API is distributed in the hope that it will be useful, but WITHOUT ANY
 * WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 * FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
 * details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with Hell API. If not, see <http://www.gnu.org/licenses/>.
 */

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

        internal Type Type;
        internal State LoadState;

        public PluginDataGridRow(Type type, State state)
        {
            this.Type = type;
            this.LoadState = state;
        }
        
        public string TypeName
        {
            get
            {
                return Type.FullName;
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
