using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{

    [SerializeField] public InputActionAsset inputActions;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;
    
    [SerializeField] private LayerMask objectLayer;

    [SerializeField] public float xLimit;
    private InputAction jump;
    private InputAction slam;
    public float speed = 5f;
    public float slamSpeed = -25f;

    public bool isSlamming = false;
    public bool isGrounded = true;

    void Awake()
    {
        jump = inputActions.FindAction("Jump");
        slam = inputActions.FindAction("Crouch");
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        xLimit = transform.position.x;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.isPaused)
        {
            return;
        }
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) || Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, objectLayer);

        if (isGrounded)
        {
            isSlamming = false;
        }
        
        if (jump.IsPressed() && !isSlamming)
        {
            Jump();
        }

        if (slam.IsPressed())
        {
            isSlamming = true;
            Slam();
        }

        if (transform.position.x > xLimit)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = xLimit;
            transform.position = newPosition;
        }
        else if(transform.position.x < xLimit)
        {
            xLimit = transform.position.x;
        }
    }

    public void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed);
    }
    
    public void Slam()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, slamSpeed);
    }
}
