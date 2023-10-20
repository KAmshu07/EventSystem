using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// An abstract base class for defining states in a state machine.
/// </summary>
/// <typeparam name="EState">The enum type representing state keys.</typeparam>
public abstract class BaseState<EState> where EState : Enum
{
    /// <summary>
    /// The key that identifies this state.
    /// </summary>
    public EState StateKey { get; private set; }

    /// <summary>
    /// Constructor to set the state key.
    /// </summary>
    public BaseState(EState key)
    {
        StateKey = key;
    }

    /// <summary>
    /// Initialize the state with the state manager.
    /// </summary>
    public abstract void Initialize(StateManager<EState> manager);

    /// <summary>
    /// Called when entering this state.
    /// </summary>
    public abstract void EnterState();

    /// <summary>
    /// Called on every frame while in this state.
    /// </summary>
    public abstract void UpdateState();

    /// <summary>
    /// Called when exiting this state.
    /// </summary>
    public abstract void ExitState();

    /// <summary>
    /// Get the key of the next state to transition to.
    /// </summary>
    public abstract EState GetNextState();

    /// <summary>
    /// Handle collision events when a trigger collider is entered.
    /// </summary>
    public abstract void OnTriggerEnter(Collider other);

    /// <summary>
    /// Handle collision events when staying in a trigger collider.
    /// </summary>
    public abstract void OnTriggerStay(Collider other);

    /// <summary>
    /// Handle collision events when a trigger collider is exited.
    /// </summary>
    public abstract void OnTriggerExit(Collider other);

    /// <summary>
    /// Handle custom events to trigger state transitions.
    /// </summary>
    public abstract void HandleEvent(object eventData);

    /// <summary>
    /// Define a condition for state transitions.
    /// </summary>
    public abstract bool ShouldTransition();
}
