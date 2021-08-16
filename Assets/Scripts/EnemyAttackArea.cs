using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackArea : MonoBehaviour
{

    private Enemy _enemy;

    // Start is called before the first frame update
    void Start()
    {
        _enemy = transform.parent.GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) 
        {
            _enemy.Ram();
        }
    }
}
