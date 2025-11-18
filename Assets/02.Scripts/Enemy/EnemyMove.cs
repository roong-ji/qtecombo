using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [Header("이동 속도")]
    [SerializeField] private float _defaultSpeed;
    private float _finalSpeed;

    [SerializeField] private Vector3 _knockbackVector;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        InitSpeed();
    }

    public void InitSpeed()
    {
        if(SpeedManager.Instance == null)
        {
            _finalSpeed = _defaultSpeed;
            return;
        }

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
    
    public void Knockback()
    {
        transform.position += _knockbackVector;
    }
}
