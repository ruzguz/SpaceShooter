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
    private float _spawnEnemyDelay = 3f;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private float _spawnPowerupDelay = 7f;
    [SerializeField]
    private bool _stopSpawn = false;
    [SerializeField]
    private int _spawnDuration = 10;
    [SerializeField]
    private UIManager _uiManager;
    private int _waveCounter;
    public int waveCounter 
    {
        get { return _waveCounter; }
        set { _waveCounter = value; }
    }
    [SerializeField]
    private GameObject _waveContainer;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update() 
    {
        CheckWave();
    }

    public void StartSpawning()
    {
        _uiManager.EnableWaveText();
        _stopSpawn = false;
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(ActiveSpawnTimerRoutine(_spawnDuration));
        _waveCounter++;
    }

    public void CheckWave()
    {
        if (_stopSpawn == true && _waveContainer.transform.childCount == 0) 
        {
            StartSpawning();
        }
    }
    IEnumerator ActiveSpawnTimerRoutine(int spawnDuration)
    {
        yield return new WaitForSeconds(spawnDuration);
        _stopSpawn = true;
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
            newEnemy.transform.parent = _waveContainer.transform;
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
            yield return new WaitForSeconds(_spawnPowerupDelay);

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

    public void UpdateSpawnDelay(float delay) 
    {
        _spawnEnemyDelay = delay;
    }

}
