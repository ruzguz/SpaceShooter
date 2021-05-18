using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _startYPosition = 8f;
    [SerializeField]
    private float _xRange = 11f;
    [SerializeField]
    private float _spawnDelay = 5f;
    [SerializeField]
    private bool _stopSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        while(_stopSpawn == false)
        {
            // Calculating random position
            float randomXPosition = Random.Range(-_xRange, _xRange);
            Vector3 randomPosition = new Vector3(randomXPosition, _startYPosition, transform.position.z);
                    
            // Spawning enemy and wait 5 seconds
            GameObject newEnemy = Instantiate(_enemyPrefab, randomPosition, Quaternion.identity);
            newEnemy.transform.parent = this.transform;
            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    public void OnPlayerDead()
    {
        _stopSpawn = true;
    }
}
