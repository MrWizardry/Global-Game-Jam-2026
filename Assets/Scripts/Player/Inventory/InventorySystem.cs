using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ItemType
{
    Mask,
    Weapon,
    Item,
    Random
}

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    private HashSet<ItemType> items = new HashSet<ItemType>();

    public GameObject inventoryUI;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    public void AddItem(ItemType item)
    {
        if(items.Add(item))
        {
            Debug.Log($"Item {item} added to inventory.");
            if(InventoryUIManager.Instance != null)
            {
                InventoryUIManager.Instance.AddItem(item);
            }
        }
        
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

    public void LogInventory()
    {
        if (items.Count == 0)
        {
            Debug.Log("Inventário vazio.");
            return;
        }

        string itemList = string.Join(", ", items.Select(i => i.ToString()));
        Debug.Log($"Itens no inventário: {itemList}");
    }
}