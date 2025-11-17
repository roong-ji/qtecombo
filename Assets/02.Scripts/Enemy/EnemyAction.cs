using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [Header("이동 속도")]
    [SerializeField] private float _speed;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocityX = -_speed;
    }

    public void StopMove()
    {
        _speed = 0;
    }
}
