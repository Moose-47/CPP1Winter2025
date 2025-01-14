using UnityEngine;

//Creates rigidbody2d attached to whatever this script is attached to and prevents removal of rigidbody2d from object.
//ReuireComponent can only contain 3 in a single statement, additonal lines would be required for more.
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
/*public makes it available in unity to change from in engine and not just from script
having [x] applies to the below var. In this case setting the range that the player speed can be.*/
    [Range(3, 10)]
    public float speed = 5.0f;
    public float jumpForce = 5.0f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    //Assigning unity Rigidbody2D to var.
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement for left and right.
        float hInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(hInput * speed, rb.linearVelocity.y);
    }
}
