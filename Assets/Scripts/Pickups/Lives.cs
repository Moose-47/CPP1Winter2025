using UnityEngine;

public class Lives : MonoBehaviour, Ipickups
{
    public void Pickup(PlayerController player)
    {
        GameManager.Instance.lives++;
        Destroy(gameObject);
    }
}
