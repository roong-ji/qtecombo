using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager _instance;
    public static TimeManager Instance
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

    private float _lerpTime;
    private float _targetTimeScale = 1f;
    private float _timer = 0f;

    private bool _slowMotion = false;

    private void Update()
    {
        HandleTimeScale();
    }

    public void SlowMotion(float time)
    {
        Time.timeScale = 0.5f;
        _lerpTime = time;
        _slowMotion = true;
    }

    private void HandleTimeScale()
    {
        if (_slowMotion == false) return;
        _timer += Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Lerp(0f, _targetTimeScale, _timer / _lerpTime);

        if (_timer < _lerpTime) return;
        _slowMotion = false;
        _timer = 0f;
        Time.timeScale = _targetTimeScale;
    }
}
