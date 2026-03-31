using UnityEngine;
using UnityEngine.InputSystem;


public class AttackController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public int maxCombo = 3;
    private int currentCombo = 0;
    private float comboTimer = 0f;
    public float comboResetTime = 0.7f;
    public float attackSpeed = 1.0f;
    public float hitStopDuration = 0.05f;
    public float knockbackForce = 5f;
    private bool isGrounded = true;
    private bool attackPressed = false;

    void Update()
    {
        if (currentCombo > 0)
        {
            comboTimer += Time.deltaTime;
            if (comboTimer > comboResetTime)
            {
                currentCombo = 0;
                comboTimer = 0f;
            }

        }

        if (attackPressed)
        {
            Attack();
            attackPressed = false;
        }
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            attackPressed = true;
        }
    }

    void Attack()
    {
        if (isGrounded)
        {
            currentCombo++;

            if (currentCombo > maxCombo)
            {
                currentCombo = 1;
            }

            comboTimer = 0f;

            animator.SetFloat("attackSpeed", attackSpeed);
            animator.SetInteger("attackIndex", currentCombo);
            animator.SetTrigger("attack");
        }
        else
        {
            animator.SetFloat("attackSpeed", attackSpeed);
            animator.SetInteger("attackIndex", 1);
            animator.SetTrigger("airAttack");
        }
    }

    public void SetGrounded(bool grounded)
    {
        isGrounded = grounded;

        if (grounded)
        {
            currentCombo = 0;
            comboTimer = 0;
        }
    }

    public void ApplyKnockback()
    {
        rb.linearVelocity = new Vector2(knockbackForce * transform.localScale.x,
                                        rb.linearVelocity.y);
    }

    public void ApplyHitStop()
    {
        Time.timeScale = 0;
        Invoke(nameof(ResetHitStop), hitStopDuration);
    }

    void ResetHitStop()
    {
        Time.timeScale = 1f;
    }
}