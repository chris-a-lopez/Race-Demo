using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private RaceGameInput raceGameInput;
    public event Action OnReset;
    public event Action OnSkip;

    private void Awake()
    {
        raceGameInput = new RaceGameInput();
        raceGameInput.GameInput.Skip.performed += SkipAction;
        raceGameInput.GameInput.Reset.performed += ResetAction;
    }

    private void SkipAction(InputAction.CallbackContext obj)
    {
        OnSkip?.Invoke();
    }


    private void ResetAction(InputAction.CallbackContext obj)
    {
        OnReset?.Invoke();
    }


    private void OnEnable()
    {
        raceGameInput.Enable();
    }

    private void OnDisable()
    {
        raceGameInput.Disable();
    }

}
