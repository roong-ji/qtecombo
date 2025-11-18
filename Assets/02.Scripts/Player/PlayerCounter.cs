using UnityEngine;

public class PlayerCounter : MonoBehaviour
{
    [Header("슬로우 모션 설정")]
    private float _lerpSpeed = 50f;
    private float _targetTimeScale = 1f;

    private Enemy _enemy;

    private void Update()
    {
        //HandleTimeScale();
    }

    public void Counter(Enemy enemy)
    {
        _enemy = enemy;
        //Time.timeScale = 0f;
    }

    public void CounterAttack()
    {
        if (_enemy == null) return;

        ScoreManager.Instance.AddScore(_enemy.DefaultScore);
        _enemy.TakeHit();
        _enemy.Knockback();
    }

    private void HandleTimeScale()
    {
        Time.timeScale = Mathf.Lerp(0f, _targetTimeScale, Time.unscaledDeltaTime * _lerpSpeed);
    }
}
