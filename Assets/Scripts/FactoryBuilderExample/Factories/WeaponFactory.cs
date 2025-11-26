using FactoryBuilderExample.Builders;
using FactoryBuilderExample.ExampleAttachments;
using FactoryBuilderExample.Weapons;
using UnityEngine;

namespace FactoryBuilderExample.Factories
{
	// Weapon factory example
	public class WeaponFactory
	{
		// BASIC
		public Weapon CreateWeapon(WeaponConfig config)
		{
			return CreateWeapon(config, null);
		}

		// WITH ATTACHMENTS
		public Weapon CreateWeapon(WeaponConfig config, params IWeaponAttachment[] attachments)
		{
			var builder = new WeaponBuilder()
						  .WithName(config.name)
						  .WithDamage(config.damage)
						  .WithFireRate(config.fireRate)
						  .WithAmmoCount(config.ammoCount)
						  .WithReloadTime(config.reloadTime);

			if (config.StartExplosive)
				builder.MakeExplosive(config.explosionRadius);

			if (attachments != null)
			{
				foreach (var att in attachments)
					builder.AddAttachment(att);
			}

			return builder.Build();
		}

		// WITH OVERRIDES
		public Weapon CreateWeapon(
			WeaponConfig config,
			int? overrideDamage = null,
			float? overrideFireRate = null,
			bool? forceExplosive = null,
			float? explosionRadius = null,
			params IWeaponAttachment[] attachments)
		{
			var builder = new WeaponBuilder()
						  .WithName(config.name)
						  .WithDamage(overrideDamage ?? config.damage)
						  .WithFireRate(overrideFireRate ?? config.fireRate)
						  .WithAmmoCount(config.ammoCount)
						  .WithReloadTime(config.reloadTime);

			if (forceExplosive ?? config.StartExplosive)
				builder.MakeExplosive(explosionRadius ?? config.explosionRadius);

			if (attachments != null)
			{
				foreach (var a in attachments)
					builder.AddAttachment(a);
			}

			return builder.Build();
		}
	}
	
	// Example usage:
	public class PlayerInventory : MonoBehaviour
	{
		public WeaponConfig pistolConfig;
		public WeaponConfig tacticalMGConfig;
		public WeaponConfig grenadeLauncherConfig;

		WeaponFactory factory = new WeaponFactory();
		Weapon currentWeapon;

		void Start()
		{
			// Example: pistol with silencer
			currentWeapon = factory.CreateWeapon(
				pistolConfig,
				new SilencerAttachment()
			);

			// Example: grenade launcher with explosive rounds (bigger boom)
			Weapon bigBoom = factory.CreateWeapon(
				grenadeLauncherConfig,
				null,
				null,
				forceExplosive: true,
				explosionRadius: 6f,
				new ExplosiveRoundsAttachment(6f)
			);

			// Example: machine gun + scope + silencer
			Weapon tacticalMG = factory.CreateWeapon(
				tacticalMGConfig,
				new ScopeAttachment(1.25f),
				new SilencerAttachment()
			);
		}
	}
}