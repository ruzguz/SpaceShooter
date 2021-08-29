using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // General vars
    private Vector3 startingPosition = new Vector3(0,-3,0);
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score;
    // Movement variables
    [SerializeField] 
    private float _normalSpeed = 5f;
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _extraSpeed = 0f;
    private float _xLimit = 11.5f;
    private float _minY = -3, _maxY = 0;
    // Shooting variables 
    [SerializeField] 
    private float _laserOffset = 1f;
    [SerializeField] 
    private float _fireRate = 0.5f;
    private bool _canShoot = true;
    [SerializeField]
    private int _ammo = 15;
    [SerializeField]
    private int _maxAmmo = 30;
    public float maxAmmo 
    {
        get 
        {
            return _maxAmmo;
        }
    }
    // Powerup variables
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField] 
    private GameObject _laserPrefab;
    [SerializeField] 
    private GameObject _tripleShotPrefab;
    private float _tripleShotDuration = 5f;
    [SerializeField]
    private float _speedBoostDuration = 5f;
    [SerializeField]
    private float _speedBoost = 8.5f;
    // GameObject references 
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _playerShield;
    private UIManager _uiManager;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private PostProcessing _gamePostProcess;
    [SerializeField]
    private AudioSource _powerupAudio;
    [SerializeField]
    private AudioSource _damageAudio;
    [SerializeField]
    private GameObject _thruster;
    [SerializeField]
    private GameObject _shieldPrefab;
    private GameObject _shield;
    [SerializeField]
    private AudioSource _outOfAmmoAudio;
    [SerializeField]
    private GameObject _combustionLaser;
    [SerializeField]
    private bool _isCombustionLaserActive = false;
    [SerializeField]
    private float _fuel = 100f;
    private bool _fuelCooldownActive = false;
    private bool _isSystemHacked = false;
    private Vector3 _hackingPath;
    [SerializeField]
    private GameObject _hommingLaser;
    [SerializeField]
    private bool _isHommingLaserActive = false;



    // Start is called before the first frame update
    void Start()
    {
        // Set player initial position 
        transform.position = startingPosition;

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();

        if (_spawnManager == null) 
        {
            Debug.LogError("Spawn Manager is NULL");
        }

        if (_uiManager == null) 
        {
            Debug.LogError("UIManager is NULL");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // If player press left Key 
        // Then change speed value
        if (Input.GetKey(KeyCode.LeftShift)  && !_fuelCooldownActive) 
        {
            if (_fuel > 0) 
            {
                _thruster.SetActive(true);
                _extraSpeed = 3f;
                _fuel -= 15 * Time.deltaTime;
                _uiManager.UpdateThrusterFuel(_fuel);
            } else
            {
                _fuel = 0;
                _thruster.SetActive(false);
                _extraSpeed = 0f;
                StartCoroutine(ThrusterCooldownRoutine());
            }
        }

        // If player releases the shift key
        // Then set speed to normal
        if (Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            _thruster.SetActive(false);
            _extraSpeed = 0f;
        }


        CalculateMovement();
        
        if (Input.GetKeyDown(KeyCode.Space) && _canShoot)
        {
            Shoot();
        }

    }

    IEnumerator ThrusterCooldownRoutine()
    {
        yield return new WaitForSeconds(3f);
        _fuelCooldownActive = true;
        while (true) 
        {
            _fuel += 15 * Time.deltaTime;
            _uiManager.UpdateThrusterFuel(_fuel);
            if (_fuel >= 100f) 
            {
                _fuelCooldownActive = false;
                break;
            }
            yield return new WaitForSeconds(15 * Time.deltaTime);
        }
    }

    // Make player wait _fireRate before shoot again
    IEnumerator ActiveCooldown()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_fireRate);
        _canShoot = true;
    }

    public void ActiveTripleShot()
    {
        _isTripleShotActive = true;
        _isCombustionLaserActive = false;
        StartCoroutine(TripleShotCooldown());
    }

    IEnumerator TripleShotCooldown()
    {
        yield return new WaitForSeconds(_tripleShotDuration);
        _isTripleShotActive = false;
    }

    public void ActivateCombustionLaser()
    {
        _isCombustionLaserActive = true;
        _isTripleShotActive = false;
        StartCoroutine(CombustionLaserRoutine());
    }

    IEnumerator CombustionLaserRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isCombustionLaserActive = false;
    }

    IEnumerator SlowDownCooldown()
    {
        _speed = 1.5f;
        yield return new WaitForSeconds(5f);
        _speed = _normalSpeed;
    }

    public void ActiveSlowDown()
    {
        StartCoroutine(SlowDownCooldown());
    }

    public void ActivateSpeedBoost()
    {
        StartCoroutine(SpeedBoostCooldown());
    }
    
    IEnumerator SpeedBoostCooldown()
    {
        _speed = _speedBoost;
        _gamePostProcess.SetSpeedEffect(1f);
        yield return new WaitForSeconds(_speedBoostDuration);
        _speed = _normalSpeed;
        _gamePostProcess.SetSpeedEffect(0f);
    }

    public void ActivateShield()
    {
        if (_shield != null) 
        {
            Destroy(_shield);
        }

        GameObject shield = Instantiate(_shieldPrefab, transform.position, Quaternion.identity);
        shield.transform.parent = transform;
        _shield = shield; 

    }

    IEnumerator HommingLaserRoutine()
    {
        _isHommingLaserActive = true;
        yield return new WaitForSeconds(6f);
        _isHommingLaserActive = false;
    }

    public void ActiveHommingLaser()
    {
        StartCoroutine(HommingLaserRoutine());
    }

    public void AddAmmo()
    {
        _ammo += 15;
        
        if (_ammo > _maxAmmo) 
        {
            _ammo = _maxAmmo;
        }
        _uiManager.UpdateAmmo(_ammo, _maxAmmo);
    }

    IEnumerator HackSystemRoutine()
    {
        _hackingPath = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        _isSystemHacked = true;
        yield return new WaitForSeconds(10f);
        _isSystemHacked = false;
    }

    public void EnableHakingMovement() 
    {
        StartCoroutine(HackSystemRoutine());
    }

    void CalculateMovement() 
    {
        // Getting axis values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        // Translate player
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * (_speed + _extraSpeed) * Time.deltaTime);

        // Movement if player is hacked
        if (_isSystemHacked) 
        {
            transform.Translate(_hackingPath * (_speed + _extraSpeed) * Time.deltaTime);
        }

        // Vertical bounds
        if (transform.position.y <= _minY) 
        {
            transform.position = new Vector3(transform.position.x, _minY, 0);
        } else if (transform.position.y >= _maxY) 
        {
            transform.position = new Vector3(transform.position.x, _maxY, 0);
        }

        // Horizontal movement boudnds validation
        if (transform.position.x  > _xLimit || 
            transform.position.x  < -_xLimit) 
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y,0);
        }
    }

    void Shoot()
    {    
        GameObject laser;
        if (_isTripleShotActive) 
        {
            laser = Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        } else if (_isCombustionLaserActive)
        {
            laser = Instantiate(_combustionLaser, transform.position, Quaternion.identity);
        } else if (_isHommingLaserActive) 
        {
            laser = Instantiate(_hommingLaser, transform.position, Quaternion.identity);
        } else if (_ammo > 0)
        {
            _ammo--;
            _uiManager.UpdateAmmo(_ammo, _maxAmmo);
            // Shooting a laser
            Vector3 laserStartPosition = new Vector3(transform.position.x, transform.position.y + _laserOffset, 0f);
            laser = Instantiate(_laserPrefab, laserStartPosition , Quaternion.identity);
        } else 
        {
            _outOfAmmoAudio.Play();
        }

        // Calling cooldown function
        StartCoroutine(ActiveCooldown());
    }

    public void ActiveZeroAmmo()
    {
        _ammo = 0;
        _uiManager.UpdateAmmo(_ammo, _maxAmmo);
    }

    public void Heal()
    {
        if (_lives < 3) 
        {
            _lives++;
            _uiManager.UpdateLives(_lives);
        }
        DrawEngineDamage();
    }

    public void Damage()
    {
        _gameManager.ShakeCamera();
        _lives--;
        _uiManager.UpdateLives(_lives);
        _damageAudio.Play();


        DrawEngineDamage();

        // Check dead
        if (_lives <= 0) 
        {
            _spawnManager.OnPlayerDead();
            _uiManager.ShowGameOverScreen();
            _gameManager.FinishGame();
            Destroy(this.gameObject);
        }
    }

    void DrawEngineDamage()
    {
        if (_lives == 3) 
        {
            _rightEngine.SetActive(false);
            _leftEngine.SetActive(false);
        }
        else if (_lives == 2) 
        {
            _rightEngine.SetActive(true);
            _leftEngine.SetActive(false);
        } else if (_lives == 1) 
        {
            _rightEngine.SetActive(true);
            _leftEngine.SetActive(true);
        }
    }

    public void UpdateScore(int score)
    {
        _score += score;
        _uiManager.UpdateScore(_score);
    }

    public void PlayPowerupAudio()
    {
        _powerupAudio.Play();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy Laser")) 
        {
            Destroy(other.gameObject);
            Damage();
        }
    }
}

