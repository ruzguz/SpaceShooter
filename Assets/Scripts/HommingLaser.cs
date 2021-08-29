using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HommingLaser : MonoBehaviour
{
    [SerializeField]
    private Transform _nearestEnemy = null;
    private float _minDistance = Mathf.Infinity;
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private float _rotationSpeed = 45f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update() {


        if (_nearestEnemy == null) 
        {
            // Get all enemies
            // for each enemy in enemies 
            //     if enemy distance is lower that min distance 
            //          nearest enemy = current enemy 

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) 
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance < _minDistance) 
                {
                    _minDistance = distance;
                    _nearestEnemy = enemy.transform;
                }
            }
        } else 
        {
            transform.position = Vector3.MoveTowards(transform.position, _nearestEnemy.transform.position, _speed * Time.deltaTime);
            transform.Rotate(0,0, Time.time * _rotationSpeed);
        }
 
    }

}
