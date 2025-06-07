using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotsParent;
    public int NumberOfSlots = 10;

    public ItemData testItem1; //for testing
    public ItemData testItem2;
    public ItemData testItem3;

    private List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public List<InventorySlotUI> itemsInfo = new List<InventorySlotUI>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateSlots();

        AddItem(testItem1, 3);
        AddItem(testItem2, 15);
        AddItem(testItem3, 37);
    }

    void GenerateSlots()
    {
        for(int i = 0; i < NumberOfSlots; i++)
        {
            InventorySlot newSlot = new InventorySlot();
            inventorySlots.Add(newSlot);

            GameObject slot = Instantiate(slotPrefab, slotsParent);
            InventorySlotUI slotUI = slot.GetComponent<InventorySlotUI>();
            slotUI.SetSlot(newSlot);
            itemsInfo.Add(slotUI);
        }
    }

    public void AddItem(ItemData itemToAdd, int quantity)
    {
        foreach (var slot in inventorySlots)
        {
            if (!slot.IsEmpty && slot.item == itemToAdd && itemToAdd.isStackable)
            {
                slot.quantity += quantity;
                UpdateUI();
                return;
            }
        }

        foreach (var slot in inventorySlots)
        {
            if (slot.IsEmpty)
            {
                slot.item = itemToAdd;
                slot.quantity = quantity;
                UpdateUI();
                return;
            }
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < itemsInfo.Count; i++)
        {
            itemsInfo[i].SetSlot(inventorySlots[i]);
        }
    }
}
