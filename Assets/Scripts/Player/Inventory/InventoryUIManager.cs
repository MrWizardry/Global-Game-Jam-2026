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

    public void AddItem(InventoryItem item)
    {
        if (item == null || item.itemData == null)
        {
            Debug.LogWarning("Tentou adicionar um InventoryItem inválido à UI.");
            return;
        }

        InventorySlotUI targetSlot = FindAvailableSlot(item);

        if (targetSlot == null)
        {
            Debug.Log("Não existe espaço disponível para esse item.");
            return;
        }

        GameObject newItem = Instantiate(
            inventoryItemPrefab,
            targetSlot.transform
        );

        InventoryItemUI itemUI = newItem.GetComponent<InventoryItemUI>();

        itemUI.item = item;
        itemUI.currentSlot = targetSlot;

        targetSlot.SetItem(itemUI);

        newItem.transform.localPosition = Vector3.zero;

        UnityEngine.UI.Image image = newItem.GetComponent<UnityEngine.UI.Image>();

        if (image != null)
        {
            image.sprite = item.itemData.icon;
        }
    }

    private InventorySlotUI FindAvailableSlot(InventoryItem item)
    {
        if (item.itemData.itemType == ItemType.Mask)
        {
            // Primeiro tenta equipar automaticamente a máscara.
            if (maskSlots.Count > 0 && !maskSlots[0].IsOccupied())
            {
                return maskSlots[0];
            }

            // Se já existe uma máscara equipada,
            // coloca a nova máscara no inventário normal.
            foreach (InventorySlotUI slot in normalSlots)
            {
                if (!slot.IsOccupied())
                {
                    return slot;
                }
            }

            return null;
        }

        // Outros itens vão para o inventário normal.
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