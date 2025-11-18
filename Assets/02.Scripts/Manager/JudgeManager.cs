using UnityEngine;

public enum EJudgeType
{
    Perfect = 0,
    Great = 1,
    Good = 2,
    Bad = 3,
}

public class JudgeManager : MonoBehaviour
{
    private Vector2 _originScale;
    private Vector2 _targetScale;

    private float _timer;
    private const float LERP_TIME = 0.2f;

    private const float MIN_SCALE = 0.2f;

    private float _lostTime = 1f;

    [SerializeField] private Sprite[] _judges;
    private SpriteRenderer _spriteRenderer;


    private void Awake()
    {
        _originScale = transform.localScale;
        _targetScale = Vector3.one * MIN_SCALE;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        transform.localScale = _targetScale;
        _timer = 0f;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        transform.localScale = Vector3.Lerp(_targetScale, _originScale, _timer / LERP_TIME);

        if (_timer < _lostTime) return;
        gameObject.SetActive(false);
    }

    public void Judge(EJudgeType judge)
    {
        Init();
        _spriteRenderer.sprite = _judges[(int)judge];
        gameObject.SetActive(true);
    }
}
