using System;

namespace Player.StateMachine
{
    public class PlayerRunState : PlayerBaseState
    {
        public PlayerRunState(PlayerStateMachine currentPlayer, PlayerStateFactory playerStateFactory)
            : base(currentPlayer, playerStateFactory) {}
        
        public override void EnterState()
        {
            Player.PlayerAnimator.SetBool(Player.IsRunningHash, true);
            Player.PlayerAnimator.SetBool(Player.IsSprintingHash, false);

            Player.CurrentSpeed = Player.movementSpeed;
        }

        protected override void UpdateState() => CheckTransition();

        protected override void ExitState() { }

        public override void CheckTransition()
        {
            switch (Player.IsRunning)
            {
                case false:
                    SwitchState(StateFactory.Idle());
                    break;
                case true when Player.IsSprinting:
                    SwitchState(StateFactory.Sprint());
                    break;
            }
        }

        public override void InitSubState() { }
    }
}
