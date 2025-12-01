using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private bool _isSpawning = true;
    [SerializeField] private float _spawnCooldown;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Transform _destinationPoint;
    [SerializeField] private float _timer;
    private Coroutine _coroutine;

    private void Start()
    {
        _timer = 0.0f;
        _coroutine = StartCoroutine(SpawnUnit(_spawnCooldown));
    }
    
    private IEnumerator SpawnUnit(float respawnTime)
    { 
        while(_isSpawning == true)
        {
            if (_timer < respawnTime)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                int randomIndex = UnityEngine.Random.Range(0, _spawnPoints.Count - 1);
                Unit unit = Instantiate(_unitPrefab, _spawnPoints[randomIndex]);
                unit.SetTarget(_destinationPoint);
                _timer += Time.deltaTime - respawnTime;
            }

            yield return null;
        }    
    }
}
