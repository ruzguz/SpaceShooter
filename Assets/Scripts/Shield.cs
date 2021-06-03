using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) 
        {
            Destroy(other.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
