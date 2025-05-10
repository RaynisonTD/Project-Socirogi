using Inventory_System;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickUp;


    public void PickUpItem(int id)
    { 
        bool result = inventoryManager.AddItem(itemsToPickUp[id]);
        if (result == true)
        {
            Debug.Log("Item Picked Up");
        }
        else
        {
            Debug.Log("Item not picked up");
        }
    }
}
