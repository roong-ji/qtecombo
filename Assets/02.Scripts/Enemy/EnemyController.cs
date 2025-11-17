using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyAction _enemyAction;
    private EnemyAnimator _enemyAnimator;
    private GameObject _player;

    private Collider2D _collider;

    private void Awake()
    {
        _enemyAction = GetComponent<EnemyAction>();
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
        _enemyAction.StopMove();
    }

    public void Attack()
    {
        _player.GetComponent<PlayerController>().TakeHit();
        _collider.enabled=false;
    }
    
    public void TakeHit()
    {
        _enemyAnimator.PlayTakeHitAnimation();
        _enemyAction.StopMove();
        _collider.enabled = false;
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == false) return;
        _player = collision.gameObject;
        PlayAttack();
    }

}
