using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalPostProcessingController : MonoBehaviour
{
    private const float VIGNETTE_SPEED = 2;
    private const float VIGNETTE_INTESITY = 0.6f;
    
    private Volume _volume;
    private Vignette _vignette;
    private bool _rotating;
    
    void Awake()
    {
        InputManager.Instance.OnRotateStartedCallback += () => _rotating = true;
        InputManager.Instance.OnRotateEndedCallback += () => _rotating = false;

        _volume = GetComponent<Volume>();
        _volume.profile.TryGet(out _vignette);
    }

    void Update()
    {
        if (_rotating == true && _vignette.intensity.value < VIGNETTE_INTESITY)
        {
            _vignette.intensity.value = Mathf.MoveTowards(_vignette.intensity.value, VIGNETTE_INTESITY, Time.deltaTime * VIGNETTE_SPEED);
        }
        else if(_rotating == false && _vignette.intensity.value > 0)
        {
            _vignette.intensity.value = Mathf.MoveTowards(_vignette.intensity.value, 0, Time.deltaTime * VIGNETTE_SPEED);
        }
    }
}
