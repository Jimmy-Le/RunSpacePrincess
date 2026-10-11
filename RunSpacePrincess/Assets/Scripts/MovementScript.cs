using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
	[SerializeField] public Animator animator;
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
	public float maxVelocity = -1f;
	public float decayPerSecond = 30f;

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

    void Update()
    {
        if(GameManager.instance.tutorialOn)
        		{
        			if (jump.IsPressed() || slam.IsPressed())
        			{
        				GameManager.instance.TurnOffTutorial();
        			}
        		}
    }


    // Update is called once per frame
    void FixedUpdate()
    {
		if(GameManager.instance.tutorialOn)
		{
			if (jump.IsPressed() || slam.IsPressed())
			{
				GameManager.instance.TurnOffTutorial();
			
			}
		}


        if (GameManager.instance.isPaused)
        {
			rb.linearVelocity = Vector2.zero;
			rb.angularVelocity = 0f;
			rb.bodyType = RigidbodyType2D.Kinematic;
            return;
        }
		else {
			rb.bodyType = RigidbodyType2D.Dynamic;
			if(rb.linearVelocity.x < maxVelocity)
			{
			  	rb.linearVelocityX = Mathf.MoveTowards(rb.linearVelocityX, 0f, decayPerSecond * Time.fixedDeltaTime);
			}	
		}
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) || Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, objectLayer);
		
		bool isFalling = rb.linearVelocity.y < -0.1f;
		animator.SetBool("isJumping", !isFalling);
		animator.SetBool("isFalling", isFalling);	
		


        if (isGrounded)
        {
            isSlamming = false;
			animator.SetBool("isFalling", false);
			animator.SetBool("isJumping", false);
        }
        
        if (jump.IsPressed() && !isSlamming)
        {
            Jump();
			animator.SetBool("isJumping", true);
			animator.SetBool("isFalling", false);
        }

        if (slam.IsPressed())
        {
			SoundManager.PlaySound(SoundManager.SoundType.Jump, 0.2f);
            isSlamming = true;
            Slam();
			animator.SetBool("isFalling", true);
			animator.SetBool("isJumping", false);
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
        rb.linearVelocity = new Vector2(0, speed);
    }
    
    public void Slam()
    {
        rb.linearVelocity = new Vector2(0, slamSpeed);
    }

	public void Death()
	{
		GameManager.instance.GameOver();
	}

	public void Win()
	{
		GameManager.instance.Win();
	}
}
