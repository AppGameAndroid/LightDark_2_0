using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter();
    public void Exit();
    public void HandleInput();

    public void Update();
    public void PhysicsUpdates();

    public void OnAnimationEnterEvent();
    // puede hacer hacer uso 

    public void OnAnimationExitEvent();
    public void OnAnimationTransitionEvent();
}
