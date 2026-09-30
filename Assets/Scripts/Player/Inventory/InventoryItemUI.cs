using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemUI : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IPointerClickHandler
{
    public InventoryItem item;

    public InventorySlotUI currentSlot;

    [HideInInspector]
    public bool isFromSafe;

    private Transform originalParent;
    private Vector3 originalPosition;

    private Canvas canvas;
    private CanvasGroup canvasGroup;

    public ItemType ItemType
    {
        get
        {
            return item.itemData.itemType;
        }
    }

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();

        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
{
    if (SafeInventoryUI.Instance != null &&
        SafeInventoryUI.Instance.IsOpen())
    {
        SafeInventoryUI.Instance.TransferItem(this);
    }
}

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = transform.localPosition;

        canvasGroup.blocksRaycasts = false;

        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (currentSlot == null || transform.parent == canvas.transform)
        {
            ReturnToOriginalSlot();
        }
    }

    public void ReturnToOriginalSlot()
    {
        transform.SetParent(originalParent);
        transform.localPosition = originalPosition;
    }
}