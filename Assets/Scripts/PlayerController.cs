using UnityEngine;

//Creates rigidbody2d attached to whatever this script is attached to and prevents removal of rigidbody2d from object.
//ReuireComponent can only contain 3 in a single statement, additonal lines would be required for more.
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
/*public makes it available in unity to change from in engine and not just from script
  having [x] applies to the below var. In this case setting the range that the player speed can be.*/
    [Range(3, 10)]
    public float speed = 5.0f;
    [Range(5, 15)]
    public float jumpForce = 9.0f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    //groundCheck variables
    [Range(0.01f, 1.0f)]
    private Vector2 groundCheckSize = new Vector2(0.8f, 0.04f);
    public LayerMask isGroundLayer;

    bool isGrounded = false;

    private Transform groundCheck;

// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Assigning unity components to variables.
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        rb.linearVelocity = Vector3.zero;

        //groundCheck init
        GameObject newGameObject = new GameObject();
        newGameObject.transform.SetParent(transform); //sets the position to the parents position
        newGameObject.transform.localPosition = Vector3.zero;
        newGameObject.name = "GroundCheck";
        groundCheck = newGameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //isGrounded
        checkGrounded();
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        //Movement for left and right.
        float hInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(hInput * speed, rb.linearVelocity.y);

        /*Sprite flip- "sr.flipX" implies true, "!sr.flipX" implies false
                if (hInput > 0 && sr.flipX || hInput < 0 && !sr.flipX) sr.flipX = !sr.flipX;*/

        //Another way to flip sprite
        if (hInput != 0) sr.flipX = (hInput < 0);

        /*Jump
        if (Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); 
        }*/

//Another way to do jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        //Animation tags
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("speed", Mathf.Abs(hInput));
        anim.SetBool("isFalling", rb.linearVelocity.y < -0.1f);
        anim.SetBool("isDucking", Input.GetButton("Vertical"));
    }
    void checkGrounded()
    {
        if (!isGrounded)
        {
            if (rb.linearVelocity.y <= 0) isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, isGroundLayer);
        }
            else  isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, isGroundLayer);
    }
    
}