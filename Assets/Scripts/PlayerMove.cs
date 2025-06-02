using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 5f;
    float horizontalMovement = 0f;
    public float jumpForce = 10f;
    public int maxJumps = 2;
    private int jumpsLeft;
    bool isGrounded = true;
    //Sprint 
    public float speedMult = 1.5f;
    private bool isSprinting = false;
    public float maxStamina = 5f;              // maksymalna wytrzyma³oœæ w sekundach sprintu
    public float stamina;                      // aktualna wytrzyma³oœæ
    public float staminaRegenRate = 1f;        // ile wytrzyma³oœci wraca na sekundê
    public float staminaDrainRate = 1.5f;      // ile wytrzyma³oœci schodzi na sekundê

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stamina = maxStamina;
        jumpsLeft = maxJumps;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float currentSpeed = speed;
        bool isTryingToSprint = isSprinting && Mathf.Abs(horizontalMovement) > 0.01f && stamina > 0;
        if(isTryingToSprint)
        {
            currentSpeed *= speedMult;
            stamina -= staminaDrainRate * Time.fixedDeltaTime;
            if (stamina < 0.01f) stamina = 0f;
        }
        else
        {
            stamina += staminaRegenRate * Time.fixedDeltaTime;
            if (stamina > maxStamina) stamina = maxStamina;
        }

        rb.linearVelocity = new Vector2(horizontalMovement * currentSpeed, rb.linearVelocityY);

        if(horizontalMovement > 0.01f)
        {
            FlipRight();
        }
        else if(horizontalMovement < -0.01f)
        {
            FlipLeft();
        }

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
        if (context.performed && jumpsLeft > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
            jumpsLeft--;
            isGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpsLeft = maxJumps;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    public void Sprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValueAsButton();
    }
}
