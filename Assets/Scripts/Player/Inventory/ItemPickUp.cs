using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private ItemType itemType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventorySystem.Instance.AddItem(itemType);
            Destroy(gameObject);
        }
    }
}
