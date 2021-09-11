using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    // General  variables
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _leftEngine, _rightEngine;
    [SerializeField]
    private int _hits = 0;
    private int _currentPhase = 0;
    [SerializeField]
    private int _maxHits = 10;
    [SerializeField]
    private bool _isInmune = true;
    // Movement variables
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _frequncy;
    [SerializeField]
    private float _magnitude;

    private Vector3 _startPosition;
    [SerializeField]
    private Transform _leftCannon, _rightCannon;
    
    // Phase 1 variables
    [SerializeField]
    private float _attackDelay = 2f;
    [SerializeField]
    private GameObject _bossAttack1;
    private Coroutine _phase1Routine;
    // Phase 2 variables
    [SerializeField]
    private GameObject _enemyPrefab;
    private Coroutine _phase2Routine;



    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource ==  null) 
        {
            Debug.LogError("Audio Source is NULL");
        }

        _startPosition = new Vector3(0,3.5f,0);
        StartCoroutine(EntranceAnimationRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator EntranceAnimationRoutine()
    {
        while (transform.position.y > 3.5f) 
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(WaveMovementRoutine());
        _phase1Routine = StartCoroutine(Phase1AttackRoutine());
    }

    IEnumerator WaveMovementRoutine()
    {
        while(true) 
        {
            transform.position = _startPosition + Vector3.right * Mathf.Sin(Time.time * _frequncy) * _magnitude;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Phase1AttackRoutine()
    {
        while (true) 
        {
            Instantiate(_bossAttack1, _leftCannon.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(1f,_attackDelay));
            Instantiate(_bossAttack1, _rightCannon.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(1f,_attackDelay));
        }
    }

    IEnumerator Phase2AttackRoutine() 
    {
        while (true) 
        {
            // Calculating random position
            float randomXPosition = Random.Range(-9f, 9f);
            Vector3 randomPosition = new Vector3(randomXPosition, 8f, transform.position.z);
                    
            // Spawning enemy and wait 5 seconds
            GameObject newEnemy = Instantiate(_enemyPrefab, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Laser")) 
        {
            _audioSource.Play();
            Destroy(other.gameObject);
            _hits++;
            Debug.Log("hits: "+_hits);
            
            if (_hits >= _maxHits) 
            {
                _hits = 0;
                _currentPhase++;

                switch(_currentPhase) 
                {
                    case 1:
                        _phase2Routine = StartCoroutine(Phase2AttackRoutine());
                        _leftEngine.SetActive(true);
                        break;
                    case 2: 
                        // active phase 3
                        _rightEngine.SetActive(true);
                        break;
                    case 3:
                        // Show win screen
                        break;
                }
            }
        }
    }

}
