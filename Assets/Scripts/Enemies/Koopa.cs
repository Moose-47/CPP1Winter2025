using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Koopa : Enemy
{
    Rigidbody2D rb;

    [SerializeField] private float xVel;
    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        if (xVel <= 0) xVel = 3;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        if (curPlayingClips[0].clip.name.Contains("walk"))
            rb.linearVelocity = (sr.flipX) ? new Vector2(-xVel, rb.linearVelocityY) : new Vector2(xVel, rb.linearVelocityY);
        else rb.linearVelocityX = 0.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("barrier"))
        {
            anim.SetTrigger("Turn");
            sr.flipX = !sr.flipX;
        }
    }

    public override void takeDamage(int DamageValue, DamageType damageType = DamageType.Default)
    {
        if (damageType == DamageType.JumpedOn)
        {
            anim.SetTrigger("Squish");
            Destroy(transform.parent.gameObject, 0.5f);
            return;
        }

        base.takeDamage(DamageValue, damageType);       
    }
}
