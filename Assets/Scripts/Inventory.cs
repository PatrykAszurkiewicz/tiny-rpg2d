using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public int maxSlots = 20;
    public List<InventorySlot> slots = new();
    public ItemData testItem;
    
    private void Awake() //singleton
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
    public void Start()
    {
        AddItem(testItem, 3);
    }
    public bool AddItem(ItemData item, int amount = 1) //addding items
    {
        for (int i = 0; i < slots.Count; i++) //is item=item and less than maxstacks but more than 1
        {
            if (!slots[i].IsEmpty && slots[i].item == item && item.isStackable && slots[i].quantity < item.maxStack)
            {
                slots[i].quantity += amount;
                return true;
            }
        }
        for (int i = 0; i < slots.Count; i++) //is slot empty
        {
            if (slots[i].IsEmpty)
            {
                slots[i].item = item;
                slots[i].quantity = amount;
                return true;
            }
        }
        InventoryUI.Instance?.RefreshUI();

        Debug.Log("Brak miejsca w ekwipunku");
        return false;
    }

}
