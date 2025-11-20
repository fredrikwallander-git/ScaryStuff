using UnityEngine;

public class Health : MonoBehaviour
{
	public float Value { get; private set; }

	public void TakeDamage(float damage)
	{
		Value -= damage;
	}

	public void Heal(float heal)
	{
		Value += heal;
	}
}