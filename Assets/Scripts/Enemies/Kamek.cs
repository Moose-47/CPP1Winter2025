using UnityEngine;

public class Kamek : Enemy
{
    [SerializeField] private float fireRate = 2.0f;
    [SerializeField] private float atkRange = 5.0f;
    private float timeSinceLastFire = 0;
    private Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        if (fireRate <= 0) fireRate = 2.0f;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj) player = playerObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        facePlayer();

        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        if (curPlayingClips[0].clip.name.Contains("idle")) checkFire();
    }
    void facePlayer()
    {
        if (player == null) return;

        if (player.position.x < transform.position.x)
            sr.flipX = true;
        else
            sr.flipX= false;
    }
    void checkFire()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= atkRange && Time.time >= timeSinceLastFire + fireRate)
        {
            anim.SetTrigger("fire");
            timeSinceLastFire = Time.time;
        }
    }
    public override void takeDamage(int DamageValue, DamageType damageType = DamageType.Default)
    {
        if (damageType == DamageType.JumpedOn)
        {
            anim.SetTrigger("Squish");
            Destroy(transform.gameObject, 0.5f);
            return;
        }
        base.takeDamage(DamageValue, damageType);
    }
}
