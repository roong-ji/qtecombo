using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance
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

    [SerializeField] private Text _scoreTextUI;

    private float _score;
    private float _textScore;

    private float _timer;
    private const float LERP_TIME = 1f;

    private const float MAX_SCALE = 1.5f;
    private readonly Vector3 _originScale = Vector3.one;

    private void Start()
    {
        InitScore();
    }
    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > LERP_TIME) return;
        LerpScore();
    }
    private void LerpScore()
    {
        _textScore = Mathf.Lerp(_textScore, _score, _timer / LERP_TIME);
        _scoreTextUI.text = $"Score : {Mathf.RoundToInt(_textScore)}";

        _scoreTextUI.rectTransform.localScale = Vector3.Lerp(
            _scoreTextUI.rectTransform.localScale,
            _originScale,
            _timer / LERP_TIME
        );
    }

    private void InitScore()
    {
        _score = 0;
        _textScore = 0;
        _timer = 0f;
    }

    public void AddScore(float score)
    {
        _timer = 0f;
        _score += score;
        _scoreTextUI.rectTransform.localScale = _originScale * MAX_SCALE;
    }
}
