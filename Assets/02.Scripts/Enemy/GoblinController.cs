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
        _player.TakeHit();
        //_collider.enabled = false;
    }

    private void Blocked()
    {
        _blocked = true;
    }
}
