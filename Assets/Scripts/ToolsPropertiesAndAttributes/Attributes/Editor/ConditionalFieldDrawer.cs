using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ToolsPropertiesAndAttributes.Attributes.Editor
{
	[CustomPropertyDrawer(typeof(ConditionalFieldAttribute))]
	public class ConditionalFieldDrawer : PropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (ShouldShow(property))
			{
				return EditorGUI.GetPropertyHeight(property, label, true);
			}

			return -2f;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if(ShouldShow(property))
				EditorGUI.PropertyField(position, property, label, true);
		}

		private bool ShouldShow(SerializedProperty property)
		{
			ConditionalFieldAttribute cond = (ConditionalFieldAttribute) attribute;
			string conditionField = cond.ConditionalFieldName;
			
			SerializedProperty conditionProperty = property.serializedObject.FindProperty(conditionField);

			if (conditionProperty == null) return true;

			if (conditionProperty.propertyType == SerializedPropertyType.Boolean)
			{
				bool condValue = conditionProperty.boolValue;

				if (cond.ComparisonValue == null)
				{
					return condValue;
				}
				
				return condValue.Equals(cond.ComparisonValue);
			}

			if (conditionProperty.propertyType == SerializedPropertyType.Enum)
			{
				int intValue = conditionProperty.intValue;

				if (cond.ComparisonValue == null) return true;
				
				return intValue == (int)cond.ComparisonValue;
			}
			
			object actualValue = GetActualValue(property, conditionField);
			
			if(actualValue == null) return true;

			if (cond.ComparisonValue == null) return true;
			
			return actualValue.Equals(cond.ComparisonValue);
		}

		private object GetActualValue(SerializedProperty property, string fieldName)
		{
			object target = property.serializedObject.targetObject;
			FieldInfo fieldInfo = target.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			
			if(fieldInfo == null) return null;
			return fieldInfo.GetValue(target);
		}
	}
}