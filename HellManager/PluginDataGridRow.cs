/*
 * Copyright (C) 2010-2011 by ForNeVeR
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
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
