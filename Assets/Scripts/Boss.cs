using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    [SerializeField]
    private bool _isInmune = true;
    [SerializeField]
    private float _speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 3.5f) 
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
    }
}
