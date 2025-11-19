using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("체력")]
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;

    private PlayerController _playerController;
    private HealthUI _healthUI;

    public bool IsBlocking => _playerController.IsBlocking;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _healthUI = GetComponent<HealthUI>();
    }

    public void TakeHit()
    {
        if (_health <= 0) return;

        --_health;
        _playerController.TakeHit();
        _healthUI.OffHealthUI();

        if (_health > 0) return;
        _playerController.Death();
    }

    public void Counter(Enemy enemy)
    {
        _playerController.Counter(enemy);
    }

    public void Heal()
    {
        if (_health >= _maxHealth) return;
        ++_health;
        _healthUI.OnHealthUI();
    }

    public void QTEMode(bool qte)
    {
        _playerController.QTEMode(qte);
    }

    public EButton GetInputButton()
    {
        return _playerController.InputButton;
    }
}
