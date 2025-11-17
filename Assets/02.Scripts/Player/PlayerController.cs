using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player _player;
    private PlayerAnimator _playerAnimator;
    private PlayerMove _playerMove;
    private PlayerAttack _playerAttack;
    private PlayerBlock _playerBlock;
    private Rigidbody2D _rigidbody2D;

    private bool _isJumping;
    private bool _isBlocking;
    private bool _counterAttack;

    public bool IsBlocking => _isBlocking;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerMove = GetComponent<PlayerMove>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerBlock = GetComponent<PlayerBlock>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _isJumping = false;
        _isBlocking = false;
        _counterAttack = false;
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
            InputAttack(EEnemyType.Mushroom);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InputJump();
        }

        _playerAnimator.PlayFallAnimation(_rigidbody2D.linearVelocity.y);
    }

    public void Block(Enemy enemy)
    {
        _counterAttack = true;
        _playerBlock.Block(enemy);
    }

    public void InputBlock()
    {
        if(_counterAttack == true)
        {
            InputCounterAttack();
            return;
        }

        _isBlocking = true;
        Debug.Log("blocked");
        _playerAnimator.PlayBlockAnimation();
    }

    public void InputEndBlock()
    {
        Debug.Log("block end");
        _isBlocking = false;
        _counterAttack = false;
    }

    private void InputCounterAttack()
    {
        _playerBlock.BlockAttack();
        _playerAnimator.PlayBlockAttackAnimation();
        _counterAttack = false;
    }

    public void InputAttack(EEnemyType attackType)
    {
        _playerAttack.Attack(attackType);
        _playerAnimator.PlayAttackAnimation(attackType);
        InputEndBlock();
    }

    public void InputJump()
    {
        if (_isJumping == true) return;

        _playerMove.Jump();
        _playerAnimator.PlayJumpAnimation();

        _isJumping = true;
        InputEndBlock();
    }

    public void TakeHit()
    {
        _playerAnimator.PlayHurtAnimation();
        InputEndBlock();
    }

    public void Death()
    {
        _playerAnimator.PlayDeathAnimation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") == false) return;
        _playerAnimator.EndJumpAnimation();
        _isJumping = false;
    }
}