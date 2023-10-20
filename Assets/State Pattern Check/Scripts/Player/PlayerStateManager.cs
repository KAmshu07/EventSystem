public class PlayerStateManager : StateManager<PlayerState>
{
    protected override void InitializeStates()
    {
        // Add your state instances to the state manager.
        AddState(PlayerState.Idle, new IdleState());
        AddState(PlayerState.Walking, new WalkingState());
        AddState(PlayerState.Running, new RunningState());

        // Set the initial state.
        CurrentState = States[PlayerState.Idle];
    }
}