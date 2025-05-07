using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory_System
{
    public class ItemSlot : MonoBehaviour, IPointerClickHandler
    {
    
        private InventoryManager inventoryManager;
        
        //Item Information
        public string itemName;
        public int quantity;
        public Sprite itemSprite;
        public bool isFull;
    
        [SerializeField]
        private TMP_Text quantityText;
    
        
        //Item Slot
        [SerializeField]
        private Image itemImage;
        
        public bool thisItemSelected;
        
        public GameObject selectedShader;


        private void Start()
        {
            inventoryManager = FindAnyObjectByType<InventoryManager>();
        }


        public void AddItem(string itemName, int quantity, Sprite itemSprite)
        {
            this.itemName = itemName;
            this.quantity = quantity;
            this.itemSprite = itemSprite;
            isFull = true;
            
            quantityText.text = quantity.ToString();
            quantityText.enableCulling = true;
            itemImage.sprite = itemSprite;
        }
        
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnLeftCLick();
            }

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightClick();
            }
        }

        private void OnLeftCLick()
        {
            inventoryManager.DeselectAll();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            
        }

        private void OnRightClick()
        {
            
        }


        
    }
}
