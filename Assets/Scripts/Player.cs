using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // General vars
    private Vector3 startingPosition = new Vector3(0,-3,0);
    [SerializeField]
    private int _lives = 3;
    // Movement variables
    [SerializeField] 
    private float _speed = 2.5f;
    private float _xLimit = 11.5f;
    private float _minY = -3, _maxY = 0;
    // Shooting variables 
    [SerializeField] 
    private float _laserOffset = 1f;
    [SerializeField] 
    private float _fireRate = 0.5f;
    private bool _canShoot = true;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField] 
    private GameObject _laserPrefab;
    [SerializeField] 
    private GameObject _tripleShotPrefab;
    // GameObject references 
    private SpawnManager _spawnManager;


    // Start is called before the first frame update
    void Start()
    {
        // Set player initial position 
        transform.position = startingPosition;

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null) 
        {
            Debug.LogError("Spawn Manager is NULL");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        
        
        if (Input.GetKeyDown(KeyCode.Space) && _canShoot)
        {
            Shoot();
        }
    }

    // Make player wait _fireRate before shoot again
    IEnumerator ActiveCooldown()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_fireRate);
        _canShoot = true;
    }

    void CalculateMovement() 
    {
        // Getting axis values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        // Translate player
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

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
        } else 
        {
            // Shooting a laser
            Vector3 laserStartPosition = new Vector3(transform.position.x, transform.position.y + _laserOffset, 0f);
            laser = Instantiate(_laserPrefab, laserStartPosition , Quaternion.identity);
        }

        laser.transform.parent = this.transform;
        // Calling cooldown function
        StartCoroutine(ActiveCooldown());
    }

    public void Damage()
    {
        _lives--;

        // Check dead
        if (_lives <= 0) 
        {
            _spawnManager.OnPlayerDead();
            Destroy(this.gameObject);
        }
    }
}

