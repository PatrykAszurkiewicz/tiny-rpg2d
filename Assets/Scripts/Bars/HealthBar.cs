using UnityEngine;

public class HealthBar : MonoBehaviour
{
    PlayerStats stats;
    public ResourceBarUI healthBarUI;

    private void Start()
    {
        stats = FindAnyObjectByType<PlayerStats>();
    }
    void Update()
    {
        if (stats != null && healthBarUI != null)
        {
            healthBarUI.SetValue(stats.currentHealth, stats.maxHealth);
        }
    }
}
