using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<Vector2Int, ItemInstance> slots = new Dictionary<Vector2Int, ItemInstance>();

    [SerializeField] private int width = 8;
    [SerializeField] private int height = 8;

    public bool AddItem(ItemInstance newItem)
    {
        Debug.Log($"Intentando agregar item: {newItem.itemData.itemName} x{newItem.quantity}");

        // 1. Intentar apilar si es stackeable
        foreach (var slot in slots)
        {
            if (slot.Value.IsStackableWith(newItem))
            {
                int spaceLeft = slot.Value.itemData.maxStack - slot.Value.quantity;
                int amountToAdd = Mathf.Min(spaceLeft, newItem.quantity);

                slot.Value.quantity += amountToAdd;
                newItem.quantity -= amountToAdd;

                Debug.Log($"Item {slot.Value.itemData.itemName} apilado en slot {slot.Key}, cantidad ahora: {slot.Value.quantity}");

                if (newItem.quantity <= 0)
                {
                    Debug.Log("Item agregado completamente por apilamiento.");
                    return true;
                }
            }
        }

        // 2. Buscar slot vacío
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                if (!slots.ContainsKey(pos))
                {
                    slots[pos] = new ItemInstance(newItem.itemData, newItem.quantity);
                    Debug.Log($"Item {newItem.itemData.itemName} colocado en slot vacío {pos} con cantidad {newItem.quantity}");
                    return true;
                }
            }
        }

        Debug.LogWarning($"Inventario lleno. No se pudo agregar {newItem.itemData.itemName}");
        return false; // Inventario lleno
    }

    public void RemoveItem(Vector2Int slotPos, int amount = 1)
    {
        if (slots.ContainsKey(slotPos))
        {
            slots[slotPos].quantity -= amount;
            Debug.Log($"Removiendo {amount} de {slots[slotPos].itemData.itemName} en slot {slotPos}. Cantidad restante: {slots[slotPos].quantity}");

            if (slots[slotPos].quantity <= 0)
            {
                Debug.Log($"Slot {slotPos} ahora está vacío. {slots[slotPos].itemData.itemName} eliminado.");
                slots.Remove(slotPos);
            }
        }
        else
        {
            Debug.LogWarning($"Intentaste remover un item de un slot vacío: {slotPos}");
        }
    }

    public void MoveItem(Vector2Int from, Vector2Int to)
    {
        if (!slots.ContainsKey(from))
        {
            Debug.LogWarning($"No hay item en el slot {from} para mover.");
            return;
        }

        if (!slots.ContainsKey(to))
        {
            slots[to] = slots[from];
            slots.Remove(from);
            Debug.Log($"Item movido de {from} a {to}");
        }
        else
        {
            ItemInstance fromItem = slots[from];
            ItemInstance toItem = slots[to];

            if (fromItem.IsStackableWith(toItem))
            {
                int spaceLeft = toItem.itemData.maxStack - toItem.quantity;
                int amountToAdd = Mathf.Min(spaceLeft, fromItem.quantity);

                toItem.quantity += amountToAdd;
                fromItem.quantity -= amountToAdd;

                Debug.Log($"Items stackeados al mover: {fromItem.itemData.itemName} ahora tiene {fromItem.quantity}, {toItem.itemData.itemName} ahora tiene {toItem.quantity}");

                if (fromItem.quantity <= 0)
                {
                    slots.Remove(from);
                    Debug.Log($"Slot {from} quedó vacío después del stackeo.");
                }
            }
            else
            {
                ItemInstance temp = slots[from];
                slots[from] = slots[to];
                slots[to] = temp;

                Debug.Log($"Items intercambiados entre slot {from} y {to}");
            }
        }
    }

    public void ExpandInventory(int extraRows)
    {
        height += extraRows;
        Debug.Log($"Inventario expandido. Nuevo tamaño: {width}x{height}");
    }
}
