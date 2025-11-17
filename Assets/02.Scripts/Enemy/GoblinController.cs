using UnityEngine;

public class GoblinController : EnemyController
{
    private bool _blocked;

    protected override void Init()
    {
        _collider.enabled = true;
        _blocked = false;
    }

    public override void Attack()
    {

        // 1, 플레이어가 블락 중일 경우 => attack2 애니메이션 재생하고 플레이어 블락 메서드 실행
        if(_player.IsBlocking == true)
        {
            _enemyAnimator.PlayBlockedAnimation(true);
            _player.Block(gameObject,GetComponent<Enemy>());
        }

        // 2. 플레이어가 블락 중이지 않을 경우 => run 애니메이션 재생하고 플레이어 피격 메서드 실행
        else
        {
            _enemyAnimator.PlayBlockedAnimation(false);
            _enemyMove.InitSpeed();
            _player.TakeHit();
        }
    }

    private void Blocked()
    {
        _blocked = true;
    }
}
