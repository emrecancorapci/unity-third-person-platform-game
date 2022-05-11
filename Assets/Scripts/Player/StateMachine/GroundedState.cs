namespace Player.StateMachine
{
    public sealed class GroundedState : BaseState
    {
        public GroundedState(PlayerController currentPlayer, StateFactory stateFactory)
            : base(currentPlayer, stateFactory) => IsBaseState = true;

        public override void EnterState()
        {
            if (Player.IsRunning)
                Player.walkParticle.Play();
        }
        protected override void UpdateState() => CheckTransition();
        protected override void ExitState() { }
        public override void CheckTransition()
        {
            if (Player.IsJumpPressed)
                SwitchState(StateFactory.Jump());
            if (Player.Rigid.velocity.y <= -0.1)
                SwitchState(StateFactory.Fall());
        }

        public override void InitSubState()
        {
            // I think this is NOT useless. Because it initiates the sub-states.
            switch (Player.IsRunning)
            {
                case false:
                    SetSubState(StateFactory.Idle());
                    break;
                case true when !Player.IsSprinting:
                    SetSubState(StateFactory.Run());
                    break;
                case true when Player.IsSprinting:
                    SetSubState(StateFactory.Sprint());
                    break;
            }
        }
    }
}
