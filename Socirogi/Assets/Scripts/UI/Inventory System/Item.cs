using UnityEngine;

namespace UI.Inventory_System
{
    public abstract class Item : ScriptableObject
    {
        public abstract string GiveName();

        public virtual int MaxQuantity()
        {
            return 1;
        }

        public virtual Sprite GetItemIcon()
        {
             return Resources.Load<Sprite>("UI/Images/Item Images/No Item Image Icon");
        }
    }
}

