using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotsParent;
    public int NumberOfSlots = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateSlots();
    }

    void GenerateSlots()
    {
        for(int i = 0; i < NumberOfSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotsParent);
        }
    }
}
