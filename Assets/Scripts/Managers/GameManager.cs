using System;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    public event Action<PlayerController> OnPlayerSpawned;
    public UnityEvent<int> OnLifeValueChanged;
#region Game Properties
    private int _score = 0;
    public int score
    {
        get => _score;
        set => _score = value;
    }

    [SerializeField] private int maxLives = 10;
    private int _lives = 5;
    public int lives
    {
        get => _lives;
        set
        {
            if (value <= 0)
            {
                gameOver();
                return;
            }
            if (_lives > value) Respawn();
            
            _lives = value;

            if (_lives > maxLives) _lives = maxLives;
            
            OnLifeValueChanged?.Invoke(_lives);
        }
    }
    #endregion
    #region Player Controller
    [SerializeField] private PlayerController playerPrefab;
    private PlayerController _playerInstance;
    public PlayerController PlayerInstance => _playerInstance;
    #endregion
    private Transform currentCheckpoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(this);
            return;
        }
        Destroy(gameObject);
    }
    private void Start()
    {
        if (lives <= 0) lives = 5;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string sceneName = (SceneManager.GetActiveScene().name.Contains("Level")) ? "Title" : "Level";
            SceneManager.LoadScene(sceneName);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        { score++; Debug.Log(_score); }

        if (Input.GetKeyDown(KeyCode.Escape) &&  SceneManager.GetActiveScene().name == "GameOver")
            SceneManager.LoadScene("Title");       
    }
    void gameOver()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOver");
        Debug.Log("Game Over goes here");
        lives = 5;
        score = 0;
    }
    void Respawn()
    {
        _playerInstance.transform.position = currentCheckpoint.position;
    }
    public void InstantiatePlayer(Transform spawnLocation)
    {
        _playerInstance = Instantiate(playerPrefab, spawnLocation.position.normalized, Quaternion.identity);
        currentCheckpoint = spawnLocation;
        OnPlayerSpawned?.Invoke(_playerInstance);
    }
    public void UpdateCheckpoint(Transform updatedCheckpoint)
    {
        currentCheckpoint = updatedCheckpoint;
        Debug.Log("Checkpoint updated");
    }
    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
