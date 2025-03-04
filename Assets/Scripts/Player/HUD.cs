using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HUD : MonoBehaviour
{
    public TMP_Text livesTxt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateLivesDisplay(GameManager.Instance.lives);

        GameManager.Instance.OnLifeValueChanged.AddListener(UpdateLivesDisplay);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLifeValueChanged.RemoveListener(UpdateLivesDisplay);
    }

    private void UpdateLivesDisplay(int lives)
    {
        livesTxt.text = "Lives: " + lives;
    }
}
