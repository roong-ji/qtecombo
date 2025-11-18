using UnityEngine;

public class NormalEnemyController : EnemyController
{
    public override void Attack()
    {
        _player.TakeHit();
        _collider.enabled = false;
    }
}
