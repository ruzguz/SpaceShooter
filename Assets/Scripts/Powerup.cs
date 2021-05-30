using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int powerupID;

    // Update is called once per frame.
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
                switch(powerupID)
                {
                    case 0:
                        player.ActiveTripleShot();
                        break;
                    case 1:
                        Debug.Log("Active Speed Boost");
                        break;
                    case 2:
                        Debug.Log("Active Shield");
                        break;
                    default:
                        Debug.Log("Invalid Powerup");
                        break;
                }
            }
            Destroy(gameObject);
        }
    }
}
