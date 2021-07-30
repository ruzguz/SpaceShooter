using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private bool _gameIsOver = false;
    [SerializeField]
    private Animator _cameraAnim;
    private int _waveNumber = 0;
    [SerializeField]
    private GameObject _spawnManger;
    

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

        if (Input.GetKey(KeyCode.Escape)) 
        {
            //Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    public void FinishGame()
    {
        _gameIsOver =  true;
    }

    void ResetGame()
    {
        SceneManager.LoadScene(1); // Load Game Scene 
    }

    public void ShakeCamera()
    {
        _cameraAnim.Play("Camera_anim");
    }
}
