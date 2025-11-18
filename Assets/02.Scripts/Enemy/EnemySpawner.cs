using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct SpawnPattern
{
    public EEnemyType[] Type;
    public float[] Interval;
    public int Length;
}

public class EnemySpawner : MonoBehaviour
{
    [Header("몬스터 스폰 패턴")]
    [SerializeField] private SpawnPattern[] _spawnPatterns;

    private Queue<(EEnemyType Type, float Interval)> _enemyQueue;
    private EEnemyType _nextEnemy;
    private float _nextInterval;
    private float _timer;

    private void Awake()
    {
        _timer = 0f;
        _enemyQueue = new Queue<(EEnemyType, float)>();
        NextPattern();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer < _nextInterval) return;
        SpawnEnemy();
        
        if (_enemyQueue.Count > 0) return;
        NextPattern();
    }

    private void SpawnEnemy()
    {
        EnemyFactory.Instance.MakeEnemy(_nextEnemy, transform.position.x);
        SetNextEnemy();
    }

    private void NextPattern()
    {
        // 다음 패턴 지정
        int nextIndex = Random.Range(0, _spawnPatterns.Length);
        SpawnPattern spawnPattern = _spawnPatterns[nextIndex];

        for (int i = 0; i < spawnPattern.Length; ++i)
        {
            _enemyQueue.Enqueue((spawnPattern.Type[i], spawnPattern.Interval[i]));
        }

        SetNextEnemy();
    }

    private void SetNextEnemy()
    {
        var next = _enemyQueue.Dequeue();
        _nextEnemy = next.Type;
        _nextInterval = next.Interval;

        _timer = 0f;
    }
}
