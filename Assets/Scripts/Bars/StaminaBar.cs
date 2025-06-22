using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    private PlayerStats stats;
    public ResourceBarUI staminaBarUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stats = FindAnyObjectByType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats != null && staminaBarUI != null)
        {
            staminaBarUI.SetValue(stats.currentStamina, stats.maxStamina);
        }
    }
}
