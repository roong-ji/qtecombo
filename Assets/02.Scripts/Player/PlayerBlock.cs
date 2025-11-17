using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    private Enemy _enemy;

    public void Block(Enemy enemy)
    {
        _enemy = enemy;

        // 시간이 느려짐
        // 일정 시간 대기 후 복구 및 블락 상태 해제
    }

    public void BlockAttack()
    {
        if (_enemy == null) return;

#if UNITY_EDITOR
        Debug.Log("COUNTER ATTACK!!!");
#endif

        ScoreManager.Instance.AddScore(_enemy.DeafaultScore);

        _enemy.TakeHit();
    }
}
