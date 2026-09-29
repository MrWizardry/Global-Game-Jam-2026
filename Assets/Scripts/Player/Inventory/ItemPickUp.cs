using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    public void Collect()
    {
        if (InventorySystem.Instance == null)
            return;

        InventorySystem.Instance.AddItem(itemData);

        Destroy(gameObject);
    }
}