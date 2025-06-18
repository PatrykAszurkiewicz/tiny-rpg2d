using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseSlotUI : MonoBehaviour
{
    [SerializeField] protected Image iconImage;
    [SerializeField] protected TextMeshProUGUI quantityText;

    protected InventorySlot slotData = new InventorySlot();

    public virtual void SetSlot(InventorySlot slot)
    {
        slotData = slot;
        RefreshUI();
    }

    public virtual void RefreshUI()
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
                quantityText.text = slotData.quantity.ToString();
            else
                quantityText.text = "";
        }
    }

    public InventorySlot GetSlotData() => slotData;
}
