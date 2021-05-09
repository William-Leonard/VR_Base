using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [SerializeField]
    private InputActionReference _turnActionReference = null;

    public event Action OnRotateStartedCallback;
    public event Action OnRotateEndedCallback;

    private bool _started;

    void Awake()
    {
        _turnActionReference.action.started += context =>
        {
            _started = true;
            OnRotateStartedCallback?.Invoke();
        };
        
        _turnActionReference.action.canceled += context =>
        {
            if (_started == true)
            {
                OnRotateEndedCallback?.Invoke();
            }
        };
    }
}
