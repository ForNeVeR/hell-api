using System;

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