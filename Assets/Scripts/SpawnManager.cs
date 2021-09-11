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
    private float _spawnEnemyDelay = 3f;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private float _spawnPowerupDelay = 4f;
    [SerializeField]
    private bool _stopSpawn = false;
    [SerializeField]
    private int _spawnDuration = 10;
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private int _waveCounter;
    public int waveCounter 
    {
        get { return _waveCounter; }
        set { _waveCounter = value; }
    }
    [SerializeField]
    private GameObject _waveContainer;
    [SerializeField]
    private GameObject[] _enemies;
    [SerializeField]
    private int _lastWave = 10;
    private Coroutine _spawnEnemyRoutine, _activeSpawnTimerRoutine;
    [SerializeField]
    private GameObject _bossPrefab;
    private GameObject _boss;

    void Update() 
    {
        if (_waveCounter < _lastWave) 
        {
            CheckWave();
        } else if (_boss == null)
        {
            StopSpawning();
            _boss = Instantiate(_bossPrefab, new Vector3(0,12,0), Quaternion.identity);
        }
    }

    public void StopSpawning() 
    {
        StopCoroutine(_spawnEnemyRoutine);
        StopCoroutine(_activeSpawnTimerRoutine);
    }

    public void StartSpawning()
    {

        // Check if wave counter is a multiple of 2 to increase the spawn speed 
        // The idea is increase the speed each 2 rounds
        if (_waveCounter%2 == 0 && _spawnEnemyDelay>0.5f) 
        {
            _spawnEnemyDelay -= 0.3f;
            _spawnDuration += 5;
        }

        _uiManager.EnableWaveText();
        _stopSpawn = false;
        if (_boss == null) 
        {
            _spawnEnemyRoutine = StartCoroutine(SpawnEnemyRoutine());
            _activeSpawnTimerRoutine = StartCoroutine(ActiveSpawnTimerRoutine(_spawnDuration));
        }
        StartCoroutine(SpawnPowerupRoutine());
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

            // Get enemy type 
            int enemyIndex = GetEnemyIndex(Random.Range(0,60));
            Debug.Log(enemyIndex);
                    
            // Spawning enemy and wait 5 seconds
            GameObject newEnemy = Instantiate(_enemies[enemyIndex], randomPosition, Quaternion.identity);
            newEnemy.transform.parent = _waveContainer.transform;
            yield return new WaitForSeconds(_spawnEnemyDelay);
        }
    }

    int GetEnemyIndex(int value)
    {
        if (value < 20) 
        {
            return 0; // normal enemy
        } else if (value < 30) 
        {
            return 1; // Doger
        } else if (value < 40) 
        {
            return 2; // Aggressive enemy
        } else if (value < 50) 
        {
            return 3; // Enemy with Shield
        } else if (value < 60) 
        {
            return 4; // Smart Enemy
        } else 
        {
            return 0; // normal enemy
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
            int randomPowerup = GeneratePowerupIndex(Random.Range(0,101));

            GameObject newPowerup = Instantiate(_powerups[randomPowerup], RandomPosition, Quaternion.identity);
            newPowerup.transform.parent = this.transform;
            yield return new WaitForSeconds(_spawnPowerupDelay);
        }
    }

    int GeneratePowerupIndex(int random)
    {
        if (random >= 0 && random < 10) 
        {
            return 0; // Tripleshoot 
        } else if (random >= 10 && random < 20) 
        {
            return 1; // Speed Boost
        } else if (random >= 20 && random < 30) 
        {
            return 2; // Shield
        } else if (random >= 30 && random < 50) 
        {
            return 3; // Ammo
        } else if (random >= 50 && random < 55)
        {
            return 4; // Health
        } else if (random >= 60 && random < 70)  
        {
            return 6; // Negative: random movement
        } else if (random >= 70 && random < 80)
        {
            return 7; // Negative: Slow down player
        } else if (random >= 80 && random < 90) 
        {
            return 8; // Negative: zero ammo
        } else if (random >= 90 && random < 93) 
        {
            return 9; //  Combustion laser
        } else if (random >= 100 && random < 105) 
        {
            return 10; // Homming Laser
        }
        {
            return 3; //  Ammo
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
