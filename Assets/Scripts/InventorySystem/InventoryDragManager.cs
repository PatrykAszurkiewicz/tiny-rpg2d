using UnityEngine;
using UnityEngine.UI;

public class InventoryDragManager : MonoBehaviour
{
    public static InventoryDragManager instance;
    public Image draggedIcon;
    private InventorySlot draggedSlot;
    private InventorySlotUI draggedSlotUI;
    public float sizeMult = 0.85f;

    [Header("Drop Item Settings")]
    public GameObject droppedItemPrefab;
    public Camera mainCamera;

    private void Awake() //singleton
    {
        if (instance == null)
        {
        instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        draggedIcon.gameObject.SetActive(false);
    }

    public void BeginDrag(InventorySlot slot, Sprite icon, InventorySlotUI slotUI)
    {
        draggedSlot = slot;
        draggedSlotUI = slotUI;
        draggedIcon.sprite = icon;
        draggedIcon.gameObject.SetActive(true);
        draggedIcon.rectTransform.localScale = Vector3.one * sizeMult;
    }

    public void Drag(Vector2 position)
    {
        draggedIcon.rectTransform.position = position;
    }
    public void EndDrag()
    {
        draggedSlot = null;
        draggedSlotUI = null;
        draggedIcon.gameObject.SetActive(false);
    }
    public void DropItemToWorld()
    {
        if(draggedSlot == null || draggedSlot.IsEmpty)
        {
            return;
        }
        Vector3 dropPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        dropPosition.z = 0f;

        GameObject go = Instantiate(droppedItemPrefab, dropPosition, Quaternion.identity);
        var pickup = go.GetComponent<PickupItem>();
        pickup.itemData = draggedSlot.item;
        pickup.quantity = draggedSlot.quantity;

        draggedSlot.Clear();
        draggedIcon.gameObject.SetActive(false);
        draggedSlot = null;
        draggedSlotUI = null;
    }

    public InventorySlot GetDraggedSlot() => draggedSlot;
    public InventorySlotUI GetDraggedSlotUI() => draggedSlotUI;
}
