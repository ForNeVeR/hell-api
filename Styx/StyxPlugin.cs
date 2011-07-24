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

using Hell;

namespace Styx
{
    /// <summary>
    /// Styx is plugin for history synchronizing between multiple Miranda IM
    /// instances.
    /// </summary>
    [MirandaPlugin]
    public class StyxPlugin : Plugin
    {
        /// <summary>
        /// This method will be called by adapter system when all preparations
        /// for plugin loading done (i.e. calling private Load method).
        /// </summary>
        protected override void Load()
        {
            
        }

        /// <summary>
        /// This method called on plugin unloading (for example, on Miranda
        /// exit).
        /// </summary>
        public override void Unload()
        {
            
        }
    }
}
