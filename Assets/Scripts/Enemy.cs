using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // General vars
    [SerializeField] 
    private float _speed = 4f;
    private float _respawnYPosition = 8f;
    private float _bottomLimit = -5.5f;
    private float _horizontalLimit = 9.5f;
    // References
    Player _player;
    private Animator _enemyAnim;

    void Start() {

        _player = GameObject.Find("Player").GetComponent<Player>();
        
        if (_player == null) 
        {
            Debug.Log("Enemy Script: Player is null");
        }    

        _enemyAnim = this.GetComponent<Animator>();
        
        if (_enemyAnim == null) 
        {
            Debug.Log("Enemy Script: Enemy Animator is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move enemy down at 4 meters per second
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        // If enemy goes out of the camera, respawn the enemy at the top i a new random x position
        if (transform.position.y <= _bottomLimit) 
        {
            float newXposition = Random.Range(-_horizontalLimit, _horizontalLimit);
            transform.position = new Vector3(newXposition, _respawnYPosition, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

        // Collision with player
        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null) {
                player.Damage();
                this.Explode();
            }
        }

        // Collision with laser
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            _player.UpdateScore(10);
            this.Explode();
        }

        if (other.CompareTag("Shield")) 
        {
            other.gameObject.SetActive(false);
            _player.UpdateScore(10);
            this.Explode();
        }
    }

    void Explode()
    {
        _enemyAnim.SetTrigger("OnEnemyDeath");
        _speed = 0;
        Destroy(this.gameObject, 3f);
    }
}