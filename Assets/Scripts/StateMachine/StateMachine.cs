using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public sealed class StateMachine<T>
{
    public StateMachine()
    {
    }

    /// <summary>
    /// Current state the object is in
    /// </summary>       
    public State<T> CurrentState;

    public void Initialize(State<T> newState)
    {
        CurrentState = newState;
        CurrentState.Enter();
    }


    /// <summary>
    /// Change to a new state
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(State<T> newState)
    {

        // Exit the current state
        CurrentState.Exit();

        // Change to the new state
        CurrentState = newState;

        // enter the new state
        CurrentState.Enter();
    }

    public bool IsInState(State<T> t)
    {
        return CurrentState == t;
    }
}
