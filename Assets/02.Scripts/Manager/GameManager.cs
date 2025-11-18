using UnityEngine;

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

    public void GameOver()
    {
        _isGameOver = true;
    }
}
