using UnityEngine;

public class Score : MonoBehaviour, Ipickups
{
    public int addScore;
    public void Pickup(PlayerController player)
    {
        player.score += addScore;
        Destroy(gameObject);
    }
}