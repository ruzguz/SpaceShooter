using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _extraSpeed = 0f;
    [SerializeField]
    private int powerupID; // 0 = Triple Shot, 1 = Speed Boost, 2 = Shield, 3 = Extra Ammo, 4 = Add Live, 5 = Combustion Laser, 
                           //6 = Hack System, 7 = Slow Down, 8 = Zero Ammo

    // Update is called once per frame.
    void Update()
    {

        // Pickup Collect Extra Speed
        if (Input.GetKey(KeyCode.C)) 
        {
            _extraSpeed = 6f;
        } else 
        {
            _extraSpeed = 0;
        }

        Move();   
    }

    void Move()
    {
        transform.Translate(Vector3.down * (_speed + _extraSpeed) * Time.deltaTime);

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
            player.PlayPowerupAudio();
            if (player != null) 
            {
                switch(powerupID)
                {
                    case 0:
                        player.ActiveTripleShot();
                        break;
                    case 1:
                        player.ActivateSpeedBoost();
                        break;
                    case 2:
                        player.ActivateShield();
                        break;
                    case 3:
                        player.AddAmmo();
                        break;
                    case 4: 
                        player.Heal();
                        break;
                    case 5:
                        player.ActivateCombustionLaser();
                        break;
                    case 6:
                        player.EnableHakingMovement();
                        break;
                    case 7:
                        player.ActiveSlowDown();
                        break;
                    case 8: 
                        player.ActiveZeroAmmo();
                        break;
                    default:
                        Debug.Log("Invalid Powerup");
                        break;
                }
            }
            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy Laser")) 
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy")) 
        {
            Destroy(gameObject);
        }
    }
}
