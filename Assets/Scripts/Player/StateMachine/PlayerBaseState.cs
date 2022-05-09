using UnityEngine;

namespace Player.StateMachine
{
    public abstract class PlayerBaseState
    {
        protected PlayerBaseState(PlayerStateMachine currentPlayer, PlayerStateFactory playerStateFactory)
        {
            Player = currentPlayer;
            StateFactory = playerStateFactory;
        }
        
        protected bool IsBaseState = false;
        protected readonly PlayerStateMachine Player;
        protected readonly PlayerStateFactory StateFactory;
        private PlayerBaseState _currentMainState;
        private PlayerBaseState _currentSubState;

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
        protected void SwitchState(PlayerBaseState newState)
        {
            ExitState();
            newState.EnterState();

            if (IsBaseState)
                Player.CurrentState = newState;
            else
                Player.CurrentState.SetSubState(newState);
        }
        private void SetMainState(PlayerBaseState newMainState)
        {
            _currentMainState = newMainState;
        }
        protected void SetSubState(PlayerBaseState newSubState)
        {
            _currentSubState = newSubState;
            newSubState.SetMainState(this);
        }
    }
}
