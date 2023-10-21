using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _templates = new List<GameObject>();
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;
    [SerializeField] private List<Wave> _waves;

    private Wave _currentWave;
    private int _currentWaveIndex = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;
    private int _killed;

    public event UnityAction AllEnemyKilled;
    public event UnityAction<int, int> EnemySpawned;
    public event UnityAction<int, int> EnemyKilled;

    private void Start()
    {
        SetWave(_currentWaveIndex);
    }

    private void Update()
    {
        if(_currentWave == null)
        {
            return;
        }

        _timeAfterLastSpawn += Time.deltaTime;

        if(_timeAfterLastSpawn >= _currentWave.Delay & _currentWave.IsWork == true)
        {
            InstantiateEnemy();
            _spawned++;
            EnemySpawned?.Invoke(_spawned, _currentWave.Count);
            _timeAfterLastSpawn = 0;
        }

        if(_currentWave.Count <= _spawned)
        {
            _currentWave.IsWork = false;
        }

        if(_killed == _currentWave.Count)
        {
            if (_waves.Count > _currentWaveIndex + 1)
            {
                AllEnemyKilled?.Invoke();
            }
        }
    }

    public void NextWave()
    {
        _killed = 0;
        _spawned = 0;
        EnemySpawned?.Invoke(_spawned, 1);
        EnemyKilled?.Invoke(_killed, 1);
        SetWave(++_currentWaveIndex);
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private void InstantiateEnemy()
    {
        int enemyIndex = Random.Range(0, _currentWave.Templates.Count);
        Enemy enemy = Instantiate(_currentWave.Templates[enemyIndex], _spawnPoint.position,_spawnPoint.rotation,_spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Dying += OnEnemyDying;
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;
        _killed++;
        EnemyKilled?.Invoke(_killed, _currentWave.Count);
        _player.AddMoney(enemy.Reward);
    }
}

[System.Serializable]
public class Wave
{
    public List<GameObject> Templates;
    public float Delay;
    public int Count;
    public bool IsWork = true;
}
