using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
	[CreateAssetMenu(fileName = "New WeaponConfig", menuName = "Weapons/Config")]
	public class WeaponConfig : ScriptableObject
	{
		public WeaponType type;
		public string name;
		public float explosionRadius;
		public int damage;
		public int ammoCount;
		public float fireRate;
		public float reloadTime;
		public float ExplosionRadius;
		public bool StartExplosive;
		
		public List<IWeaponAttachment> Attachments = new();
		// plus any other meaningful field..
	}
}