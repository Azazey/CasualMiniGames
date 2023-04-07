using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private int _poolCount;
    [SerializeField] private bool _autoExpand;
    [SerializeField] private Food _foodPrefab;
    [SerializeField] private float _spawnTimer;

    private Pool<Food> _pool;

    public float SpawnTimer
    {
        get => _spawnTimer;
        set => _spawnTimer = value;
    }

    private void Awake()
    {
        _pool = new Pool<Food>(_foodPrefab, _poolCount, _autoExpand, transform);
        Invoke("SpawnFood", _spawnTimer);
    }

    public void SpawnFood()
    {
        var food = _pool.GetFreeElement();
        
        food.transform.position = new Vector3(Random.Range(-13f,12f),0.15f,Random.Range(-12f,13f));
        Invoke("SpawnFood", _spawnTimer);
    }
}
