using UnityEngine;

namespace ToolsPropertiesAndAttributes.Attributes
{
	public enum ConditionalWeaponType { Melee, Ranged, Magic}
	
	[CreateAssetMenu(menuName = "Weapons/Conditional Weapon")]
	public class ConditionalWeapon : ScriptableObject
	{
		public ConditionalWeaponType weaponType;
		public int weaponLevel = 1;
		public bool isThrowable = false;

		[ConditionalField("weaponType", ConditionalWeaponType.Ranged)]
		public float maximumRange;
		
		[ConditionalField("weaponType", ConditionalWeaponType.Ranged)]
		public float projectileSpeed;
		
		[ConditionalField("weaponType", ConditionalWeaponType.Melee)]
		public float swingArc;

		[ConditionalField("weaponLevel", 5)]
		public float extraDamage;
		
		[ConditionalField("isThrowable", true)]
		public float throwRange;
	}
}