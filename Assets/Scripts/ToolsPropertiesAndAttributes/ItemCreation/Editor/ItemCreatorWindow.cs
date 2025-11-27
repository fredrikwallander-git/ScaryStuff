using UnityEditor;
using UnityEngine;

namespace ToolsPropertiesAndAttributes.ItemCreation.Editor
{
	public class ItemCreatorWindow : EditorWindow
	{
		string itemName;
		int damage;
		Sprite icon;
		
		[MenuItem("Items/Item Creator")]
		public static void ShowWindow()
		{
			GetWindow<ItemCreatorWindow>("Level Tools");
		}
		
		void OnGUI()
		{
			GUILayout.Label("Create New Item", EditorStyles.boldLabel);
			
			itemName = EditorGUILayout.TextField("Item Name", itemName);
			damage = EditorGUILayout.IntField("Damage", damage);
			icon = EditorGUILayout.ObjectField("Icon", icon, typeof(Sprite), false) as Sprite;

			if (GUILayout.Button("Create Item"))
			{
				CreateItem();
			}
		}

		void CreateItem()
		{
			Item newItem = ScriptableObject.CreateInstance<Item>();
			
			newItem.name = itemName;
			newItem.damage = damage;
			newItem.icon = icon;
			
			// Remember that the folders / path needs to exist.
			// You can auto create the folder if not exists by using the system class Directory / File managers.
			AssetDatabase.CreateAsset(newItem, "Assets/Resources/Items/" + itemName + ".asset");
			AssetDatabase.SaveAssets();
			
			EditorUtility.DisplayDialog("Item created", $"Item: {newItem.name}, created successfully", "OK");
		}
	}
}