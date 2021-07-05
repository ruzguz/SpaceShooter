﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    private AudioSource _explosionAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        _explosionAudioSource = GetComponent<AudioSource>();
        _explosionAudioSource.Play();
        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}