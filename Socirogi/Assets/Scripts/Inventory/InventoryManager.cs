using UnityEngine;

namespace Inventory_System
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;
        private AttackOrigin attackOrigin;
        HealingItem healingItem;

        
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
            attackOrigin = FindObjectOfType<AttackOrigin>();
            healingItem = FindFirstObjectByType<HealingItem>();

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
                Debug.Log(receivedItem.type);
                if (receivedItem != null)
                {

                    if (receivedItem.type == ItemType.Attack)
                    {
                        StartCoroutine(attackOrigin.AttackRoutine());
                    }else if (receivedItem.type == ItemType.Potion)
                    {
                        Debug.Log(healingItem);
                        StartCoroutine(healingItem.Heal(receivedItem.healingAmount));
                    }
                }
                else
                {
                    Debug.Log("er is geen item type gevonden grote caramba");
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
