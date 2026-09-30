using UnityEngine;
using System.Collections.Generic;

public class SafeInventory : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] private int maxSlots = 6;

    [Header("Itens do Cofre")]
    [SerializeField] private List<ItemData> items = new List<ItemData>();

    public IReadOnlyList<ItemData> Items => items;

    public int MaxSlots => maxSlots;

    public bool HasSpace()
    {
        return items.Count < maxSlots;
    }

    public bool AddItem(ItemData item)
    {
        if (item == null)
        {
            Debug.LogWarning(
                "Tentou adicionar um item nulo ao inventário do cofre."
            );

            return false;
        }

        if (!HasSpace())
        {
            Debug.Log("O cofre está cheio.");
            return false;
        }

        items.Add(item);

        Debug.Log(
            $"Item {item.itemName} adicionado ao inventário do cofre."
        );

        return true;
    }

    public bool RemoveItem(ItemData item)
    {
        if (item == null)
        {
            Debug.LogWarning(
                "Tentou remover um item nulo do inventário do cofre."
            );

            return false;
        }

        if (!items.Remove(item))
        {
            Debug.LogWarning(
                $"O item {item.itemName} não estava no cofre."
            );

            return false;
        }

        Debug.Log(
            $"Item {item.itemName} removido do inventário do cofre."
        );

        return true;
    }

    public bool Contains(ItemData item)
    {
        return items.Contains(item);
    }
}