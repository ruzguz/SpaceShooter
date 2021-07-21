using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    [SerializeField]
    private float _startYPosition = 8f;
    [SerializeField]
    private float _xRange = 11f;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _spawnEnemyDelay = 5f;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private float _spawnTripleShotPowerupDelay = 7f;
    [SerializeField]
    private bool _stopSpawn = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2f);
        while(_stopSpawn == false)
        {
            // Calculating random position
            float randomXPosition = Random.Range(-_xRange, _xRange);
            Vector3 randomPosition = new Vector3(randomXPosition, _startYPosition, transform.position.z);
                    
            // Spawning enemy and wait 5 seconds
            GameObject newEnemy = Instantiate(_enemyPrefab, randomPosition, Quaternion.identity);
            newEnemy.transform.parent = this.transform;
            yield return new WaitForSeconds(_spawnEnemyDelay);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(2f);
        while (_stopSpawn == false) 
        {
            // Calculating random position
            float randomXPosition = Random.Range(-11f, 11f);
            Vector3 RandomPosition = new Vector3(randomXPosition, 8f, transform.position.z);

            // Spaning triple shot powerup
            int randomPowerup = Random.Range(0,_powerups.Length -1);
            GameObject newPowerup = Instantiate(_powerups[randomPowerup], RandomPosition, Quaternion.identity);
            newPowerup.transform.parent = this.transform;
            yield return new WaitForSeconds(_spawnTripleShotPowerupDelay);

            // Spawn combustion laser rarely 
            randomXPosition = Random.Range(-11f, 11f);
            RandomPosition = new Vector3(randomXPosition, 8f, transform.position.z);

            // if random number = 0 then spawn combustion laser
            int probability  = Random.Range(0,20);
            if (probability == 0) 
            {
                newPowerup = Instantiate(_powerups[5], RandomPosition, Quaternion.identity);
                newPowerup.transform.parent = this.transform;
            }
        }
    }

    public void OnPlayerDead()
    {
        _stopSpawn = true;
    }
}
