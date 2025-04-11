using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace UI.Inventory_System
{
    public class Inventory : MonoBehaviour
    {
    
        [SerializeReference] public List<ItemSlot> items = new();
        
        [Space]
        [Header("Inventory Menu Components")]
        public GameObject inventoryMenu;

        public GameObject itemPanel;
        public GameObject itemPanelGrid;
        
        private List<ItemPanel> existingPanels = new();

        [Space] 
        public  int inventorySize = 6;
        
        
        
        void Start()
        {
            for (int i = 0; i < inventorySize; i++)
            {
                items.Add(new ItemSlot(null, 0));
            }
        }

        public void RefreshInventory()
        {
            existingPanels = itemPanelGrid.GetComponentsInChildren<ItemPanel>().ToList();
            //Create Panels if needed
            if (existingPanels.Count < inventorySize)
            {
                int amountToCreate = inventorySize - existingPanels.Count;
                for (int i = 0; i < amountToCreate; i++)
                {
                    GameObject newPanel = Instantiate(itemPanel, itemPanelGrid.transform);
                    existingPanels.Add(newPanel.GetComponent<ItemPanel>());
                }
            }

            int index = 0;
            foreach (ItemSlot i in items)
            {
                //Name our List Elements
                i.itemName = "" + (index + 1);
                if (i.item) i.itemName += ": " + i.item.GiveName();
                else i.itemName += ": -";

                //Update our Panels
                ItemPanel panel = existingPanels[index];
                panel.name = i.itemName + " Panel";
                if (panel != null)
                {
                    panel.inventory = this;
                    panel.itemSlot = i;
                    if (i.item)
                    {
                        panel.itemImage.gameObject.SetActive(true);
                        panel.itemImage.sprite = i.item.GetItemIcon();
                        
                        panel.stacksText.gameObject.SetActive(true);
                        panel.stacksText.text = "" + i.quantity;
                    }
                    else
                    {
                        panel.itemImage.gameObject.SetActive(false);
                        panel.stacksText.gameObject.SetActive(false);
                    }
                }
                index++;
            }
        }

        // Update is called once per frame
        void Update()
        {
            // set the inventory to active or in active on button press
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventoryMenu.activeSelf)
                {
                    inventoryMenu.SetActive(false);
                    
                }
                else
                {
                    inventoryMenu.SetActive(true);
                    
                    RefreshInventory();
                }
                
            }
        }

        
    }
}
