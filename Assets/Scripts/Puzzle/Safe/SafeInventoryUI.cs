using System.Collections.Generic;
using UnityEngine;

public class SafeInventoryUI : MonoBehaviour
{
    public static SafeInventoryUI Instance;

    [Header("Inventário do Cofre")]
    [SerializeField] private SafeInventory safeInventory;

    [Header("Painel")]
    [SerializeField] private GameObject inventoryPanel;

    [Header("Slots do Cofre")]
    [SerializeField] private List<InventorySlotUI> safeSlots;

    [Header("Prefab do Item")]
    [SerializeField] private GameObject inventoryItemPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);
    }

    public bool IsOpen()
    {
        return inventoryPanel != null &&
               inventoryPanel.activeSelf;
    }

    public void Open()
    {
        if (inventoryPanel == null)
            return;

        inventoryPanel.SetActive(true);

        RefreshUI();
    }

    public void Close()
    {
        if (inventoryPanel == null)
            return;

        inventoryPanel.SetActive(false);
    }

    public void RefreshUI()
    {
        ClearSlots();

        foreach (ItemData itemData in safeInventory.Items)
        {
            CreateItemUI(itemData);
        }
    }

    private void ClearSlots()
    {
        foreach (InventorySlotUI slot in safeSlots)
        {
            if (slot.currentItem != null)
            {
                Destroy(slot.currentItem.gameObject);
                slot.ClearSlot();
            }
        }
    }

    private void CreateItemUI(ItemData itemData)
    {
        InventorySlotUI targetSlot = FindAvailableSlot();

        if (targetSlot == null)
        {
            Debug.LogWarning(
                "Não existem slots suficientes na UI do cofre."
            );

            return;
        }

        GameObject newItem = Instantiate(
            inventoryItemPrefab,
            targetSlot.transform
        );

        InventoryItemUI itemUI =
            newItem.GetComponent<InventoryItemUI>();

        if (itemUI == null)
        {
            Debug.LogError(
                "O prefab do item não possui InventoryItemUI."
            );

            Destroy(newItem);
            return;
        }

        // Cria o InventoryItem visual do cofre
        InventoryItem inventoryItem =
            new InventoryItem(itemData);

        itemUI.item = inventoryItem;
        itemUI.currentSlot = targetSlot;

        targetSlot.SetItem(itemUI);

        newItem.transform.localPosition = Vector3.zero;

        UnityEngine.UI.Image image =
            newItem.GetComponent<UnityEngine.UI.Image>();

        if (image != null)
        {
            image.sprite = itemData.icon;
        }
    }

    private InventorySlotUI FindAvailableSlot()
    {
        foreach (InventorySlotUI slot in safeSlots)
        {
            if (!slot.IsOccupied())
                return slot;
        }

        return null;
    }

    public void TransferItem(InventoryItemUI itemUI)
    {
        if (itemUI == null || itemUI.item == null)
            return;

        Debug.Log(
            "Tentou transferir: " +
            itemUI.item.itemData.itemName
        );
    }
}