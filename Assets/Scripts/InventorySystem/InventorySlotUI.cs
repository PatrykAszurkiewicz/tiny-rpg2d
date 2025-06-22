using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : BaseSlotUI, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
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
             InventoryDragManager.instance.inventoryArea, Input.mousePosition, null))
        {
            InventoryDragManager.instance.DropItemToWorld();
        }
        else
        {
            InventoryDragManager.instance.EndDrag();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        var draggedSlot = InventoryDragManager.instance.GetDraggedSlot();
        var draggedUI = InventoryDragManager.instance.GetDraggedSlotUI();

        if (draggedSlot == null || draggedSlot == slotData) return;

        (slotData.item, draggedSlot.item) = (draggedSlot.item, slotData.item);
        (slotData.quantity, draggedSlot.quantity) = (draggedSlot.quantity, slotData.quantity);

        RefreshUI();
        draggedUI.RefreshUI();

        InventoryDragManager.instance.EndDrag();
    }
}
