using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    private static SpeedManager _instance;
    public static SpeedManager Instance
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

    [Header("게임 속도")]
    [SerializeField] private float _speed;
    public float Speed => _speed;

    [SerializeField] private float _speedupInterval;
    [SerializeField] private float _speedIncrement;

    private float _timer;

    private void Start()
    {
        _timer = 0f;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer < _speedupInterval) return;
        SpeedUp();

        _timer = 0f;
    }

    private void SpeedUp()
    {
        _speed += _speedIncrement;
    }
}
