using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerAnimator _playerAnimator;
    private PlayerMove _playerMove;
    private PlayerAttack _playerAttack;
    private Rigidbody2D _rigidbody2D;

    private bool _isJumping;
    private bool _isIdleBlock;
    private bool _isDeath;

    [Header("체력")]
    [SerializeField] private int _health;

    private void Awake()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerMove = GetComponent<PlayerMove>();
        _playerAttack = GetComponent<PlayerAttack>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _isJumping = false;
        _isDeath = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            InputBlock();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            InputAttack(EEnemyType.FlyingEye);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            InputAttack(EEnemyType.Goblin);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InputJump();
        }

        _playerAnimator.PlayFallAnimation(_rigidbody2D.linearVelocity.y);
    }

    public void InputBlock()
    {
        if (_isIdleBlock == true)
        {
            InputAttack(EEnemyType.FlyingEye);
            _isIdleBlock = false;
            return;
        }

        _playerAnimator.PlayBlockAnimation();
        
        if (_playerAttack.Block() == false) return;
        _playerAnimator.PlayIdleBlockAnimation();

        _isIdleBlock = true;
    }

    public void InputEndBlock()
    {
        _isIdleBlock = false;
        _playerAnimator.EndIdleBlockAnimation();
    }

    public void InputAttack(EEnemyType attackType)
    {
        _playerAttack.Attack(attackType);
        _playerAnimator.PlayAttackAnimation((int)attackType);
    }

    public void InputJump()
    {
        if (_isJumping == true) return;

        _playerMove.Jump();
        _playerAnimator.PlayJumpAnimation();

        _isJumping = true;
    }

    public void TakeHit()
    {
        if (_isDeath == true) return;

        _playerAnimator.PlayHurtAnimation();
        --_health;

        if (_health > 0) return;
        Death();
    }

    private void Death()
    {
        _playerAnimator.PlayDeathAnimation();
        _isDeath = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") == false) return;
        _playerAnimator.EndJumpAnimation();
        _isJumping = false;
    }
}