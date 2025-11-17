using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    [Header("방어 범위")]
    [SerializeField] private Transform _attackBox;
    [SerializeField] private float _attackBoxLength;
    [SerializeField] private LayerMask _enemyLayer;

    private Enemy _enemy;

    public bool Block()
    {
        RaycastHit2D hit = Physics2D.Raycast(
        _attackBox.position,
        Vector2.right,
        _attackBoxLength,
        _enemyLayer
        );

        if (hit.collider == null) return false;

        _enemy = hit.collider.GetComponent<Enemy>();
        return true;
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
