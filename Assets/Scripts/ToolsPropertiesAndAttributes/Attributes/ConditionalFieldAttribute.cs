using System;
using UnityEngine;

namespace ToolsPropertiesAndAttributes.Attributes
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class ConditionalFieldAttribute : PropertyAttribute
	{
		public string ConditionalFieldName;
		public object ComparisonValue;

		public ConditionalFieldAttribute(string conditionalFieldName)
		{
			ConditionalFieldName = conditionalFieldName;
			ComparisonValue = null;
		}

		public ConditionalFieldAttribute(string conditionalFieldName, object comparisonValue)
		{
			ConditionalFieldName = conditionalFieldName;
			ComparisonValue = comparisonValue;
		}
	}
}