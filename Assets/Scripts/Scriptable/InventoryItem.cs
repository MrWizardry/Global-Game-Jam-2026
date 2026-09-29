using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public string instanceID;
    public InventoryItem(ItemData data)
    {
        itemData = data;
        instanceID = Guid.NewGuid().ToString();
    }
}
