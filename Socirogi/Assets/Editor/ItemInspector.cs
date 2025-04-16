using UI.Inventory_System;
using UnityEditor;
using UnityEngine;

// Dit maakt een editor voor alle ScriptableObjects die van de 'Item' klasse afstammen.
[CustomEditor(typeof(Item), true)]
public class ItemEditor : Editor
{
    // Dit zorgt ervoor dat we de standaard GUI voor het ScriptableObject behouden
    public override void OnInspectorGUI()
    {
        // Begin met het tekenen van de standaard items in de GUI
        base.OnInspectorGUI();

        // Krijg de referentie naar het Item-object
        Item item = (Item)target;

        // Pas de GUI aan
        EditorGUILayout.LabelField("Item Properties", EditorStyles.boldLabel);

        // Laat de velden van het item zien en maak ze bewerkbaar
        item.itemName = EditorGUILayout.TextField("Item Name", item.itemName);
        item.itemImage = (Sprite)EditorGUILayout.ObjectField("Item Image", item.itemImage, typeof(Sprite), false);
        item.maxQuantity = EditorGUILayout.IntField("Max Quantity", item.maxQuantity);

        // Zorg ervoor dat alle veranderingen in de inspector worden opgeslagen
        if (GUI.changed)
        {
            EditorUtility.SetDirty(item);
        }
    }
}