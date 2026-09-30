using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager Instance;

    [Header("Slots Normais")]
    public List<InventorySlotUI> normalSlots;

    [Header("Slots Especiais")]
    public List<InventorySlotUI> maskSlots;

    [Header("Prefab do Item")]
    public GameObject inventoryItemPrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool CanAddItem(InventoryItem item)
    {
        return FindAvailableSlot(item) != null;
    }

    public InventoryItemUI AddItem(InventoryItem item)
    {
        if (item == null || item.itemData == null)
        {
            Debug.LogWarning("Tentou adicionar um InventoryItem inválido à UI.");
            return null;
        }

        InventorySlotUI targetSlot = FindAvailableSlot(item);

        if (targetSlot == null)
        {
            Debug.Log("Não existe espaço disponível para esse item.");
            return null;
        }

        GameObject newItem = Instantiate(
            inventoryItemPrefab,
            targetSlot.transform
        );

        InventoryItemUI itemUI = newItem.GetComponent<InventoryItemUI>();

        itemUI.item = item;
        itemUI.currentSlot = targetSlot;
        itemUI.isFromSafe = false;

        targetSlot.SetItem(itemUI);

        newItem.transform.localPosition = Vector3.zero;

        UnityEngine.UI.Image image =
            newItem.GetComponent<UnityEngine.UI.Image>();

        if (image != null)
        {
            image.sprite = item.itemData.icon;
        }

        return itemUI;
    }

    public void RemoveItem(InventoryItem item)
    {
        if (item == null)
            return;

        foreach (InventorySlotUI slot in normalSlots)
        {
            if (slot.currentItem != null &&
                slot.currentItem.item == item)
            {
                Destroy(slot.currentItem.gameObject);
                slot.ClearSlot();
                return;
            }
        }

        foreach (InventorySlotUI slot in maskSlots)
        {
            if (slot.currentItem != null &&
                slot.currentItem.item == item)
            {
                Destroy(slot.currentItem.gameObject);
                slot.ClearSlot();
                return;
            }
        }
    }

    private InventorySlotUI FindAvailableSlot(InventoryItem item)
    {
        if (item.itemData.itemType == ItemType.Mask)
        {
            if (maskSlots.Count > 0 && !maskSlots[0].IsOccupied())
            {
                return maskSlots[0];
            }

            foreach (InventorySlotUI slot in normalSlots)
            {
                if (!slot.IsOccupied())
                {
                    return slot;
                }
            }

            return null;
        }

        foreach (InventorySlotUI slot in normalSlots)
        {
            if (!slot.IsOccupied())
            {
                return slot;
            }
        }

        return null;
    }
}