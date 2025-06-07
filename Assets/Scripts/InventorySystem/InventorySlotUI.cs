using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    private Image iconImage;
    private TextMeshProUGUI quantityText;
    private InventorySlot slotData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        iconImage = transform.Find("Icon").GetComponent<Image>();
        quantityText = transform.Find("Amount").GetComponent<TextMeshProUGUI>();

        iconImage.enabled = false;
        quantityText.text = "";
    }
    public void SetSlot(InventorySlot slot)
    {
        slotData = slot;
        RefreshUI();
    }
    public void RefreshUI()
    {
        if (slotData == null || slotData.IsEmpty)
        {
            iconImage.enabled = false;
            quantityText.text = "";
        }
        else
        {
            iconImage.enabled = true;
            iconImage.sprite = slotData.item.icon;

            if (slotData.item.isStackable && slotData.quantity > 1)
            {
                quantityText.text = slotData.quantity.ToString();
            }
            else
            {
                quantityText.text = "";
            }
        }
    }
}
