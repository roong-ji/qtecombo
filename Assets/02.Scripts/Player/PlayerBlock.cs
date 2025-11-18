using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    [Header("슬로우 모션 설정")]
    private float _slowSpeed = 50f;
    private float _targetTimeScale = 1f;

    private Enemy _enemy;

    private void Update()
    {
        HandleTimeScale();

    }

    public void Block(Enemy enemy)
    {
        _enemy = enemy;
        _targetTimeScale = 0f;
    }

    public void CounterAttack()
    {
        if (_enemy == null) return;

#if UNITY_EDITOR
        Debug.Log("COUNTER ATTACK!!!");
#endif

        ScoreManager.Instance.AddScore(_enemy.DefaultScore);
        _enemy.TakeHit();
        _enemy.Knockback();

        _targetTimeScale = 1f;
    }

    private void HandleTimeScale()
    {
        Time.timeScale = _targetTimeScale;
        if (Input.GetKeyDown(KeyCode.F)) { Time.timeScale = 0; Debug.Log(Time.timeScale); }
        if (Input.GetKeyDown(KeyCode.D)) { Time.timeScale = 1; Debug.Log(Time.timeScale); }
        //Time.timeScale = Mathf.Lerp(Time.timeScale, _targetTimeScale, Time.unscaledDeltaTime * _slowSpeed);
    }
}
