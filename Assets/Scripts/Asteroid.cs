using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField]
    private float _rotationSpeed = 4f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * _rotationSpeed);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Laser")) 
        {
            Destroy(this.gameObject);
        }    
    }
}
