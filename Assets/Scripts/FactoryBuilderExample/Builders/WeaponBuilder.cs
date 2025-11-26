namespace DefaultNamespace.Builders
{
	// Example of a weapon builder class, can make other like a BulletBuilder for various kinds of bullet types etc
	public class WeaponBuilder
	{
		Weapon weapon = new Weapon();

		public WeaponBuilder WithName(string name)
		{
			weapon.Name = name;

			return this;
		}
		
		public WeaponBuilder WithDamage(int damage)
		{
			weapon.Damage = damage;

			return this;
		}
		
		public WeaponBuilder WithSpeed(float speed)
		{
			weapon.Speed = speed;

			return this;
		}
		
		public WeaponBuilder AsRanged(bool isRanged)
		{
			weapon.IsRanged = isRanged;

			return this;
		}
		
		public WeaponBuilder WithMaterial(string material)
		{
			weapon.Material = material;

			return this;
		}
		
		public WeaponBuilder WithCritChance(float critChance)
		{
			weapon.CritChance = critChance;

			return this;
		}
		
		public WeaponBuilder WithFireRate(float fireRate)
		{
			weapon.FireRate = fireRate;

			return this;
		}
		
		public WeaponBuilder WithAmmoCount(int ammoCount)
		{
			weapon.AmmoCount = ammoCount;

			return this;
		}
		
		public WeaponBuilder WithReloadTime(float reloadTime)
		{
			weapon.ReloadTime = reloadTime;

			return this;
		}		
		
		public WeaponBuilder MakeExplosive(float explosionRadius)
		{
			weapon.ExplosionRadius = explosionRadius;

			return this;
		}
		
		public WeaponBuilder AddAttachment(IWeaponAttachment att)
		{
			weapon.Attachments.Add(att);

			return this;
		}

		public Weapon Build()
		{
			// Apply all attachments before returning the final weapon
			foreach (var attachment in weapon.Attachments)
				attachment.Apply(weapon);
			
			return weapon;
		}
	}
}