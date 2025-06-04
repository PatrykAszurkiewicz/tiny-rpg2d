using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public int maxSlots = 20;
    public List<InventorySlot> slots = new();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            for(int i = 0; i < maxSlots; i++)
            {
                slots.Add(new InventorySlot());
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public bool AddItem(ItemData item, int amount = 1)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].IsEmpty && slots[i].item == item && item.isStackable && slots[i].quantity < item.maxStack)
            {
                slots[i].quantity += amount;
                return true;
            }
        }
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].IsEmpty)
            {
                slots[i].item = item;
                slots[i].quantity = amount;
                return true;
            }
        }
        Debug.Log("Brak miejsca w ekwipunku");
        return false;
    }
}
