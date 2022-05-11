using System.Collections.Generic;

namespace Player.StateMachine
{
    public class StateFactory
    {
        private enum State
        {
            Idle,
            Run,
            Sprint,
            Jump,
            Grounded,
            Fall
        }
        private readonly Dictionary<State, BaseState> _states;

        public StateFactory(PlayerController currentContext)
        {
            _states = new Dictionary<State, BaseState>
            {
                {State.Idle, new IdleState(currentContext, this)},
                {State.Run, new RunState(currentContext, this)},
                {State.Sprint, new SprintState(currentContext, this)},
                {State.Jump, new JumpState(currentContext, this)},
                {State.Grounded, new GroundedState(currentContext, this)},
                {State.Fall, new FallState(currentContext, this)},
            };
        }

        // SubStates
        public BaseState Idle() => _states[State.Idle];
        public BaseState Run() => _states[State.Run];
        public BaseState Sprint() => _states[State.Sprint];
        
        // MainStates
        public BaseState Jump() => _states[State.Jump];
        public BaseState Grounded() => _states[State.Grounded];
        public BaseState Fall() => _states[State.Fall];
    }
}
