using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    
    [Header("점프력")]
    [SerializeField] private float _jumpForce;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }


    public void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
