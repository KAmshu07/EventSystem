using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// An abstract base class for managing states and transitions in a state machine.
/// </summary>
/// <typeparam name="EState">The enum type representing state keys.</typeparam>
public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    /// <summary>
    /// A dictionary to store states by their state keys.
    /// </summary>
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();

    /// <summary>
    /// The current state of the state machine.
    /// </summary>
    protected BaseState<EState> CurrentState;

    /// <summary>
    /// Flag to indicate if a state transition is in progress.
    /// </summary>
    protected bool IsTransitioningState = false;

    /// <summary>
    /// A stack to manage the state history for returning to previous states.
    /// </summary>
    protected Stack<EState> StateStack = new Stack<EState>();

    /// <summary>
    /// Add a state to the state machine.
    /// </summary>
    public void AddState(EState stateKey, BaseState<EState> state)
    {
        States[stateKey] = state;
    }

    /// <summary>
    /// Initialize states and transition to the initial state.
    /// </summary>
    public void Start()
    {
        InitializeStates();
        TransitionToState(CurrentState.StateKey);
    }

    /// <summary>
    /// Update the state machine, handling state transitions and updates.
    /// </summary>
    public void Update()
    {
        if (!IsTransitioningState)
        {
            EState nextStateKey = CurrentState.GetNextState();
            if (nextStateKey.Equals(CurrentState.StateKey) && CurrentState.ShouldTransition())
            {
                CurrentState.UpdateState();
            }
            else
            {
                TransitionToState(nextStateKey);
            }
        }
    }

    /// <summary>
    /// Transition to a new state.
    /// </summary>
    public void TransitionToState(EState stateKey)
    {
        if (IsTransitioningState) return;

        if (States.TryGetValue(stateKey, out BaseState<EState> newState))
        {
            IsTransitioningState = true;
            CurrentState.ExitState();
            StateStack.Push(CurrentState.StateKey); // Push the current state onto the stack.
            CurrentState = newState;
            CurrentState.EnterState();
            IsTransitioningState = false;
        }
        else
        {
            Debug.LogError("State not found: " + stateKey);
        }
    }

    /// <summary>
    /// Handle collision events when a trigger collider is entered.
    /// </summary>
    public void OnTriggerEnter(Collider other)
    {
        CurrentState.OnTriggerEnter(other);
    }

    /// <summary>
    /// Handle collision events when staying in a trigger collider.
    /// </summary>
    public void OnTriggerStay(Collider other)
    {
        CurrentState.OnTriggerStay(other);
    }

    /// <summary>
    /// Handle collision events when a trigger collider is exited.
    /// </summary>
    public void OnTriggerExit(Collider other)
    {
        CurrentState.OnTriggerExit(other);
    }

    /// <summary>
    /// Handle custom events to trigger state transitions.
    /// </summary>
    public void HandleEvent(object eventData)
    {
        CurrentState.HandleEvent(eventData);
    }

    /// <summary>
    /// Initialize all states in the state machine.
    /// </summary>
    protected abstract void InitializeStates();
}
