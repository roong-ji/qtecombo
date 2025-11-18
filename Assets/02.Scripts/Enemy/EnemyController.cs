using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    protected EnemyMove _enemyMove;
    protected EnemyAnimator _enemyAnimator;
    protected Player _player;

    protected Collider2D _collider;
    [SerializeField] protected GameObject _guide;
    
    private void Awake()
    {
        _enemyMove = GetComponent<EnemyMove>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        Init();
    }
    private void Init()
    {
        _collider.enabled = true;
    }

    private void PlayAttack()
    {
        _enemyAnimator.PlayAttackAnimation();
        _enemyMove.StopMove();
    }

    public abstract void Attack();

    public virtual void TakeHit()
    {
        _enemyAnimator.PlayTakeHitAnimation();
        _enemyMove.StopMove();
        _collider.enabled = false;
        Destroy(_guide);
    }

    public void Knockback()
    {
        _enemyMove.Knockback();
    }


    public virtual void Death()
    {
        gameObject.SetActive(false);
        Destroy(_guide);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == false) return;
        _player = collision.GetComponent<Player>();
        PlayAttack();
    }
}
