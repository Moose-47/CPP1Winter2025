using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.UpdateCheckpoint(transform);
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
        }
    }
}
