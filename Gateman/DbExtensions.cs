using System.Data.Common;

namespace Hell.Gateman
{
	/// <summary>
	/// A class for database extension methods.
	/// </summary>
	internal static class DbExtensions
	{
		/// <summary>
		/// Adds parameter to database command.
		/// </summary>
		/// <param name="command">Database command object.</param>
		/// <param name="parameterName">Parameter name.</param>
		/// <param name="parameterValue">Parameter value to add.</param>
		public static void AddParameter(this DbCommand command, string parameterName,
			object parameterValue)
		{
			var parameter = command.CreateParameter();
			parameter.ParameterName = parameterName;
			parameter.Value = parameterValue;
			command.Parameters.Add(parameter);
		}
	}
}