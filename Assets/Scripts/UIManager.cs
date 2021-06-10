﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private float _flikerDelay = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: 0";
        _gameOverText.gameObject.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        _scoreText.text =  "Score: " + score;
    }

    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _liveSprites[currentLives];
    }

    public void ShowGameOverScreen()
    {    
        StartCoroutine(FlikerGameOverText());
    }

    IEnumerator FlikerGameOverText()
    {
        while(true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(_flikerDelay);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(_flikerDelay);
        }
    }

}