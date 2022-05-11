namespace Player.StateMachine
{
    public class RunState : BaseState
    {
        public RunState(PlayerController currentPlayer, StateFactory stateFactory)
            : base(currentPlayer, stateFactory) {}
        
        public override void EnterState()
        {
            Player.PlayerAnimator.SetBool(Player.IsRunningHash, true);
            Player.PlayerAnimator.SetBool(Player.IsSprintingHash, false);
            if (Player.OnGround || Player.OnWalkableEnvironment)
                Player.walkParticle.Play();
            
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
