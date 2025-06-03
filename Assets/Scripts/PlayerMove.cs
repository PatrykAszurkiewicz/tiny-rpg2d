using Unity.VisualScripting;
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
    //Ground check ------------------
    bool isGrounded = true;
    public Transform groundCheckPlace;
    public Vector2 groundCheckSize = new Vector2 (1f, 0.2f);
    public LayerMask groundLayers;
    //Sprint ------------------------
    public float speedMult = 1.5f;
    private bool isSprinting = false;
    public float maxStamina = 5f;              // max stamina
    public float stamina;                      // current stamina
    public float staminaRegenRate = 1f;        // stamina regen rate
    public float staminaDrainRate = 1.5f;      // stamina usage rate

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPlace.position, groundCheckSize);
    }
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
        isGrounded = Physics2D.OverlapBox(groundCheckPlace.position,groundCheckSize,0f,groundLayers);

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
        if (isGrounded)
        {
            jumpsLeft = maxJumps;
        }
        if (context.performed && jumpsLeft > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
            jumpsLeft--;
        }
    }
    public void Sprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValueAsButton();
    }
    
}
