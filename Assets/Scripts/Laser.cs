using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    private float _destroyLimit = 8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move laser up
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        // If laser position is greather than 8
        //    destroy the object
        if (transform.position.y > _destroyLimit) 
        {
            Destroy(gameObject);
        }
    }
}
