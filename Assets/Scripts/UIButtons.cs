using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonType
{
    START_NEW_RACE,
    SKIP_RACE,
    RESET_RACE,
    SKIP_CELEBRATION,
    ALL_BUTTONS,
}

public class UIButtons : MonoBehaviour
{
    public event Action OnStartNewRace;
    public event Action OnSkipRace;
    public event Action OnResetRace;
    public event Action OnSkipCelebration;

    [SerializeField] protected Button StartNewRaceButton;
    [SerializeField] protected Button SkipRaceButton;
    [SerializeField] protected Button ResetRaceButton;
    [SerializeField] protected Button SkipCelebrationButton;
    [SerializeField] protected Button QuitButton;

    private void Awake()
    {
        StartNewRaceButton.onClick.AddListener(() => OnStartNewRace?.Invoke());
        SkipRaceButton.onClick.AddListener(() => OnSkipRace?.Invoke());
        ResetRaceButton.onClick.AddListener(() => OnResetRace?.Invoke());
        SkipCelebrationButton.onClick.AddListener(() => OnSkipCelebration?.Invoke());
        QuitButton.onClick.AddListener(() => Application.Quit());

        StartNewRaceButton.gameObject.SetActive(false);
        SkipRaceButton.gameObject.SetActive(false);
        ResetRaceButton.gameObject.SetActive(false);
        SkipCelebrationButton.gameObject.SetActive(false);
    }


    public void EnableButton(ButtonType buttonType, bool enable)
    {
        switch (buttonType)
        {
            case ButtonType.START_NEW_RACE:
                StartNewRaceButton.gameObject.SetActive(enable);
                break;
            case ButtonType.SKIP_RACE:
                SkipRaceButton.gameObject.SetActive(enable);
                break;
            case ButtonType.RESET_RACE:
                ResetRaceButton.gameObject.SetActive(enable);
                break;
            case ButtonType.SKIP_CELEBRATION:
                SkipCelebrationButton.gameObject.SetActive(enable);
                break;
            case ButtonType.ALL_BUTTONS:
                ResetRaceButton.gameObject.SetActive(enable);
                SkipRaceButton.gameObject.SetActive(enable);
                StartNewRaceButton.gameObject.SetActive(enable);
                SkipCelebrationButton.gameObject.SetActive(enable);
                break;
        }
    }
}
