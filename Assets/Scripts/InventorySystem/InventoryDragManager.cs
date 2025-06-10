using UnityEngine;
using UnityEngine.UI;

public class InventoryDragManager : MonoBehaviour
{
    public static InventoryDragManager instance;
    public Image draggedIcon;
    private InventorySlot draggedSlot;
    public float sizeMult = 0.85f;

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

    public void BeginDrag(InventorySlot slot, Sprite icon)
    {
        draggedSlot = slot;
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
        draggedIcon.gameObject.SetActive(false);
    }

    public InventorySlot GetDraggedSlot() => draggedSlot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
