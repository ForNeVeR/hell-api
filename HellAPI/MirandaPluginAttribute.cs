using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hell
{
    /// <summary>
    /// Classes marked with this attribute will be loaded by Hell plugin
    /// controller.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false,
        AllowMultiple = false)]
    public sealed class MirandaPluginAttribute : Attribute
    {

    }
}
