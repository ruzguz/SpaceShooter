using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDetectionArea : MonoBehaviour
{
    private Enemy _enemy;

    // Start is called before the first frame update
    void Start()
    {
        _enemy = transform.parent.GetComponent<Enemy>();

        if (_enemy == null) 
        {
            Debug.LogError("Enemy is NULL");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Laser"))
        {
            _enemy.Dodge();
        }
    }
}
