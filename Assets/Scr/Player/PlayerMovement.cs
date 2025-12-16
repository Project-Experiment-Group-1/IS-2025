using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    private Vector2 moveInput;
    private AttackController attackController;
    public float speed = 5f;
    public float jumpForce = 5f;
    private bool jumpPressed = false;
    private bool isGrounded = false;

    private int jumpCount = 0;
    public int maxJumpCount = 2;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("OnMove");
    }



    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpPressed = true;
        }
        Debug.Log("OnJump");
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackController = GetComponent<AttackController>();
        animator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        Move();
        Jump();

    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
        if (moveInput.x != 0)
        {
            transform.localScale = new Vector3(
                Mathf.Sign(moveInput.x),
                1,
                1
            );
        }
    }

    void Jump()
    {
        if (jumpPressed && jumpCount < maxJumpCount)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
            jumpPressed = false;
        }
    }


    void Update()
    {
       // animator.SetBool("isRunning", Mathf.Abs(moveInput.x) > 0.1f);
      //  animator.SetBool("isJumping", !isGrounded);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}