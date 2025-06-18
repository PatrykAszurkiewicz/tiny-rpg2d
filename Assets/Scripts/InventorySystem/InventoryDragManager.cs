using UnityEngine;
using UnityEngine.UI;

public class InventoryDragManager : MonoBehaviour
{
    public static InventoryDragManager instance;
    public Image draggedIcon;
    private InventorySlot draggedSlot;
    private BaseSlotUI draggedSlotUI;
    public float sizeMult = 0.85f;

    [Header("Drop Item Settings")]
    public GameObject droppedItemPrefab;
    public Camera mainCamera;
    private Vector2 dragStartPos;
    private Vector2 dragEndPos;

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

    public void BeginDrag(InventorySlot slot, Sprite icon, BaseSlotUI slotUI)
    {
        draggedSlot = slot;
        draggedSlotUI = slotUI;
        draggedIcon.sprite = icon;
        draggedIcon.gameObject.SetActive(true);
        draggedIcon.rectTransform.localScale = Vector3.one * sizeMult;

        dragStartPos = Input.mousePosition;
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
        dragEndPos = Input.mousePosition;
    Vector2 direction = (dragEndPos - dragStartPos).normalized;

    Vector3 spawnPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    spawnPos.z = 0f;

    GameObject go = Instantiate(droppedItemPrefab, spawnPos, Quaternion.identity);

    var pickup = go.GetComponent<PickupItem>();
    pickup.itemData = draggedSlot.item;
    pickup.quantity = draggedSlot.quantity;

    // Dodaj si³ê do Rigidbody2D
    Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float forceAmount = 3f;
            rb.AddForce(direction * forceAmount, ForceMode2D.Impulse);
        }

        draggedSlot.Clear();

        if (draggedSlotUI != null)
            draggedSlotUI.RefreshUI();

        draggedIcon.gameObject.SetActive(false);
        draggedSlot = null;
        draggedSlotUI = null;
    }

    public InventorySlot GetDraggedSlot() => draggedSlot;
    public BaseSlotUI GetDraggedSlotUI() => draggedSlotUI;
    public InventorySlotUI GetDraggedInventorySlotUI() => draggedSlotUI as InventorySlotUI;
}
