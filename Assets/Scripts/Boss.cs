using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    [SerializeField]
    private bool _isInmune = true;
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

    [SerializeField]
    private int _currentPhase = 0;

    // Start is called before the first frame update
    void Start()
    {
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

}
