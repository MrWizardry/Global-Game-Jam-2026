using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Key,
    Baton,
    Card
}
public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    private HashSet<ItemType> items = new HashSet<ItemType>();

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(ItemType item)
    {
        items.Add(item);
        Debug.Log($"Item {item} added to inventory.");
    }

    public bool HasItem(ItemType item)
    {
        return items.Contains(item);
    }

    public void RemoveItem(ItemType item)
    {
        items.Remove(item);
        Debug.Log($"Item {item} removed from inventory.");
    }
}
