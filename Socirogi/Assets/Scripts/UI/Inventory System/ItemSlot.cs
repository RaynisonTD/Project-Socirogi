


namespace UI.Inventory_System
{
    [System.Serializable]
    public class ItemSlot
    {
        public Item item;
        public string itemName;
        public int quantity;

        public ItemSlot(Item newItem, int newQuantity)
        {
            item = newItem;
            quantity = newQuantity;
            itemName = "";
        }
    }
}
