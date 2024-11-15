using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private float moveSpeed;
    private float xInput;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        
        HandleMovement();
        HandleAnimation();
        

    }

    private void HandleAnimation()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
    }
    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);


    }
}
