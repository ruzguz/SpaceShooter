using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _resetGameText;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private float _flikerDelay = 0.5f;
    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private Slider _thrusterSlider;
    [SerializeField]
    private Text _waveText;
    [SerializeField]
    private SpawnManager _spawnManager;


    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: 0";
        _gameOverText.gameObject.SetActive(false);
        _resetGameText.gameObject.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        _scoreText.text =  "Score: " + score;
    }

    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _liveSprites[currentLives];
    }

    public void UpdateAmmo(int ammo, int maxAmmo) 
    {

        _ammoText.text = "Ammo: " + ammo+ "/" + maxAmmo;
    }

    public void ShowGameOverScreen()
    {    
        StartCoroutine(FlikerGameOverText());
        _resetGameText.gameObject.SetActive(true);
    }

    public void UpdateThrusterFuel(float value)
    {
        _thrusterSlider.value = value;
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

    public void EnableWaveText()
    {
        StartCoroutine(WaveTextRoutine());
    }

    IEnumerator WaveTextRoutine()
    {
        _waveText.text = "Wave "+_spawnManager.waveCounter;
        _waveText.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        _waveText.gameObject.SetActive(false);
    }

}
