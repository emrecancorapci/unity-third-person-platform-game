namespace Player.StateMachine
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerStateMachine currentPlayer, PlayerStateFactory playerStateFactory)
            : base(currentPlayer, playerStateFactory)
        {}
        public override void EnterState()
        { 
            Player.PlayerAnimator.SetBool(Player.IsRunningHash, false);
            Player.PlayerAnimator.SetBool(Player.IsSprintingHash, false);
            Player.walkParticle.Stop();
        }
        protected override void UpdateState() => CheckTransition();
        protected override void ExitState() { }
        public override void CheckTransition()
        {
            if (Player.IsRunning)
            {
                switch (Player.IsSprinting)
                {
                    case false:
                        SwitchState(StateFactory.Run());
                        break;
                    case true:
                        SwitchState(StateFactory.Sprint());
                        break;
                }
            }

        }

        public override void InitSubState() { }
        
    }
}
