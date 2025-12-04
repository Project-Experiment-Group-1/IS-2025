using UnityEngine;

public class NPCBase : MonoBehaviour
{
    [Header("Base Settings ")]
    public float MoveSpeed = 3.0f;
    public float JumpForce = 10.0f; 
    public float DetectionRadius = 10.0f;
    
    [Header("Ground Detection ")]
    public LayerMask GroundLayer;
    public Transform GroundCheckPos; 
    public float GroundCheckRadius = 0.2f;

    [Header("Components")]
    public Rigidbody2D Rb;
    public Animator Animator;
    public SpriteRenderer SpriteRenderer;
    public Transform PlayerTransform;

    public bool IsGrounded { get; private set; }
    
   protected virtual void Start()
    {
        if (Rb == null) Rb = GetComponent<Rigidbody2D>();
        if (Animator == null) Animator = GetComponent<Animator>();
        if (SpriteRenderer == null) SpriteRenderer = GetComponent<SpriteRenderer>();

        // Playerを探す
        if (PlayerTransform == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p) PlayerTransform = p.transform;
        }

        // Auto generate GroundCheck Position
        if (GroundCheckPos == null)
        {
            GameObject checkObj = new GameObject("GroundCheck");
            checkObj.transform.parent = this.transform;
            checkObj.transform.localPosition = new Vector3(0, -0.5f, 0); // 假设脚底在中心下方0.5米
            GroundCheckPos = checkObj.transform;
        }
    }

    protected virtual void Update()
    {
        CheckGrounded();
        
        // Animation Update
        if (Animator) Animator.SetBool("IsGrounded", IsGrounded);
    }

    //Tool Functions

    /// <summary>
    /// 移動Function：目標座標に移動するFunction
    /// </summary>
    /// <param name="targetX">目標座標（Wrold Position）</param>
    /// <param name="speedMultiplier">速度の倍率（1.0はデーフォルト速度）</param>
    public void MoveTo(float targetX, float speedMultiplier = 1.0f)
    {
        // 方向：1 右, -1 左, 0 隣
        float distDiff = targetX - transform.position.x;
        
        if (Mathf.Abs(distDiff) < 0.1f)
        {
            StopMoving();
            return;
        }

        float directionX = Mathf.Sign(distDiff);
        float finalSpeed = MoveSpeed * speedMultiplier;

        Rb.linearVelocity = new Vector2(directionX * finalSpeed, Rb.linearVelocity.y);

        if (directionX != 0)
        {
            SpriteRenderer.flipX = directionX < 0; 
        }

        if (Animator) Animator.SetFloat("Speed", Mathf.Abs(finalSpeed));
    }

    public void StopMoving()
    {
        // Only clear X Velocity
        Rb.linearVelocity = new Vector2(0, Rb.linearVelocity.y);
        if (Animator) Animator.SetFloat("Speed", 0f);
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, JumpForce);
            if (Animator) Animator.SetTrigger("Jump");
        }
    }

    private void CheckGrounded()
    {
        bool hit = Physics2D.OverlapCircle(GroundCheckPos.position, GroundCheckRadius, GroundLayer);
        IsGrounded = hit;
    }
    
    public float GetHorizontalDistanceToPlayer()
    {
        if (PlayerTransform == null) return 9999f;
        return Mathf.Abs(transform.position.x - PlayerTransform.position.x);
    }

    public EmotionData GetEmotionData()
    {
        if (EmotionReceiver.Instance != null)
        {
            return EmotionReceiver.Instance.GetEmotion();
        }
        return new EmotionData();
    }

    public void SetAnimationBool(string paramName, bool value)
    {
        if (Animator != null)
        {
            Animator.SetBool(paramName, value);
        }
    }

    // Debug function : draw Detect range
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DetectionRadius);

        if (GroundCheckPos != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(GroundCheckPos.position, GroundCheckRadius);
        }
    }

}
