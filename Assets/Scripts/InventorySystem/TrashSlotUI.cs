using UnityEngine;
using UnityEngine.EventSystems;

public class TrashSlotUI : BaseSlotUI, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var draggedSlot = InventoryDragManager.instance.GetDraggedSlot();
        var draggedUI = InventoryDragManager.instance.GetDraggedSlotUI();

        if (draggedSlot == null || draggedSlot.IsEmpty) return;

        // Usuñ stary przedmiot
        if (!slotData.IsEmpty)
        {
            slotData.Clear();
        }

        // Przenieœ nowy przedmiot
        slotData.item = draggedSlot.item;
        slotData.quantity = draggedSlot.quantity;

        draggedSlot.Clear();
        draggedUI?.RefreshUI();

        RefreshUI();
        InventoryDragManager.instance.EndDrag();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slotData == null || slotData.IsEmpty) return;
        InventoryDragManager.instance.BeginDrag(slotData, iconImage.sprite, this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        InventoryDragManager.instance.Drag(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!RectTransformUtility.RectangleContainsScreenPoint(
            GetComponent<RectTransform>(), Input.mousePosition, null))
        {
            InventoryDragManager.instance.EndDrag();
        }
    }
}
