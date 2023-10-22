using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
    
    protected IState currentState;
    
    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        // ? revisar si el estado es null si no es asi ejecutara exit 
        currentState = newState;
        currentState.Enter();
    }

    public void HandleInput()
    {
    currentState?.HandleInput();
    }

    public void Update()
    {
    currentState?.Update();
    }
    public void PhysicsUpdates()
    {
    currentState?.PhysicsUpdates();
    }

    public void OnAnimationEnterEvent()
    {
        currentState?.OnAnimationEnterEvent();
    }

    public void OnAnimationTransitionEvent()
    {
        currentState?.OnAnimationTransitionEvent();
    }

    public void OnAnimationExitEvent()
    {
        currentState?.OnAnimationExitEvent();
    }

    public void OnTriggerEnter(Collider collider)
    {
        currentState?.OnTriggerEnter(collider);
    }

    public void OnTriggerExit(Collider collider)
    {
        currentState?.OnTriggerExit(collider);
    }
}
