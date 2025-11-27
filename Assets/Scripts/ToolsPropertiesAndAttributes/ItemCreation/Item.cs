using UnityEngine;

namespace ToolsPropertiesAndAttributes.ItemCreation
{
	[CreateAssetMenu(fileName = "New Item", menuName = "ItemCreation/Item")]
	public class Item : ScriptableObject
	{
		public string name;
		public int damage;
		public Sprite icon;
	}
}