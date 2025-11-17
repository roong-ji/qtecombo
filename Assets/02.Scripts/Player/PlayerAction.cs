using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    
    [Header("점프력")]
    [SerializeField] private float _jumpForce;

    [Header("공격 범위")]
    [SerializeField] private Transform _attackBox;
    [SerializeField] private float _attackBoxLength;
    [SerializeField] private LayerMask _enemyLayer;

    private const float PERFECT_DISTANCE = 1.75f;
    private const float GREAT_DISTANCE = 1.25f;
    private const float GOOD_DISTANCE = 0.5f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            _attackBox.position,
            Vector2.right,
            _attackBoxLength,
            _enemyLayer
            );

        if (hit.collider == null) return;

        EnemyController enemy = hit.collider.GetComponent<EnemyController>();

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

    public void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    public void Block()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);

        Gizmos.DrawCube(new Vector2(_attackBox.position.x+1f, _attackBox.position.y), new Vector2(_attackBoxLength, 1f));
    }
}
