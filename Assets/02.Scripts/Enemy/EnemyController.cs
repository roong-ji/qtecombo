using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyAction _enemyAction;
    private EnemyAnimator _enemyAnimator;
    private GameObject _player;

    private bool _isHittable;
    public bool IsHittable => _isHittable;

    private void Awake()
    {
        _enemyAction = GetComponent<EnemyAction>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _isHittable = true;
    }

    private void PlayAttack()
    {
        _enemyAnimator.PlayAttackAnimation();
        _enemyAction.StopMove();
    }

    public void Attack()
    {
        _player.GetComponent<PlayerController>().TakeHit();
        _isHittable = false;
    }
    
    public void TakeHit()
    {
        _enemyAnimator.PlayTakeHitAnimation();
        _enemyAction.StopMove();
        _isHittable = false;
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == false) return;
        if (IsHittable == false) return;
        _player = collision.gameObject;
        PlayAttack();
    }

}
