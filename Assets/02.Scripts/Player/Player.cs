using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("체력")]
    [SerializeField] private int _health;

    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    public void TakeHit()
    {
        if (_health <= 0) return;

        --_health;
        _playerController.TakeHit();

        if (_health > 0) return;
        _playerController.Death();
    }
}
