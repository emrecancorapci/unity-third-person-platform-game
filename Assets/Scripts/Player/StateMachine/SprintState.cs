namespace Player.StateMachine
{
    public class SprintState : BaseState
    {
        public SprintState(PlayerController currentPlayer, StateFactory stateFactory) 
            : base(currentPlayer, stateFactory) {}
        public override void EnterState()
        {
            Player.PlayerAnimator.SetBool(Player.IsRunningHash, true);
            Player.PlayerAnimator.SetBool(Player.IsSprintingHash, true);
            
            if (Player.OnGround || Player.OnWalkableEnvironment)
                Player.walkParticle.Play();
            
            Player.CurrentSpeed = Player.movementSpeed * Player.sprintMultiplier;
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
                case true when !Player.IsSprinting:
                    SwitchState(StateFactory.Run());
                    break;
            }
        }
        public override void InitSubState() { }
    }
}
