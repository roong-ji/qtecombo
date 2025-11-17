using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("점수 증가량")]
    [SerializeField] private float _deafaultScore;
    public float DeafaultScore => _deafaultScore;

    EEnemyType _enemyType;

    private EnemyController _enemyController;

    public void TakeHit()
    {
        _enemyController.TakeHit();
    }

    public bool CompareType(EEnemyType enemyType)
    {
        return _enemyType == enemyType;
    }

}
