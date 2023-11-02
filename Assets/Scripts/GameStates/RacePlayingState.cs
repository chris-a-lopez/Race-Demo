using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacePlayingState : GameState
{
    public RacePlayingState(GameStart gameStart)
    {
        Initialize(gameStart);
    }

    public override void Enter()
    {
        base.Enter();
        GameStart.RaceableObjectManager.StartRace();
        GameStart.UIButtonEvents.EnableButton(ButtonType.SKIP_RACE, true);
        GameStart.UIButtonEvents.EnableButton(ButtonType.RESET_RACE, true);
    }

    public override void Exit()
    {
        base.Exit();
        GameStart.UIButtonEvents.EnableButton(ButtonType.SKIP_RACE, false);
        GameStart.UIButtonEvents.EnableButton(ButtonType.RESET_RACE, false);
    }

    #region Events
    protected override void RegisterEvents()
    {
        base.RegisterEvents();
        GameStart.GameInput.OnReset += ResetEvent;
        GameStart.UIButtonEvents.OnResetRace += ResetEvent;
        GameStart.GameInput.OnSkip += SkipRaceEvent;
        GameStart.UIButtonEvents.OnSkipRace += SkipRaceEvent;
        GameStart.RaceableObjectManager.OnAllRaceableObjectFinishRace += AllRaceObjectFinish;
    }

    protected override void UnRegisterEvents()
    {
        base.UnRegisterEvents();
        GameStart.GameInput.OnReset -= ResetEvent;
        GameStart.UIButtonEvents.OnResetRace -= ResetEvent;
        GameStart.GameInput.OnSkip -= SkipRaceEvent;
        GameStart.UIButtonEvents.OnSkipRace -= SkipRaceEvent;
        GameStart.RaceableObjectManager.OnAllRaceableObjectFinishRace -= AllRaceObjectFinish;

    }

    private void SkipRaceEvent()
    {
        GameStart.ChangeState(GameStatesType.RACE_END);
    }

    private void ResetEvent()
    {
        GameStart.ChangeState(GameStatesType.RACE_START);
    }

    private void AllRaceObjectFinish()
    {
        GameStart.ChangeState(GameStatesType.RACE_END);
    }


    #endregion
}
