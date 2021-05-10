using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // General vars
    private Vector3 startingPosition = new Vector3(0,-3,0);
    // Movement variables
    [SerializeField] private float _speed = 2.5f;
    private float _xLimit = 11.5f;
    private float _minY = -3, _maxY = 0;
    [SerializeField] private float _laserOffset = 2f;
    
    // Shooting variables
    [SerializeField] private GameObject _laserPrefab;


    // Start is called before the first frame update
    void Start()
    {
        // Set player initial position 
        transform.position = startingPosition;
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Shooting a laser
            Vector3 laserStartPosition = new Vector3(transform.position.x, transform.position.y + _laserOffset, 0f);
            Instantiate(_laserPrefab, laserStartPosition , Quaternion.identity);
        }
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
}
