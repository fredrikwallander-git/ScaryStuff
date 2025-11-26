using DefaultNamespace.Builders;
using UnityEngine;

namespace DefaultNamespace
{
	public class Enemy : MonoBehaviour
	{
		public float movementSpeed;
		public int health;
		public Weapon Weapon;

		public void Start()
		{
			// Example usage of builder for weapon
			// WeaponConfig: config.name, config.speed, etc...
			var weapon = new WeaponBuilder()
						 .WithName("Grenade Launcher")
						 .WithDamage(500)
						 .WithMaterial("GrenadeMaterial")
						 .WithCritChance(0.1f)
						 .AsRanged(true)
						 .WithSpeed(10)
						 .Build();
		}
	}
}