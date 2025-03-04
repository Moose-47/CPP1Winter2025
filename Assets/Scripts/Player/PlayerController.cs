using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

//Creates rigidbody2d attached to whatever this script is attached to and prevents removal of rigidbody2d from object.
//ReuireComponent can only contain 3 in a single statement, additonal lines would be required for more.
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(GroundCheck))]
public class PlayerController : MonoBehaviour
{
    /*public makes it available in unity to change from in engine and not just from script
      having [x] applies to the below var. In this case setting the range that the player speed can be.*/

    [Range(3, 10)]
    public float speed = 5.0f;
    [Range(5, 15)]
    public float jumpForce = 9.0f;
    private Coroutine speedChange = null;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private GroundCheck grChk;

    bool isGrounded = false;

// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Assigning unity components to variables.
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        grChk = GetComponent<GroundCheck>();
        rb.linearVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        animations();
        checkGrounded();

        if (canJump(true, anim, "shoot"))
        {
            jump();
        }

        if (canMove(anim, "shoot"))
        {
            move();
        }
   
        if (canFire(anim, "shoot"))
        {
            fire();
        }
    }


    void checkGrounded()
    {
        if (!isGrounded)
        {
            if (rb.linearVelocity.y <= 0.1) isGrounded = grChk.isGrounded();
        }
        else isGrounded = grChk.isGrounded();        
    }
    bool canJump(bool isGrounded, Animator anim, string shoot)
    {
        if (!isGrounded) { return false; }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("shoot")) { return false; }     
        return true;
    }
    void jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    bool canMove(Animator anim, string shoot)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(shoot)) { return false; }
        return true;
    }
    void move()
    {
        float hInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(hInput * speed, rb.linearVelocity.y);
        if (hInput != 0) sr.flipX = (hInput < 0);
        anim.SetFloat("speed", Mathf.Abs(hInput));
    }
    bool canFire(Animator anim, string shoot)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(shoot)){ return false; }
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName(shoot)) { rb.gravityScale = 1.5f; }
        return true;
    }
    void fire()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Fire");
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f;
        }
    }
    void animations()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isFalling", rb.linearVelocity.y < 0.1f);
        anim.SetBool("isDucking", Input.GetAxis("Vertical") < 0);
    }


    //PowerUP speed boost.
    public void SpeedChange()
    {
        if (speedChange != null)
        {
           StopCoroutine(speedChange);
           speed /= 2;          
        }
        speedChange = StartCoroutine(SpeedChangeCoroutine());
    }
    IEnumerator  SpeedChangeCoroutine()
    {
        //Do something immediately
        speed *= 2;

        yield return new WaitForSeconds(5.0f);

        //Do something after 5 seconds
        speed /= 2;
    }

    //Detect pickup
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ipickups pickup = collision.gameObject.GetComponent<Ipickups>();
        if (pickup != null) pickup.Pickup(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ipickups pickup = collision.GetComponent<Ipickups>();
        if (pickup != null) pickup.Pickup(this);

        if (collision.CompareTag("squish") && rb.linearVelocityY < 0)
        {
            collision.enabled = false;
            collision.gameObject.GetComponentInParent<Enemy>().takeDamage(9999, DamageType.JumpedOn);
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up * 6, ForceMode2D.Impulse);
        }
    }
}