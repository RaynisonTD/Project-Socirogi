using UnityEngine;
using System.Collections;
using System;
using Inventory_System;
using UnityEngine.Tilemaps;

public class loot : MonoBehaviour
{
    public Item item;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            InventoryManager.Instance.AddItem(item);
        }
    }
}
