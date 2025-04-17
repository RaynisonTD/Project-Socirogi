using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Inventory_System
{
    public class Inventory : MonoBehaviour
    {
        [SerializeReference] public List<ItemSlotInfo> items = new();

        [Header("Inventory Attributes")]
        public GameObject inventoryMenu;
        public GameObject itemPanel;
        public GameObject itemPanelGrid;

        [Space] public int inventorySize = 6;

        public Item itemtoadd;

        private List<ItemPanel> existingPanels = new();

        void Start()
        {
            FillInventory();

            // Voor test: een TestItem SO inladen via Resources (of handmatig toewijzen als nodig)
            
            AddItem(itemtoadd, 80);
            
        
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                ActivateInventory();
            }
        }

        public void FillInventory()
        {
            for (int i = 0; i < inventorySize; i++)
            {
                items.Add(new ItemSlotInfo(null, 0));
            }
        }

        public void ActivateInventory()
        {
            if (inventoryMenu.activeSelf)
            {
                inventoryMenu.SetActive(false);
            }
            else
            {
                inventoryMenu.SetActive(true);
                UpdateInventory();
            }
        }

        public void UpdateInventory()
        {
            existingPanels = itemPanelGrid.GetComponentsInChildren<ItemPanel>().ToList();
            AdjustPanelCount();

            for (int index = 0; index < items.Count; index++)
            {
                UpdatePanel(index, items[index]);
            }
        }

        private void AdjustPanelCount()
        {
            int difference = inventorySize - existingPanels.Count;
            if (difference > 0) AddPanels(difference);
        }

        private void AddPanels(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject newPanel = Instantiate(itemPanel, itemPanelGrid.transform);
                existingPanels.Add(newPanel.GetComponent<ItemPanel>());
            }
        }

        private void UpdatePanel(int index, ItemSlotInfo slot)
        {
            var panel = existingPanels[index];
            panel.name = $"{index + 1}: {(slot.item != null ? slot.item.GiveName() : "-")} Panel";
            panel.itemSlot = slot;
            panel.UpdateVisuals();
        }

        public int AddItem(Item item, int amount)
        {
            // 1. Stack op bestaande sloten
            foreach (ItemSlotInfo slot in items)
            {
                if (slot.item != null && slot.item.GiveName() == item.GiveName())
                {
                    int spaceLeft = item.maxQuantity - slot.quantity;
                    if (spaceLeft > 0)
                    {
                        int addAmount = Mathf.Min(amount, spaceLeft);
                        slot.quantity += addAmount;
                        amount -= addAmount;
                        if (amount == 0) break;
                    }
                }
            }

            // 2. Voeg toe aan lege sloten
            foreach (ItemSlotInfo slot in items)
            {
                if (amount <= 0) break;

                if (slot.item == null)
                {
                    int addAmount = Mathf.Min(amount, item.maxQuantity);
                    slot.item = item;
                    slot.quantity = addAmount;
                    amount -= addAmount;
                }
            }

            if (amount > 0)
            {
                Debug.Log("No space in inventory for " + item.GiveName());
            }

            if (inventoryMenu.activeSelf) UpdateInventory();
            return amount;
        }

        public void ClearSlot(ItemSlotInfo slot)
        {
            slot.item = null;
            slot.quantity = 0;
        }
    }
}
