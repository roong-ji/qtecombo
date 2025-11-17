using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [Header("이동 속도")]
    [SerializeField] private float _defaultSpeed;
    private float _finalSpeed;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        InitSpeed();
    }

    private void InitSpeed()
    {
        _finalSpeed = SpeedManager.Instance.Speed * _defaultSpeed;
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocityX = -_finalSpeed;
    }

    public void StopMove()
    {
        _finalSpeed = 0;
    }
}
