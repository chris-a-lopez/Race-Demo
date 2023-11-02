using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceInitalState : GameState
{
    public RaceInitalState(GameStart gameStart)
    {
        Initialize(gameStart);
    }

    public override void Enter()
    {
        base.Enter();
        GameStart.RaceableObjectManager.ForceToStart();
        GameStart.UIButtonEvents.EnableButton(ButtonType.START_NEW_RACE, true);
    }

    public override void Exit()
    {
        base.Exit();
        GameStart.UIButtonEvents.EnableButton(ButtonType.START_NEW_RACE, false);
    }

    #region Events
    protected override void RegisterEvents()
    {
        base.RegisterEvents();
        GameStart.UIButtonEvents.OnStartNewRace += StartNewRaceEvent;
    }

    protected override void UnRegisterEvents()
    {
        base.UnRegisterEvents();
        GameStart.UIButtonEvents.OnStartNewRace -= StartNewRaceEvent;
    }

    private void StartNewRaceEvent()
    {
        GameStart.ChangeState(GameStatesType.RACE_START);
    }
    #endregion

}
