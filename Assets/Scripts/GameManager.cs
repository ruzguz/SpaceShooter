using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private bool _gameIsOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameIsOver ==  true && Input.GetKeyDown(KeyCode.R)) 
        {
            ResetGame();
        }
    }

    public void FinishGame()
    {
        _gameIsOver =  true;
    }

    void ResetGame()
    {
        SceneManager.LoadScene(0); // Load Game Scene 
    }
}
