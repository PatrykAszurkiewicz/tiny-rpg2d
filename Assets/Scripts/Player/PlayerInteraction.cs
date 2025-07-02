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
        if (context.performed)
        {
            currentChest.GetComponent<Chest>()?.OpenChest();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
