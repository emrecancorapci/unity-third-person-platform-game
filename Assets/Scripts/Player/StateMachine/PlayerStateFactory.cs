using System.Collections.Generic;

namespace Player.StateMachine
{
    public class PlayerStateFactory
    {
        private enum State
        {
            Idle,
            Run,
            Sprint,
            Jump,
            Grounded
        }
        private readonly Dictionary<State, PlayerBaseState> _states;

        public PlayerStateFactory(PlayerStateMachine currentContext)
        {
            _states = new Dictionary<State, PlayerBaseState>
            {
                {State.Idle, new PlayerIdleState(currentContext, this)},
                {State.Run, new PlayerRunState(currentContext, this)},
                {State.Sprint, new PlayerSprintState(currentContext, this)},
                {State.Jump, new PlayerJumpState(currentContext, this)},
                {State.Grounded, new PlayerGroundedState(currentContext, this)},
            };
        }

        public PlayerBaseState Idle() => _states[State.Idle];
        public PlayerBaseState Run() => _states[State.Run];
        public PlayerBaseState Sprint() => _states[State.Sprint];
        public PlayerBaseState Jump() => _states[State.Jump];
        public PlayerBaseState Grounded() => _states[State.Grounded];
    }
}
