using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : State<GameStart>
{
    protected GameStart GameStart { get; set; }

    public virtual void Initialize(GameStart gameStart)
    {
        GameStart = gameStart;
    }
    public virtual void Enter()
    {
        RegisterEvents();
    }

    public virtual void Exit()
    {
        UnRegisterEvents();
    }

    public virtual void FrameUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }
    protected virtual void RegisterEvents()
    {

    }

    protected virtual void UnRegisterEvents()
    {

    }

}
