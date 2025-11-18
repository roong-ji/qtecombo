using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("점수 증가량")]
    [SerializeField] private float _defaultScore;
    public float DefaultScore => _defaultScore;

    [SerializeField] EEnemyType _enemyType;

    private EnemyController _enemyController;

    private void Awake()
    {
        _enemyController = GetComponent<EnemyController>();
    }

    public void TakeHit()
    {
        _enemyController.TakeHit();
    }

    public void Knockback()
    {
        _enemyController.Knockback();
    }

    public bool CompareType(EEnemyType enemyType)
    {
        return _enemyType == enemyType;
    }
}
