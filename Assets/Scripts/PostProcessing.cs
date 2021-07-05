using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume _gamePostProcessVolume;
    private ChromaticAberration _speedEffect;

    private void Start() 
    {
        if (_gamePostProcessVolume == null) 
        {
            Debug.LogError("Game Post Process Volune is NULL");
        }    

        _gamePostProcessVolume.profile.TryGetSettings(out _speedEffect);
        if (_speedEffect == null) 
        {
            Debug.LogError("Speed Effect is NULL");
        }
    }

    public void SetSpeedEffect(float value)
    {
        // 0.5 = active, 0 = deactive
        _speedEffect.intensity.value = value;
    }
}
