using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IDropHandler
{
    public bool isMaskSlot;

    public InventoryItemUI currentItem;

    public bool IsOccupied()
    {
        return currentItem != null;
    }

    public void SetItem(InventoryItemUI item)
    {
        currentItem = item;
    }

    public void ClearSlot()
    {
        currentItem = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItemUI draggedItem = eventData.pointerDrag?.GetComponent<InventoryItemUI>();

        if (draggedItem == null)
            return;

        // MaskSlot só aceita máscaras
        if (isMaskSlot && draggedItem.itemType != ItemType.Mask)
        {
            draggedItem.ReturnToOriginalSlot();
            return;
        }

        // Slot ocupado
        if (IsOccupied())
        {
            draggedItem.ReturnToOriginalSlot();
            return;
        }

        // Remove do slot antigo
        if (draggedItem.currentSlot != null)
        {
            draggedItem.currentSlot.ClearSlot();
        }

        // Coloca no novo slot
        SetItem(draggedItem);

        draggedItem.currentSlot = this;

        draggedItem.transform.SetParent(transform);
        draggedItem.transform.localPosition = Vector3.zero;
    }
}