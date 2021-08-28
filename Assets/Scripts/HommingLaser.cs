using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HommingLaser : MonoBehaviour
{
    [SerializeField]
    private Transform _nearestEnemy = null;
    private float _minDistance = Mathf.Infinity;
    private float _speed = 8f;
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
            // Vector3 laserPosition = transform.position;
            // Vector3 targetPosition = _nearestEnemy.transform.position;
            // targetPosition.z = laserPosition.z;

            // Vector3 vectorToTarget = targetPosition - laserPosition;

            // Vector3 rotatedVectorToTarget = Quaternion.Euler(0,0,90) * vectorToTarget;

            // Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);

            // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 5);


            transform.position = Vector3.MoveTowards(transform.position, _nearestEnemy.transform.position, _speed * Time.deltaTime);
            Quaternion rotation = Quaternion.LookRotation(transform.position  - _nearestEnemy.position, Vector3.forward);
            Debug.Log(rotation.z);
            // transform.rotation = rotation;
            transform.Rotate(0,0,rotation.x * 45);
        }
 
    }

}
