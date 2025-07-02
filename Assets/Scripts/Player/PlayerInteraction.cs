using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 1.5f;
    public LayerMask interactLayer;
    private Collider2D currentChest;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentChest = Physics2D.OverlapCircle(transform.position, interactRange, interactLayer);

        /*
        if (currentChest != null)
        {
            Debug.Log("Wykryto obiekt: " + currentChest.name);
        }
        */
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (currentChest == null)
        {
            //Debug.Log("Brak skrzynki w zasi�gu.");
            return;
        }

        Chest chest = currentChest.GetComponent<Chest>();
        if (chest == null)
        {
            //Debug.LogWarning("Wykryty obiekt nie ma komponentu Chest.");
            return;
        }

        chest.OpenChest();
    }
}
