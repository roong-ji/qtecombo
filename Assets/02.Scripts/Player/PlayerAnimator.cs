using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private readonly int[] _attack = { Animator.StringToHash("Attack1"),
                                        Animator.StringToHash("Attack2") };
    private readonly int _blockAttack = Animator.StringToHash("Attack3");
    private readonly int _jump = Animator.StringToHash("Jump");
    private readonly int _block = Animator.StringToHash("Block");
    private readonly int _grounded = Animator.StringToHash("Grounded");
    private readonly int _airSpeedY = Animator.StringToHash("AirSpeedY");
    private readonly int _hurt = Animator.StringToHash("Hurt");
    private readonly int _death = Animator.StringToHash("Death");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAttackAnimation(EEnemyType attackType)
    {
        _animator.SetTrigger(_attack[(int)attackType]);
    }

    public void PlayBlockAttackAnimation()
    {
        _animator.SetTrigger(_blockAttack);
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
