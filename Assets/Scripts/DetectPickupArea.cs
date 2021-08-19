using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPickupArea : MonoBehaviour
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

    IEnumerator ShootDelayRoutine()
    {
        _enemy.shootDelay = 0.3f;
        yield return new WaitForSeconds(2f);
        _enemy.shootDelay = 3f;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Powerup")) 
        {
            Debug.Log("Detecting poewrup");
            StartCoroutine(ShootDelayRoutine());
        }    
    }
}
