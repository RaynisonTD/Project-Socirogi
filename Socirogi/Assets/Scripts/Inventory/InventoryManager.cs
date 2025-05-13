using System;
using Player;
using Unity.VisualScripting;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Inventory_System
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;
        
        public Item[] startItems;
        public int maxStackedItems = 4;
        public InventorySlot[] inventorySlots;
        public GameObject inventoryItemPrefab;

        int selectedSlot = -1;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            ChangeSelectedSlot(0);
            foreach (Item item in startItems)
            {
                AddItem(item);
            }
        }

        private void Update()
        {
            if (Input.inputString != null)
            {
                bool isNumber = int.TryParse(Input.inputString, out int number);
                if (isNumber && number > 0 && number < 8)
                {
                    ChangeSelectedSlot(number - 1);
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Item receivedItem = InventoryManager.Instance.GetSelectedItem(true);
                if (receivedItem != null)
                {
                    Debug.Log("Item Picked Up" + receivedItem);
                }
                else
                {
                    Debug.Log("Item not picked up");
                }
            }
        }

        void ChangeSelectedSlot(int newValue)
        {
            if (selectedSlot >= 0)
            {
                inventorySlots[selectedSlot].Deselect();
            }

            inventorySlots[newValue].Select();
            selectedSlot = newValue;
        }

        public bool AddItem(Item item)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot != null &&
                    itemInSlot.item == item &&
                    itemInSlot.count < maxStackedItems &&
                    itemInSlot.item.stackable == true)
                {
                    itemInSlot.count++;
                    itemInSlot.RefreshCount();
                    return true;
                }
            }


            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot == null)
                {
                    SpawnNewItem(item, slot);
                    return true;
                }
            }

            return false;
        }

        void SpawnNewItem(Item item, InventorySlot slot)
        {
            GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
            InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
            inventoryItem.InitialiseItem(item);
        }

        public Item GetSelectedItem(bool use)
        {
            InventorySlot slot = inventorySlots[selectedSlot];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                
                Item item = itemInSlot.item;
                
                if (use == true)
                {
                    itemInSlot.count--;
                    if (itemInSlot.count <= 0)
                    {
                        Destroy(itemInSlot.gameObject);
                    }
                    else
                    {
                        itemInSlot.RefreshCount();
                    }
                }
                return item;
                
            }
            return null;
        }
    }

}
