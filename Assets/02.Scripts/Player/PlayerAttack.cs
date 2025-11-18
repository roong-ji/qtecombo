using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("공격 범위")]
    [SerializeField] private Transform _attackBox;
    [SerializeField] private float _attackBoxLength;
    [SerializeField] private Vector2 _attackBoxSize;
    [SerializeField] private LayerMask _enemyLayer;

    private const float PERFECT_DISTANCE = 1.5f;
    private const float GREAT_DISTANCE = 1f;
    private const float GOOD_DISTANCE = 0.25f;

    [SerializeField] private JudgeManager _judgeManager;

    public bool Attack(EEnemyType enemyType)
    {
        RaycastHit2D hit = Physics2D.Raycast(
            _attackBox.position,
            Vector2.right,
            _attackBoxLength,
            _enemyLayer
            );

        if (hit.collider == null) return false;

        Enemy enemy = hit.collider.GetComponent<Enemy>();

        if (enemy.CompareType(enemyType) == false) return false;

        float distance = hit.distance;

        if (distance > PERFECT_DISTANCE)
        {
            _judgeManager.Judge(EJudgeType.Perfect);
        }
        else if (distance > GREAT_DISTANCE)
        {
            _judgeManager.Judge(EJudgeType.Great);
        }
        else if (distance > GOOD_DISTANCE)
        {
            _judgeManager.Judge(EJudgeType.Good);
        }
        else
        {
            _judgeManager.Judge(EJudgeType.Bad);
        }

      
        ScoreManager.Instance.AddScore(distance * enemy.DefaultScore);

        enemy.TakeHit();

        return true;
    }

    public void CounterAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(
            _attackBox.position,
            _attackBoxSize,
            0f,
            _enemyLayer
            );

        foreach (var target in enemies)
        {
            Enemy enemy = target.GetComponent<Enemy>();
            ScoreManager.Instance.AddScore(enemy.DefaultScore);
            enemy.TakeHit();
        }

        _judgeManager.Judge(EJudgeType.Perfect);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);

        Gizmos.DrawCube(new Vector2(_attackBox.position.x + 1f, _attackBox.position.y), new Vector2(_attackBoxLength, 1f));

        Gizmos.DrawWireCube(_attackBox.position, _attackBoxSize);
    }
}
