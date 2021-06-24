using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    private float _destroyLimit = 8f;
    private AudioSource _audio;

    // Update is called once per frame
    void Update()
    {
        // Move laser up
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        // Destroy laser
        if (transform.position.y > _destroyLimit) 
        {
            Destroy(gameObject);
        }
    }


    void OnDestroy()
    {
        Transform parent =  this.transform.parent;
        if (parent != null && parent.childCount == 1) 
        {
            Destroy(parent.gameObject); 
        }    
    }
}
