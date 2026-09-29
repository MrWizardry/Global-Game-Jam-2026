using UnityEngine;
using System.Collections.Generic;

public class SafeInventory : MonoBehaviour
{
    [SerializeField] private List<ItemData> items = new List<ItemData>();

    public IReadOnlyList<ItemData> Items => items;
    public void AddItem(ItemData item)
    {
        if (item == null)
        {
            Debug.LogWarning("Tentou adicionar um item nulo ao inventário do cofre.");
            return;
        }

        items.Add(item);
        Debug.Log($"Item {item.itemName} adicionado ao inventário do cofre.");
    }
    
    public void RemoveItem(ItemData item)
    {
        if (item == null)
        {
            Debug.LogWarning("Tentou remover um item nulo do inventário do cofre.");
            return;
        }

        items.Remove(item);
        Debug.Log($"Item {item.itemName} removido do inventário do cofre.");
        
    }
}
