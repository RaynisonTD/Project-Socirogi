

namespace UI.Inventory_System
{
    [System.Serializable]
    public class ItemSlotInfo 
    {
        public Item item;
        public int quantity;
        
        public ItemSlotInfo(Item newItem, int newQuantity)
        {
            item = newItem;
            quantity = newQuantity;
        }
        
        
    }
    
   
}
