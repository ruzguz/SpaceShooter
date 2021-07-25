using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // General vars
    [SerializeField] 
    private float _speed = 4f;
    private float _respawnYPosition = 8f;
    private float _bottomLimit = -5.5f;
    private float _horizontalLimit = 9.5f;
    [SerializeField]
    private int _movementType; // 0 = straigh, 1 = wave, 2 = diagonal
    // References
    Player _player;
    private Animator _enemyAnim;
    private BoxCollider2D _enemyCollider;
    [SerializeField]
    private AudioSource _explosionAudio;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private Transform[] _cannonPositions; // 0 = left cannon, 1 = right cannon
    private IEnumerator _shootLaserRoutine;


    void Start() {

        _player = GameObject.Find("Player").GetComponent<Player>();
        
        if (_player == null) 
        {
            Debug.LogError("Enemy Script: Player is null");
        }    

        _enemyAnim = this.GetComponent<Animator>();
        
        if (_enemyAnim == null) 
        {
            Debug.LogError("Enemy Script: Enemy Animator is null");
        }

        _enemyCollider = GetComponent<BoxCollider2D>();

        if (_enemyCollider == null) 
        {
            Debug.LogError("Enemy Script: Collider is null");
        }

        _movementType = Random.Range(0,6);

        if (_movementType == 2) 
        {
            transform.Rotate(0,0,-30);
        }

        _explosionAudio = GameObject.Find("Explosion").GetComponent<AudioSource>();
        _shootLaserRoutine = ShootLaserRoutine();
        StartCoroutine(_shootLaserRoutine);
    }

    // Update is called once per frame
    void Update()
    {
        switch(_movementType)
        {
            case 1:
                // The number that multiply the value within cos increase the speed, the number that multiply the result increase the range 
                transform.Translate(new Vector3(Mathf.Cos(Time.time * 4) * 2, -1, 0) * _speed * Time.deltaTime);
                break;
            case 2: 
                
                transform.Translate(Vector3.down * _speed * 2 * Time.deltaTime);
                break;
            default:
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
                break;
        }
        
        // If enemy goes out of the camera, respawn the enemy at the top i a new random x position
        if (transform.position.y <= _bottomLimit) 
        {
            float newXposition = Random.Range(-_horizontalLimit, _horizontalLimit);
            transform.position = new Vector3(newXposition, _respawnYPosition, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

        // Collision with player
        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null) {
                player.Damage();
                this.Explode();
            }
        }

        // Collision with laser
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            _player.UpdateScore(10);
            this.Explode();
        }

        if (other.CompareTag("Shield")) 
        {
            other.GetComponent<Shield>().Damage();
            _player.UpdateScore(10);
            this.Explode();
        }

        if (other.CompareTag("Combustion"))
        {
            _player.UpdateScore(10);
            this.Explode();
        }

        if (other.CompareTag("CombustionLaser")) 
        {
            other.GetComponent<CombustionLaser>().Explode();
            _player.UpdateScore(10);
            this.Explode();
        }
    }

    IEnumerator ShootLaserRoutine() 
    {
        while(true)
        {
            int index = Random.Range(0,2);
            GameObject laser = Instantiate(_laserPrefab, _cannonPositions[index].position, Quaternion.identity);
            laser.transform.parent = transform;
            Debug.Log(index);
            yield return new WaitForSeconds(Random.Range(3f,5f));            
        }
    }

    void Explode()
    {
        StopCoroutine(_shootLaserRoutine);
        _explosionAudio.Play();
        _enemyAnim.SetTrigger("OnEnemyDeath");
        _speed = 0;
        _enemyCollider.enabled = false;
        Destroy(this.gameObject, 3f);
    }
}