using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 5f;
    float horizontalMovement = 0f;
    public float jumpForce = 10f;
    bool isGrounded = true;
    bool lastRight = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * speed, rb.linearVelocityY);

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
        if (context.performed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
            isGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Zak³adamy, ¿e wszystko co ma Collider to ziemia – mo¿na to rozwin¹æ
        isGrounded = true;
    }
}
