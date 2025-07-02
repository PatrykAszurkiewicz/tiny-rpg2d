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
        DetectChest();
        /*
        if (currentChest != null)
        {
            Debug.Log("Wykryto obiekt: " + currentChest.name);
        }
        */
    }
    public void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Klawisz E wciœniêty");

        DetectChest(); // wymuszone sprawdzenie zasiêgu

        if (currentChest == null)
        {
            Debug.LogWarning("Brak wykrytego obiektu w momencie interakcji.");
            return;
        }

        if (context.performed)
        {
            var chest = currentChest.GetComponent<Chest>();
            if (chest == null)
            {
                Debug.LogWarning("Brak komponentu Chest na obiekcie: " + currentChest.name);
                return;
            }

            Debug.Log("Otwieram skrzynkê: " + currentChest.name);
            chest.OpenChest();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
    private void DetectChest()
    {
        currentChest = Physics2D.OverlapCircle(transform.position, interactRange, interactLayer);
    }
}
