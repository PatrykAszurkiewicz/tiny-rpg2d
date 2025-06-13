using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public ItemData itemData;
    public int quantity = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var inventoryManager = Object.FindFirstObjectByType<InventoryManager>();
            if (inventoryManager != null)
            {
                Debug.Log("Dzia³a");
                inventoryManager.AddItem(itemData, quantity);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("InventoryManager not found!");
            }
        }
    }
}
