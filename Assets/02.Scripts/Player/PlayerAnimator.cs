using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private readonly int[] _attack = { Animator.StringToHash("Attack1"),
                                        Animator.StringToHash("Attack2"),
                                        Animator.StringToHash("Attack3") };
    private readonly int _jump = Animator.StringToHash("Jump");
    private readonly int _block = Animator.StringToHash("Block");
    private readonly int _idleBlock = Animator.StringToHash("IdleBlock");
    private readonly int _grounded = Animator.StringToHash("Grounded");
    private readonly int _airSpeedY = Animator.StringToHash("AirSpeedY");
    private readonly int _hurt = Animator.StringToHash("Hurt");
    private readonly int _death = Animator.StringToHash("Death");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAttackAnimation(int attackType)
    {
        _animator.SetTrigger(_attack[attackType]);
    }
    public void PlayJumpAnimation()
    {
        _animator.SetTrigger(_jump);
        _animator.SetBool(_grounded, false);
    }

    public void PlayBlockAnimation()
    {
        _animator.SetTrigger(_block);
    }

    public void PlayIdleBlockAnimation()
    {
        _animator.SetBool(_idleBlock, true);
    }

    public void EndIdleBlockAnimation()
    {
        _animator.SetBool(_idleBlock, false);
    }

    public void PlayFallAnimation(float speed)
    {
        _animator.SetFloat(_airSpeedY, speed);
    }

    public void EndJumpAnimation()
    {
        _animator.SetBool(_grounded, true);
    }

    public void PlayHurtAnimation()
    {
        _animator.SetTrigger(_hurt);
    }

    public void PlayDeathAnimation()
    {
        _animator.SetTrigger(_death);
    }
}
