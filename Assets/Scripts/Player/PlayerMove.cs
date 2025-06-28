using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerStats stats;

    private float horizontalMovement = 0f;
    private int jumpsLeft;
    private bool isGrounded = true;
    private bool isSprinting = false;

    [Header("Ground check")]
    public Transform groundCheckPlace;
    public Vector2 groundCheckSize = new Vector2(1f, 0.2f);
    public LayerMask groundLayers;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPlace.position, groundCheckSize);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();
        stats.currentStamina = stats.maxStamina;
        jumpsLeft = stats.maxJumps;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheckPlace.position, groundCheckSize, 0f, groundLayers);

        float currentSpeed = stats.moveSpeed;
        bool isTryingToSprint = isSprinting && Mathf.Abs(horizontalMovement) > 0.01f && stats.currentStamina > 0;

        if (isTryingToSprint)
        {
            currentSpeed *= stats.sprintMultiplier;
            stats.currentStamina -= stats.staminaDrainRate * Time.fixedDeltaTime;
            stats.currentStamina = Mathf.Max(stats.currentStamina, 0f);
        }
        else
        {
            stats.currentStamina += stats.staminaRegenRate * Time.fixedDeltaTime;
            stats.currentStamina = Mathf.Min(stats.currentStamina, stats.maxStamina);
        }

        rb.linearVelocity = new Vector2(horizontalMovement * currentSpeed, rb.linearVelocity.y);

        if (horizontalMovement > 0.01f)
            FlipRight();
        else if (horizontalMovement < -0.01f)
            FlipLeft();
    }
    public void FlipRight()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
    public void FlipLeft()
    {
        transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            jumpsLeft = stats.maxJumps;
        }
        if (context.performed && jumpsLeft > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, stats.jumpForce);
            jumpsLeft--;
        }
    }
    public void Sprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValueAsButton();
    }
    
}
