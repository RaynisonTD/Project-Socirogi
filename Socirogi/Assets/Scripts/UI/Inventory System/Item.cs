using UnityEngine;

namespace UI.Inventory_System
{
   
    public abstract class Item : ScriptableObject
    {
        [HideInInspector]
        public string itemName;
        
        [HideInInspector]
        public Sprite itemImage;
        
        [HideInInspector]
        public int maxQuantity;

        public virtual string GiveName() => itemName;
        public virtual Sprite GetItemImage() => itemImage;
    }
}
