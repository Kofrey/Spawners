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

    private void Update()
    {
        if(_isSpawning == false)
        {
            StopCoroutine(_coroutine);
        }
    }
    
    private IEnumerator SpawnUnit(float respawnTime)
    {
              
        while(enabled)
        {
            if (_timer < respawnTime)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                Unit unit = Instantiate(_unitPrefab, _spawnPoints[UserUtils.GenerateRandomNumber(0, _spawnPoints.Count - 1)]).GetComponent<Unit>();
                unit.SetDirection(_destinationPoint.position - unit.transform.position);
                _timer += Time.deltaTime - respawnTime;
            }

            yield return null;
        }    
    }
}
