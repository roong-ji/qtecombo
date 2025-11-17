using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [Header("มกวมทย")]
    [SerializeField] private float _jumpForce;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Attack()
    {

    }

    public void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    public void Block()
    {

    }
}
