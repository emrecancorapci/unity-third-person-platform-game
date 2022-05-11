namespace Player.StateMachine
{
    public abstract class BaseState
    {
        protected BaseState(PlayerController currentPlayer, StateFactory stateFactory)
        {
            Player = currentPlayer;
            StateFactory = stateFactory;
        }
        
        protected bool IsBaseState = false;
        protected readonly PlayerController Player;
        protected readonly StateFactory StateFactory;
        private BaseState _currentMainState;
        private BaseState _currentSubState;

        public abstract void EnterState();
        protected abstract void UpdateState();
        protected abstract void ExitState();
        public abstract void CheckTransition();
        public abstract void InitSubState();

        public void UpdateStates()
        {
            UpdateState();
            _currentSubState?.UpdateState();
        }
        protected void SwitchState(BaseState newState)
        {
            ExitState();
            newState.EnterState();

            if (IsBaseState)
                Player.CurrentState = newState;
            else
                Player.CurrentState.SetSubState(newState);
        }
        protected void SetSubState(BaseState newSubState)
        {
            _currentSubState = newSubState;
            _currentMainState = this;
        }
    }
}
