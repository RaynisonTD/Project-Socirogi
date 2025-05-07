using Inventory_System;
using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private string itemName;
        [SerializeField] private int quantity;
        [SerializeField] private Sprite icon;

        private InventoryManager inventoryManager;

        private void Start()
        {
            inventoryManager = FindObjectOfType<InventoryManager>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Check of het de speler is via component-check
            if (collision.gameObject.GetComponent<Player.PlayerController>())
            {
                inventoryManager.AddItem(itemName, quantity, icon);
                Destroy(gameObject); // Verwijder het item na oppakken
            }
        }
    }
}