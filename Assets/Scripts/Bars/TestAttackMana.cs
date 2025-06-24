using UnityEngine;

public class TestAttackMana : MonoBehaviour
{
    PlayerStats stats;

    private void Start()
    {
        stats = FindAnyObjectByType<PlayerStats>();
    }
    public void TestDmg()
    {
        stats.TakeDamage(10);
    }
    public void TestUseMana()
    {
        stats.UseMana(10);
    }
}
