[System.Serializable]
public class ItemInstance
{
    public ItemClass itemData;
    public int Quantity;

    public ItemInstance(ItemClass data, int qty)
    {
        itemData = data;
        Quantity = qty;
    }
}
