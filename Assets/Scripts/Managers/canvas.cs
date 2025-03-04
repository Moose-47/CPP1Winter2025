using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class canvas : MonoBehaviour
{
    [Header("Buttons")]
    public Button startBtn;
    public Button settingsBtn;
    public Button backBtn;

    public Button quitBtn;
    public Button resumeBtn;
    public Button mainMenuBtn;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;

    [Header("Text")]
    public TMP_Text volSlideTxt;
    public TMP_Text livesTxt;

    [Header("Slider")]
    public Slider volSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (startBtn) startBtn.onClick.AddListener(() => SceneManager.LoadScene("Level"));
        if (settingsBtn) settingsBtn.onClick.AddListener(() => SetMenus(settingsMenu, mainMenu));
        if (backBtn) backBtn.onClick.AddListener(() => SetMenus(mainMenu, settingsMenu));
        if (quitBtn) quitBtn.onClick.AddListener(QuitGame);
        if (resumeBtn) resumeBtn.onClick.AddListener(() => SetMenus(null, pauseMenu));
        if (mainMenuBtn) mainMenuBtn.onClick.AddListener(() => SceneManager.LoadScene("Title"));
        if (volSlider)
        {
            volSlider.onValueChanged.AddListener(OnSliderValueChanged);
            OnSliderValueChanged(volSlider.value);
        }
        if (livesTxt)
        {
            GameManager.Instance.OnLifeValueChanged.AddListener(OnLifeValueChanged);
            OnLifeValueChanged(GameManager.Instance.lives);
        }
    }
    private void OnLifeValueChanged(int value) => livesTxt.text = $"Lives: {GameManager.Instance.lives}";
    private void SetMenus(GameObject menuToActivate, GameObject menuToDeactivate)
    {
        if (menuToActivate) menuToActivate.SetActive(true);
        if (menuToDeactivate) menuToDeactivate.SetActive(false);
    }
    private void OnSliderValueChanged(float value)
    {
        float roundedValue = Mathf.Round(value * 100);
        if (volSlideTxt) volSlideTxt.text = $"{roundedValue}%";
    }

    private void OnDisable()
    {
        if (startBtn) startBtn.onClick.RemoveAllListeners();
        if (settingsBtn) settingsBtn.onClick.RemoveAllListeners();
        if (backBtn) backBtn.onClick.RemoveAllListeners();
        if (quitBtn) quitBtn.onClick.RemoveAllListeners();
        if (resumeBtn) resumeBtn.onClick.RemoveAllListeners();
        if (mainMenuBtn) mainMenuBtn.onClick.RemoveAllListeners();
    }
   
    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
   
    
    // Update is called once per frame
    void Update()
    {
        if (livesTxt) livesTxt.text = $"Lives: {GameManager.Instance.lives}";
    }
}
