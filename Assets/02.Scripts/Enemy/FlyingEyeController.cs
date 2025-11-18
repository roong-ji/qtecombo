using UnityEngine;

public class FlyingEyeController : EnemyController
{
    protected override void Init()
    {
        _collider.enabled = true;
    }
    public override void Attack()
    {
        _player.TakeHit();
        _collider.enabled = false;
    }
}
