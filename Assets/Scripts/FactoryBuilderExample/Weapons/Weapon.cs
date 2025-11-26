using System.Collections.Generic;

namespace DefaultNamespace
{
	public class Weapon
	{
		public string Name;
		public int Damage;
		public int AmmoCount;
		public float Speed;
		public bool IsRanged;
		public string Material;
		public float CritChance;
		public float FireRate;
		public float ReloadTime;
		public bool IsExplosive;
		public float ExplosionRadius;
		
		public List<IWeaponAttachment> Attachments = new();
		
		// + any other method or fields necessary
	}
}