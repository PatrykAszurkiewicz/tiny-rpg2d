using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;
    public GameObject slotPrefab;
    private Inventory inventory;
    private List<GameObject> uiSlots = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RefreshUI();
    }
    private void Awake()
    {
        inventory = Inventory.Instance;

        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
    public void RefreshUI()
    {
        // Czyœcimy stare sloty
        foreach (var slot in uiSlots)
        {
            Destroy(slot);
        }
        uiSlots.Clear();

        // Tworzymy nowe
        foreach (var slotData in inventory.slots)
        {
            GameObject slotGO = Instantiate(slotPrefab, transform);
            uiSlots.Add(slotGO);

            Image iconImage = slotGO.transform.Find("Icon").GetComponent<Image>();
            TextMeshProUGUI countText = slotGO.transform.Find("Text").GetComponent<TextMeshProUGUI>();

            if (!slotData.IsEmpty)
            {
                iconImage.sprite = slotData.item.icon;
                iconImage.enabled = true;

                countText.text = slotData.item.isStackable && slotData.quantity > 1
                    ? slotData.quantity.ToString()
                    : "";
            }
            else
            {
                iconImage.enabled = false;
                countText.text = "";
            }
        }
    }
}
