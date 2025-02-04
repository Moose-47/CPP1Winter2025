using UnityEngine;
//this script is fine for a small scope- pickups that remain fairly similar in their usecase- but anything more than 10 pickups and varied mechanics for the pickups,
//will probably require a different solution.
public class Pickups : MonoBehaviour
{
    public enum PickupType
    {
        Life,
        Powerup,
        Score,
    }

    public PickupType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();

            switch (type)
            {
                case PickupType.Life:
                    pc.lives++;//Option1
                    pc.SetLives(pc.GetLives() + 1);//Option2
                    break;
                case PickupType.Powerup:
                    pc.SpeedChange();
                    break;
                case PickupType.Score:
                    pc.score += 10;
                    break;
            }
            Destroy(gameObject);
        }
    }
}