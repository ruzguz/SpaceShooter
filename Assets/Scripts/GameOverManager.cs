using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private int _yourScore, _bestScore;
    [SerializeField]
    private Text _yourScoreText, _bestScoreText;
    

    // Start is called before the first frame update
    void Start()
    {
        // Get score values
        _yourScore = PlayerPrefs.GetInt("YourScore");
        _bestScore = PlayerPrefs.GetInt("BestScore");

        // Check for new record
        if (_yourScore > _bestScore) 
        {
            PlayerPrefs.SetInt("BestScore", _yourScore);
            _bestScore = _yourScore;
        }

        // Change UI Text
        _yourScoreText.text = "Your Score: " + _yourScore;
        _bestScoreText.text = "Best Score: " + _bestScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }
}
