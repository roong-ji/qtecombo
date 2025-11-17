using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerAnimator _playerAnimator;
    private PlayerAction _playerAction;
    private Rigidbody2D _rigidbody2D;

    private bool _isJumping;

    private void Awake()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerAction = GetComponent<PlayerAction>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _isJumping = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            InputBlock();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            InputAttack();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            InputJump();
        }

        _playerAnimator.PlayFallAnimation(_rigidbody2D.linearVelocity.y);
    }

    private void InputBlock()
    {
        _playerAction.Block();
        _playerAnimator.PlayBlockAnimation();
    }

    private void InputAttack()
    {
        _playerAction.Attack();
        _playerAnimator.PlayAttackAnimation();
    }

    private void InputJump()
    {
        if (_isJumping == true) return;

        _playerAction.Jump();
        _playerAnimator.PlayJumpAnimation();

        _isJumping = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") == false) return;
        _playerAnimator.EndJumpAnimation();
        _isJumping = false;
    }
}