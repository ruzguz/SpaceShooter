using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            Player player = other.GetComponent<Player>();
            
            if (player != null) 
            {
                player.Damage();
                Destroy(gameObject);
            }
        }    


        if (other.CompareTag("Laser")) 
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
