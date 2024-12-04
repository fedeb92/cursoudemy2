using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D cd;
    private bool canBeControlled = false;
    [Header("movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;
    private float defaultGravityScale;
    private bool canDoubleJump;

    [Header("BufferJump & CoyoteJump")]
    [SerializeField] private float bufferJumpWidow = .25f;
    private float bufferJumpActivated = -1;
    [SerializeField] private float coyoteJumpWidow = .5f;
    private float coyoteJumpActivated = -1;


    [Header("wall interaction")]
    [SerializeField] private float wallJumpDuration = .6f;
    [SerializeField] private Vector2 wallJumpForce;
    private bool isWallJumping;

    [Header("Knockback")]
    [SerializeField] private float knockbackDuration = 1;
    [SerializeField] private Vector2 knockbackPower;
    private bool isKnocked;

    [Header("collition")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;
    private bool isAirborne;
    private bool isWallDetected;

    private float xInput;
    private float yInput;
    private bool facingRight;
    private int facingDir = 1;

    [Header("VFX")]
    [SerializeField] private GameObject deathVFX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CapsuleCollider2D>();
        anim = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        defaultGravityScale = rb.gravityScale;
        RespawnFinsish(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
           {
            Knockback();
        }

        UpdateAirbornStatus();
        if(canBeControlled == false)
                return;
        if(isKnocked) {
        return;
        }
        HandleInput();
        HandleWallSlide();
        HandleMovement();
        HandleFlip();
        HandleCollision();
        HandleAnimation();
    }

    public void RespawnFinsish(bool finished)
    {
        
        if (finished)
        {
            rb.gravityScale = defaultGravityScale;
            canBeControlled = true;
            cd.enabled = true;
        }
        else 
            {
            rb.gravityScale = 0;
            canBeControlled = false;   
            cd.enabled = false; 
        }
    }

    public void Knockback()
    {
        if (isKnocked)
        {
            return;
        }

        StartCoroutine(KnockbackRoutine());
        anim.SetTrigger("knockback");
        rb.linearVelocity = new Vector2(knockbackPower.x * -facingDir, knockbackPower.y);
    }
    public void Die()
    {

    Destroy(gameObject);
        GameObject newDeathVFX = Instantiate(deathVFX,transform.position,Quaternion.identity);
    }
    

    
    private IEnumerator KnockbackRoutine()
    {
        isKnocked = true;
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }



    private void UpdateAirbornStatus()
    {
        if (isGrounded && isAirborne)
        {
            HandleLanding();
        }
        if (!isGrounded && !isAirborne)
        {
            BecomeAirBorne();
        }
    }

    private void BecomeAirBorne()
    {
        isAirborne = true;
        if (rb.linearVelocity.y < 0)
        {
            ActivateCoyoteJump();
        }
    }

    private void HandleLanding()
    {
        isAirborne = false;
        canDoubleJump = true;

        AttempBufferJump();
    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton();
            RequestBufferJump();
        }
    }

    #region Buffer & CoyoteJump
    private void RequestBufferJump()
    {
        if (isAirborne)
        {
            bufferJumpActivated = Time.time;
        }
    }

    private void AttempBufferJump()
    {
        if(Time.time < bufferJumpActivated + bufferJumpWidow)
        {
            bufferJumpActivated = Time.time -1;
            Jump();
        }
    }

    private void ActivateCoyoteJump()
    {
        coyoteJumpActivated = Time.time;
    }

    private void CancelCoyoteJump()
    {
        coyoteJumpActivated = Time.time -1;
    }
    #endregion
    private void JumpButton()
    {
        bool coyoteJumpAvalible = Time.time < coyoteJumpActivated + coyoteJumpWidow;

        if (isGrounded|| coyoteJumpAvalible)
        {
            Jump();
        }
        else if(isWallDetected && !isGrounded)
        {
            WallJump();
        }
        else if (canDoubleJump) 
        {
            DoubleJump();
        }

        CancelCoyoteJump();
    }
    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void DoubleJump()
    {
        isWallJumping = false;
        canDoubleJump = false;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, doubleJumpForce);
    }
    private void WallJump()
    {
        canDoubleJump = true;   
        rb.linearVelocity = new Vector2(wallJumpForce.x * -facingDir,wallJumpForce.y);
        Flip();
        StopAllCoroutines();
        StartCoroutine(WallJumpRoutine());
    }

    private IEnumerator WallJumpRoutine() 
    {
        isWallJumping = true ;
        yield return new WaitForSeconds(wallJumpDuration);
        isWallJumping = false ;
    }
    private void HandleWallSlide()
    {
        bool canWallSlide = isWallDetected && rb.linearVelocity.y < 0;
        float yModifer = yInput < 0 ? 1 : .05f;

        if (canWallSlide == false)
            return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * yModifer);
    }



private void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position,Vector2.right*facingDir,wallCheckDistance,whatIsGround);
    }
    private void HandleMovement()
    {
        if (isWallDetected)
        {
            return;
        }
        if(isWallJumping)
        {
            return;
        }

        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleFlip()
    {
        if(xInput > 0 && facingRight || xInput < 0 && !facingRight)        
        {
            Flip();
        }
    }


    private void Flip()
    {
        facingDir = facingDir * -1;
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }


    private void HandleAnimation()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("isGrounded",isGrounded);
        anim.SetBool("isWallDetected",isWallDetected);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x,transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x + (wallCheckDistance*facingDir),transform.position.y));
    }
}
