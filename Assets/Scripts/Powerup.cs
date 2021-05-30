using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int powerupID;

    // Update is called once per frame
    void Update()
    {
        Move();   
    }

    void Move()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -6.5f) 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null) 
            {
                if (powerupID == 0) 
                {
                    player.ActiveTripleShot();
                } else if (powerupID == 1) 
                {
                    Debug.Log("Speed boost collected");
                } else if (powerupID ==  2) 
                {
                    Debug.Log("Shield Collected");
                }

            }
            Destroy(gameObject);
        }
    }
}
