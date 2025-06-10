using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
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
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slotData == null || slotData.IsEmpty)
            return;

        InventoryDragManager.instance.BeginDrag(slotData, iconImage.sprite);
    }
    public void OnDrag(PointerEventData eventData)
    {
        InventoryDragManager.instance.Drag(Input.mousePosition);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        InventoryDragManager.instance.EndDrag();
    }
    public void OnDrop(PointerEventData eventData)
    {
        var draggedSlot = InventoryDragManager.instance.GetDraggedSlot();

        // Brak przeci¹gania? Nic nie rób
        if (draggedSlot == null || draggedSlot == slotData)
            return;

        // Zamiana itemów miêdzy slotami
        (slotData.item, draggedSlot.item) = (draggedSlot.item, slotData.item);
        (slotData.quantity, draggedSlot.quantity) = (draggedSlot.quantity, slotData.quantity);

        RefreshUI();
        InventoryDragManager.instance.EndDrag();
    }
}
