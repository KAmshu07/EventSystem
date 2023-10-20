using UnityEngine;

public class WalkingState : BaseState<PlayerState>
{
    public WalkingState() : base(PlayerState.Walking) { }

    public override void Initialize(StateManager<PlayerState> manager)
    {
        // Initialize state-specific data here if needed.
    }

    public override void EnterState()
    {
        // Add logic for entering the Walking state.
        Debug.Log("Entering Walking State");
    }

    public override void UpdateState()
    {
        // Add logic for the Walking state.
        Debug.Log("Walking State Update");
    }

    public override void ExitState()
    {
        // Add logic for exiting the Walking state.
        Debug.Log("Exiting Walking State");
    }

    public override PlayerState GetNextState()
    {
        // Define the conditions for transitioning to other states.
        if (!Input.GetKey(KeyCode.W))
        {
            return PlayerState.Idle;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            return PlayerState.Running;
        }
        return StateKey;
    }

    public override void OnTriggerEnter(Collider other)
    {
        // Handle collisions for the Walking state.
    }

    public override void OnTriggerStay(Collider other)
    {
        // Handle collisions for the Walking state.
    }

    public override void OnTriggerExit(Collider other)
    {
        // Handle collisions for the Walking state.
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