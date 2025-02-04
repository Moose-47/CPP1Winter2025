using UnityEngine;

public class Lives : MonoBehaviour, Ipickups
{
    public void Pickup(PlayerController player)
    {
        player.lives++;
        Destroy(gameObject);
    }
}
