using TMPro;
using UI.Inventory_System;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI quantityText;

    [HideInInspector] public ItemSlotInfo itemSlot;

    public void UpdateVisuals()
    {
        if (itemSlot != null && itemSlot.item != null)
        {
            itemImage.sprite = itemSlot.item.GetItemImage();
            itemImage.gameObject.SetActive(true);

            quantityText.text = itemSlot.quantity.ToString();
            quantityText.gameObject.SetActive(true);
        }
        else
        {
            itemImage.gameObject.SetActive(false);
            quantityText.gameObject.SetActive(false);
        }
    }
}