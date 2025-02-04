using UnityEngine;

public class PowerUp : MonoBehaviour, Ipickups
{

    public void Pickup(PlayerController player)
    {
        player.SpeedChange();
        Destroy(gameObject);
    }
}