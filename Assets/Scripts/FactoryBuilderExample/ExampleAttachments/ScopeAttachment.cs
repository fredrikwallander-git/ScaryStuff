using FactoryBuilderExample.Weapons;

namespace FactoryBuilderExample.ExampleAttachments
{
	public class ScopeAttachment : IWeaponAttachment {
		float damageMultiplier = 1f;
		public ScopeAttachment(float damageMultiplier)
		{
			this.damageMultiplier = damageMultiplier;
		}

		public void Apply(Weapon weapon)
		{
			// .. increase damage by using the multiplier
		}
	}
}