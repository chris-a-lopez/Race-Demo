using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class RaceEndState : GameState
{
    const int WAIT_TIME = 3000;
    const int DISPLAY_WIN_WAIT = 2000;
    CancellationTokenSource cancellationTokenSource;

    public RaceEndState(GameStart gameStart)
    {
        Initialize(gameStart);
      
    }

    public override void Enter()
    {
        cancellationTokenSource = new CancellationTokenSource(); 
        GameStart.RaceableObjectManager.ForceToEnd();
        GameStart.LanesText.UpdateLanesText(GameStart.RaceableObjectManager);
        RevealWinners(cancellationTokenSource.Token);
        GameStart.UIButtonEvents.EnableButton(ButtonType.SKIP_CELEBRATION, true);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        GameStart.LanesText.SetLanesVisibility(false);
        GameStart.UIButtonEvents.EnableButton(ButtonType.SKIP_CELEBRATION, false);
    }

    private async Task RevealWinners(CancellationToken token)
    {
        List<RaceableObject> listOfRaceableObject = GameStart.RaceableObjectManager.GetListOfRaceableObject();

        token.ThrowIfCancellationRequested();
        for (int i = 0; i < listOfRaceableObject.Count; i++)
        {
            int index = listOfRaceableObject.FindIndex(a => a.Order == i);
            GameStart.LanesText.SetLanesVisibility(true, index);
            await Task.Delay(DISPLAY_WIN_WAIT, token);
        }
        ChangeState(token);
    }

    private async Task ChangeState(CancellationToken token)
    {
        await Task.Delay(WAIT_TIME, token);
        GameStart.ChangeState(GameStatesType.RACE_INITAL);
    }

    #region Events
    protected override void RegisterEvents()
    {
        base.RegisterEvents();
        GameStart.GameInput.OnSkip += SkipRevealWinner;
        GameStart.UIButtonEvents.OnSkipCelebration += SkipRevealWinner;
    }

    protected override void UnRegisterEvents()
    {
        base.UnRegisterEvents();
        GameStart.GameInput.OnSkip -= SkipRevealWinner;
        GameStart.UIButtonEvents.OnSkipCelebration -= SkipRevealWinner;
    }

    private void SkipRevealWinner()
    {
        cancellationTokenSource.Cancel();
        GameStart.ChangeState(GameStatesType.RACE_INITAL);
    }
    #endregion

}
