using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player _player;
    private PlayerAnimator _playerAnimator;
    private PlayerMove _playerMove;
    private PlayerAttack _playerAttack;
    private PlayerCounter _playerBlock;
    private CameraShake _cameraShake;
    private Rigidbody2D _rigidbody2D;

    private float _shakeTime = 0.2f;
    private float _attackShake = 0.1f;
    private float _counterShakeTime = 0.3f;
    private float _counterShake = 5f;
    private float _lerpTime = 2.5f;

    private bool _isJumping;
    private bool _isBlocking;
    private bool _qteMode;

    private EButton _inputButton;
    public EButton InputButton => _inputButton;
    
    public bool IsBlocking => _isBlocking;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerMove = GetComponent<PlayerMove>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerBlock = GetComponent<PlayerCounter>();
        _cameraShake = GetComponent<CameraShake>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _isJumping = false;
        _isBlocking = false;
        _qteMode = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            InputBlock();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            InputGreenButton();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            InputBlueButton();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InputJump();
        }

        _playerAnimator.PlayFallAnimation(_rigidbody2D.linearVelocity.y);
    }

    public void QTEMode(bool qte)
    {
        _qteMode = qte;
        _inputButton = 0;
    }

    public void Counter(Enemy enemy)
    {
        _playerBlock.Counter(enemy);
        InputCounterAttack();
    }

    public void InputBlock()
    {
        if (GameManager.Instance.IsGameOver) return;
        _inputButton = EButton.Orange;
        if (_qteMode == true) return;

        _isBlocking = true;
        _playerAnimator.PlayBlockAnimation();
    }

    public void InputEndBlock()
    {
        _isBlocking = false;
    }

    private void InputCounterAttack()
    {
        _playerBlock.CounterAttack();
        _playerAttack.CounterAttack();
        _playerAnimator.PlayCounterAttackAnimation();
        InputEndBlock();
    }

    public void CounterAttackShake()
    {
        _cameraShake.ShakeForTime(_counterShake, _counterShakeTime);
    }

    public void InputAttack(EEnemyType attackType)
    {
        if(_playerAttack.Attack(attackType) == true)
        {
            _player.Heal();
            _cameraShake.ShakeForTime(_attackShake, _shakeTime);
        }
        _playerAnimator.PlayAttackAnimation(attackType);
        InputEndBlock();
    }

    public void InputGreenButton()
    {
        if (GameManager.Instance.IsGameOver) return;
        _inputButton = EButton.Green;
        if (_qteMode == true) return;
        InputAttack(EEnemyType.FlyingEye);
    }

    public void InputBlueButton()
    {
        if (GameManager.Instance.IsGameOver) return;
        _inputButton = EButton.Blue;
        if (_qteMode == true) return;
        InputAttack(EEnemyType.Mushroom);
    }

    public void InputJump()
    {
        if (GameManager.Instance.IsGameOver) return;
        _inputButton = EButton.Red;
        if (_qteMode == true) return;

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
        EnemyFactory.Instance.ReturnAllEnemy();
        TimeManager.Instance.SlowMotion(_lerpTime);
        GameManager.Instance.GameOver();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") == false) return;
        _playerAnimator.EndJumpAnimation();
        _isJumping = false;
    }
}