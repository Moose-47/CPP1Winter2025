using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField, Range(1, 20)] private float lifetime = 5.0f;
    [SerializeField, Range(1, 20)] private int damage = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime); 
    }

    public void SetVelocity(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().linearVelocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy e = collision.gameObject.GetComponent<Koopa>();
        if (e != null)
        {
            e.takeDamage(damage);
            Destroy(gameObject);
        }
        Enemy k = collision.gameObject.GetComponent<Kamek>();
        if (k != null)
        {
            k.takeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("Ground"))
             Destroy(gameObject);
        
        if (collision.collider.CompareTag("Player"))
            Destroy(gameObject);
    }
}
