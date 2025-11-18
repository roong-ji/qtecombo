using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    private bool _isGameOver = false;
    public bool IsGameOver => _isGameOver;

    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _line;

    private void Start()
    {
        Time.timeScale = 0f;
        _restartButton.SetActive(false);
        _line.SetActive(false);
    }

    public void GameOver()
    {
        _isGameOver = true;
        Invoke(nameof(ButtonOn), 3f);
        _line.SetActive(false);
    }

    public void ButtonOn()
    {
        _restartButton.SetActive(true);
    }

    public void GameStart()
    {
        Time.timeScale = 1f;
        _startButton.SetActive(false);
        _line.SetActive(true);
    }

    public void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
