using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Inventory_System
{
    public class InventoryManager : MonoBehaviour
    {
        public GameObject Inventory;
        public int selectedIndex = 0; // The selected index in the inventory
        public ItemSlot[] itemSlot; // Inventory item slots
        private bool menuOpen; // To toggle the inventory open/close

        // Input System action voor scrollen
        public InputAction scrollAction;

       
       

        private void Awake()
        {
            PlayerController.OnInventoryInput += ToggleInventory;
            scrollAction.Enable(); // Activeer de input action

        }

        private void OnDestroy()
        {
            PlayerController.OnInventoryInput -= ToggleInventory;
            scrollAction.Disable(); // Deactiveer de input action

        }

        private void ToggleInventory()
        {
            // Toggle de zichtbaarheid van het inventory-menu
            menuOpen = !menuOpen;
            Inventory.SetActive(menuOpen);
        }

        public void AddItem(string itemName, int quantity, Sprite icon)
        {
            // Voeg item toe aan de eerste beschikbare slot in het inventory
            for (int i = 0; i < itemSlot.Length; i++)
            {
                if (itemSlot[i].isFull == false)
                {
                    itemSlot[i].AddItem(itemName, quantity, icon);
                    return;
                }
            }
        }

        public void DeselectAll()
        {
            for (int i = 0; i < itemSlot.Length; i++)
            {
                itemSlot[i].selectedShader.SetActive(false);
                itemSlot[i].thisItemSelected = false; 
            }
        }




    }
}
