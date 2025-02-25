using UnityEngine;

public class Score : MonoBehaviour, Ipickups
{
    public int addScore;
    public void Pickup(PlayerController player)
    {
        GameManager.Instance.score += addScore;
        Debug.Log(GameManager.Instance.score);
        Destroy(gameObject);
    }
}