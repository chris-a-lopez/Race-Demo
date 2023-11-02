using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStartState : GameState
{
    public RaceStartState(GameStart gameStart)
    {
        Initialize(gameStart);
    }

    public override void Enter()
    {
        base.Enter();
        // Race Starting Initialize the race
        GameStart.InitializeRace();
        GameStart.ChangeState(GameStatesType.RACE_PLAYING);
    }

}
