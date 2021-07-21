using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombustionLaser : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5f;

    private float _limit = 6f;
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_spriteRenderer == null)
        {
            Debug.LogError("Sprite Renderer is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= _limit) 
        {
            Explode();
        }
    }
    public void Explode()
    {
        _speed = 0;
        _spriteRenderer.enabled = false;
        StartCoroutine(ExplodeRoutine());
    }

    IEnumerator ExplodeRoutine()
    {
        foreach (Transform combustion in transform) 
        {
            combustion.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject, 3);
    }
}
