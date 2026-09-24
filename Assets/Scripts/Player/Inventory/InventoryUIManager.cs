using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager Instance;

    [Header("Slots Normais")]
    public List<InventorySlotUI> normalSlots;

    [Header("Slots Especiais")]
    public List<InventorySlotUI> maskSlots;

    [Header("Prefab do Item")]
    public GameObject inventoryItemPrefab;

    [Header("Sprites")]
    public Sprite maskSprite;
    public Sprite weaponSprite;
    public Sprite itemSprite;
    public Sprite randomSprite;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(ItemType itemType)
    {
        InventorySlotUI targetSlot = FindAvailableSlot(itemType);

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

        itemUI.itemType = itemType;
        itemUI.currentSlot = targetSlot;

        targetSlot.SetItem(itemUI);

        newItem.transform.localPosition = Vector3.zero;

        Image image = newItem.GetComponent<Image>();

        if (image != null)
        {
            image.sprite = GetSprite(itemType);
        }
    }

    private InventorySlotUI FindAvailableSlot(ItemType itemType)
{
    // Máscara
    if (itemType == ItemType.Mask)
    {
        // Se o slot de máscara estiver livre,
        // equipa automaticamente.
        if (maskSlots.Count > 0 && !maskSlots[0].IsOccupied())
        {
            return maskSlots[0];
        }

        // Se já estiver ocupado, coloca no inventário normal.
        foreach (InventorySlotUI slot in normalSlots)
        {
            if (!slot.IsOccupied())
            {
                return slot;
            }
        }

        return null;
    }

    // Outros itens vão para os espaços normais
    foreach (InventorySlotUI slot in normalSlots)
    {
        if (!slot.IsOccupied())
        {
            return slot;
        }
    }

    return null;
}

    private Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Mask:
                return maskSprite;

            case ItemType.Weapon:
                return weaponSprite;

            case ItemType.Item:
                return itemSprite;

            case ItemType.Random:
                return randomSprite;

            default:
                return null;
        }
    }
}