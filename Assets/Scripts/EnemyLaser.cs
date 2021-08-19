using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{

    [SerializeField]
    private float _speed = 8f;
    
    public float speed 
    {
        set { _speed = value; }
        get { return _speed; }
    }
    private float _limit = -11f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if (transform.position.y < _limit || transform.position.y > -_limit) 
        {
            Destroy(gameObject);
        }
    }
}
