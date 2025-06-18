using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TrashSlotUI : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI quantityText;

    private InventorySlot slotData = new InventorySlot();

    private void RefreshUI()
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
            quantityText.text = slotData.item.isStackable && slotData.quantity > 1 ? slotData.quantity.ToString() : "";
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        var draggedSlot = InventoryDragManager.instance.GetDraggedSlot();
        var draggedUI = InventoryDragManager.instance.GetDraggedSlotUI();

        if (draggedSlot == null || draggedSlot.IsEmpty) return;

        if (!slotData.IsEmpty)
        {
            slotData.Clear();
        }

        // Przenieœ nowy przedmiot do trash slotu
        slotData.item = draggedSlot.item;
        slotData.quantity = draggedSlot.quantity;

        // Wyczyœæ Ÿród³owy slot
        draggedSlot.Clear();
        if (draggedUI != null) draggedUI.RefreshUI();

        RefreshUI();
        InventoryDragManager.instance.EndDrag();
    }

}
