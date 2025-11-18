using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class PlayerAttack : MonoBehaviour
{
    [Header("공격 범위")]
    [SerializeField] private Transform _attackBox;
    [SerializeField] private float _attackBoxLength;
    [SerializeField] private LayerMask _enemyLayer;

    private const float PERFECT_DISTANCE = 1.75f;
    private const float GREAT_DISTANCE = 1.25f;
    private const float GOOD_DISTANCE = 0.5f;
    
    public void Attack(EEnemyType enemyType)
    {
        RaycastHit2D hit = Physics2D.Raycast(
            _attackBox.position,
            Vector2.right,
            _attackBoxLength,
            _enemyLayer
            );

        if (hit.collider == null) return;

        Enemy enemy = hit.collider.GetComponent<Enemy>();

        if (enemy.CompareType(enemyType) == false) return;

        float distance = hit.distance;

#if UNITY_EDITOR
        //Debug.Log(distance);

        if (distance > PERFECT_DISTANCE) Debug.Log("PERFECT");
        else if (distance < GREAT_DISTANCE) Debug.Log("GREAT");
        else if (distance < GOOD_DISTANCE) Debug.Log("GOOD");
        else Debug.Log("BAD");
#endif

        ScoreManager.Instance.AddScore(distance * enemy.DeafaultScore);

        enemy.TakeHit();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);

        Gizmos.DrawCube(new Vector2(_attackBox.position.x + 1f, _attackBox.position.y), new Vector2(_attackBoxLength, 1f));
    }
}
