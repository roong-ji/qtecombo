using UnityEngine;

public class PlayerCounter : MonoBehaviour
{
    private Enemy _enemy;

    private float _lerpTime = 1f;

    public void Counter(Enemy enemy)
    {
        _enemy = enemy;
        TimeManager.Instance.SlowMotion(_lerpTime);
    }

    public void CounterAttack()
    {
        if (_enemy == null) return;

        ScoreManager.Instance.AddScore(_enemy.DefaultScore);
        _enemy.TakeHit();
        _enemy.Knockback();
    }

}
