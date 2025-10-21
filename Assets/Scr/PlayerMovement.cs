using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    private Vector2 moveInput;
    public float speed = 5f;
    public float jumpForce = 5f;
    private bool jumpPressed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        float velox = speed * moveInput.x;

        if (velox != 0 )
        {
            rb.linearVelocity = new Vector3(velox, rb.linearVelocity.y, 0f);
            Debug.Log("Moving");
        }
        Jump();
    }

    void Jump()
    {
        if (jumpPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpPressed = false;
        }
    }
}
