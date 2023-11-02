using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface State<T>
{
    public void Initialize(T Owner);
    public void Enter();

    public void Exit();

    public void FrameUpdate();

    public void PhysicsUpdate();
}

