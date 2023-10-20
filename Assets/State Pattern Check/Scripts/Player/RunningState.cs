using UnityEngine;

public class RunningState : BaseState<PlayerState>
{
    public RunningState() : base(PlayerState.Running) { }

    public override void Initialize(StateManager<PlayerState> manager)
    {
        // Initialize state-specific data here if needed.
    }

    public override void EnterState()
    {
        // Add logic for entering the Running state.
        Debug.Log("Entering Running State");
    }

    public override void UpdateState()
    {
        // Add logic for the Running state.
        Debug.Log("Running State Update");
    }

    public override void ExitState()
    {
        // Add logic for exiting the Running state.
        Debug.Log("Exiting Running State");
    }

    public override PlayerState GetNextState()
    {
        // Define the conditions for transitioning to other states.
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            return PlayerState.Walking;
        }
        return StateKey;
    }

    public override void OnTriggerEnter(Collider other)
    {
        // Handle collisions for the Running state.
    }

    public override void OnTriggerStay(Collider other)
    {
        // Handle collisions for the Running state.
    }

    public override void OnTriggerExit(Collider other)
    {
        // Handle collisions for the Running state.
    }

    public override void HandleEvent(object eventData)
    {
        // Handle custom events for state transitions.
    }

    public override bool ShouldTransition()
    {
        // Implement custom conditions for state transitions.
        return true;
    }
}

