using UnityEngine;

public class ManaBar : MonoBehaviour
{
    private PlayerStats stats;
    public ResourceBarUI manaBarUI;

    void Start()
    {
        stats = FindAnyObjectByType<PlayerStats>();
    }

    void Update()
    {
        if (stats != null && manaBarUI != null)
        {
            manaBarUI.SetValue(stats.currentMana, stats.maxMana);
        }
    }
}
