using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private int _lives = 3;
    private SpriteRenderer _spriteRenderer;

    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_spriteRenderer == null) 
        {
            Debug.LogError("Sprite Renderer is NULL");
        }
    }

    public void Damage() 
    {
        _lives--;

        if (_lives <= 0) 
        {
            Destroy(gameObject);
            return;
        }

        Color auxColor = _spriteRenderer.color;

        switch(_lives)
        {
            case 2:
                auxColor.a = 0.1f;
                break;
            case 1:
                auxColor = Color.red;
                break;
        }

        _spriteRenderer.color = auxColor;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy Laser")) 
        {
            Destroy(other.gameObject);
            Damage();
        }
    }
}
