using FactoryBuilderExample.Weapons;

namespace FactoryBuilderExample.ExampleAttachments
{
	public class ExplosiveRoundsAttachment : IWeaponAttachment
	{
		float explosionRadius = 1f;
		public ExplosiveRoundsAttachment(float explosionRadius)
		{
			this.explosionRadius = explosionRadius;
		}

		public void Apply(Weapon weapon)
		{
			// .. apply to weapon
		}
	}
}