using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrashSlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI quantityText;

    private InventorySlot slotData;
    private InventorySlotUI slotUI;
    public void SetSlot(InventorySlot newSlot)
    {
        // Zniszcz stary przedmiot (jeœli by³)
        if (slotData != null && !slotData.IsEmpty)
        {
            Debug.Log($"[Trash] Zniszczono przedmiot: {slotData.item.name} x{slotData.quantity}");
            // Mo¿na tu dodaæ efekt graficzny/dŸwiêkowy
        }

        // Ustaw nowy slot jako kopiê
        slotData = new InventorySlot
        {
            item = newSlot.item,
            quantity = newSlot.quantity
        };

        slotUI.RefreshUI();
        slotData.Clear();
    }
    public InventorySlot GetSlotData()
    {
        return slotData;
    }

}
