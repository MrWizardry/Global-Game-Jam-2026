using System.Collections.Generic;
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

    private List<InventoryItem> items = new List<InventoryItem>();

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

    // Usado quando o jogador coleta um item do mundo.
    public void AddItem(ItemData itemData)
    {
        if (itemData == null)
        {
            Debug.LogWarning("Tentou adicionar um item nulo ao inventário.");
            return;
        }

        InventoryItem newItem = new InventoryItem(itemData);

        if (!TryAddItem(newItem))
        {
            Debug.Log("Não foi possível adicionar o item ao inventário.");
        }
    }

    // Usado para transferências.
    public bool TryAddItem(InventoryItem item)
    {
        if (item == null || item.itemData == null)
            return false;

        if (InventoryUIManager.Instance == null)
            return false;

        if (!InventoryUIManager.Instance.CanAddItem(item))
        {
            Debug.Log("Não existe espaço disponível no inventário.");
            return false;
        }

        items.Add(item);

        InventoryUIManager.Instance.AddItem(item);

        Debug.Log($"Item {item.itemData.itemName} adicionado ao inventário.");

        return true;
    }

    public bool HasItem(ItemData itemData)
    {
        return items.Exists(item => item.itemData == itemData);
    }

    public bool HasItem(ItemType itemType)
    {
        return items.Exists(item => item.itemData.itemType == itemType);
    }

    public void RemoveItem(InventoryItem item)
    {
        if (item == null)
            return;

        if (items.Remove(item))
        {
            Debug.Log($"Item {item.itemData.itemName} removido do inventário.");

            if (InventoryUIManager.Instance != null)
            {
                InventoryUIManager.Instance.RemoveItem(item);
            }
        }
    }

    public List<InventoryItem> GetItems()
    {
        return items;
    }
}