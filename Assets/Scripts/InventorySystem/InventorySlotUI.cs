using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI quantityText;
    private InventorySlot slotData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
