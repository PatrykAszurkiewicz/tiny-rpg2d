using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 1.5f;
    public float chestCloseDistance = 2.5f;

    public LayerMask interactLayer;
    private Collider2D currentChest;

    Chest openChest;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentChest = Physics2D.OverlapCircle(transform.position, interactRange, interactLayer);

        if(openChest != null && openChest.IsOpen)
        {
            float distance = Vector2.Distance(transform.position, openChest.transform.position);

            if(distance > chestCloseDistance)
            {
                openChest.CloseChest();
                openChest = null;
            }
        }
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (currentChest == null) return;
        Chest chest = currentChest.GetComponent<Chest>();
        if (chest == null) return;

        chest.OpenChest();

    }
}
