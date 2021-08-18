using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            Player player = other.transform.GetComponent<Player>();

            player.Damage();
            Destroy(gameObject);
        }

        if (other.CompareTag("Laser")) 
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
