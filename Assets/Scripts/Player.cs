using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // General vars
    private Vector3 startingPosition = new Vector3(0,-3,0);
    [SerializeField] private float _speed = 2.5f;


    // Start is called before the first frame update
    void Start()
    {
        // Set player initial position 
        transform.position = startingPosition;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Getting axis values
        float horizontalInput = Input.GetAxis("Horizontal");

        // Translate player
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);

    }
}
