using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    [SerializeField] private ItemType requiredItem;
    [SerializeField] private bool consumeItem = false; // se quiser que a chave suma

    private bool isOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (InventorySystem.Instance.HasItem(requiredItem))
        {
            OpenDoor();

            if (consumeItem)
                InventorySystem.Instance.RemoveItem(requiredItem);
        }
        else
        {
            Debug.Log($"Porta trancada. Precisa de: {requiredItem}");
        }
    }

    private void OpenDoor()
    {
        if (isOpen) return;

        isOpen = true;

        Debug.Log("Porta abriu!");

        // 👉 opções rápidas:
        gameObject.SetActive(false);

        // OU animação depois 🙂
    }
}
