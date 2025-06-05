using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    private Image iconImage;
    private TextMeshProUGUI quantityText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        iconImage = transform.Find("Icon").GetComponent<Image>();
        quantityText = transform.Find("Amount").GetComponent<TextMeshProUGUI>();

        iconImage.enabled = false;
        quantityText.text = "";
    }
}
