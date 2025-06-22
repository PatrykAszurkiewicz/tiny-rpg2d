using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Bars")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    public float maxMana = 50f;
    public float currentMana = 50f;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;

    [Header("Jumping")]
    public float jumpForce = 10f;
    public int maxJumps = 2;

    [Header("Stamina")]
    public float maxStamina = 5f;
    public float staminaRegenRate = 1f;
    public float staminaDrainRate = 1.5f;
    public float currentStamina = 5f;
}
