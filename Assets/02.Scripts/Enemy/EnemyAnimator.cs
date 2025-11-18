using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;

    private readonly int _attack = Animator.StringToHash("Attack");
    private readonly int _takeHit = Animator.StringToHash("TakeHit");
    private readonly int _blocked = Animator.StringToHash("Blocked");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(_attack);
    }

    public void PlayTakeHitAnimation()
    {
        _animator.SetTrigger(_takeHit);
    }

    public void PlayBlockedAnimation(bool blocked)
    {
        _animator.SetBool(_blocked, blocked);
    }
}
