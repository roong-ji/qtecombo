using UnityEngine;
using System.Collections.Generic;

public class QTEEnemyController : EnemyController
{
    [SerializeField] private GameObject[] _guideButtons;
    [SerializeField] private SpriteRenderer[] _buttons;
    [SerializeField] private Sprite[] _buttonSprites;
    private Queue<int> _qteQueue;
    private EButton _inputButton;
    private int _index;

    public enum EButton
    {
        None = 0,
        Red = 1,
        Orange = 2,
        Green = 3,
        Blue = 4,
    }

    private void Start()
    {
        _qteQueue = new Queue<int>();
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        _collider.enabled = true;
        _guide.SetActive(false);
        _inputButton = 0;
        _index = 0;
    }

    private void Update()
    {
        if (_qteQueue.Count <= 0) return;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            _inputButton = EButton.Orange;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            _inputButton = EButton.Green;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            _inputButton = EButton.Blue;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _inputButton = EButton.Red;
        }

        if ((int)_inputButton - 1 == _qteQueue.Peek())
        {
            _qteQueue.Dequeue();
            _inputButton = 0;
            _guideButtons[_index++].SetActive(false);

            if (_qteQueue.Count > 0) return;
            EndQTE(); 
            PlayIdle();
        }
        else if (_inputButton != 0)
        {
            _qteQueue.Clear();
            EndQTE();
            PlayAttack();
        }
    }

    private void PlayQTE()
    {
        _enemyAnimator.PlayShieldAnimation();
        _player.QTEMode(true);
        MakeQTEPattern();
        _guide.SetActive(true);
        Time.timeScale = 0f;
    }

    private void MakeQTEPattern()
    {
        int i;
        for(i = 0; i<Random.Range(1, _buttons.Length); ++i)
        {
            _guideButtons[i].SetActive(true);
            int random = Random.Range(0, _buttonSprites.Length);
            _buttons[i].sprite = _buttonSprites[random];
            _qteQueue.Enqueue(random);
        }
        for (; i < _buttons.Length; ++i)
        {
            _guideButtons[i].SetActive(false);
        }
        /*foreach(var button in _buttons)
        {
            int random = Random.Range(0, _buttonSprites.Length);
            button.sprite = _buttonSprites[random];
            _qteQueue.Enqueue(random);
        }*/
    }

    private void EndQTE()
    {
        _player.QTEMode(false);
        Time.timeScale = 1f;
    }

    private void PlayIdle()
    {
        _enemyMove.StopMove();
        _enemyAnimator.PlayIdleAnimation(); 
        _player.Counter(GetComponent<Enemy>());
    }

    private void PlayAttack()
    {
        _enemyAnimator.PlayAttackAnimation();
        _enemyMove.StopMove();
        _guide.SetActive(false);
    }

    public override void Attack()
    {
        _player.TakeHit();
    }

    public void EndAttack()
    {
        _enemyMove.InitSpeed();
    }

    public override void TakeHit()
    {
        _enemyAnimator.PlayTakeHitAnimation();
        _enemyMove.StopMove();
        _collider.enabled = false;
    }

    public override void Death()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == false) return;
        _player = collision.GetComponent<Player>();
        PlayQTE();
    }
}
