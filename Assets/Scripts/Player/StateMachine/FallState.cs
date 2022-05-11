namespace Player.StateMachine
{
    public class FallState : BaseState
    {
        private float _groundedVelocityLimit;

        public FallState(PlayerController currentPlayer, StateFactory stateFactory)
            : base(currentPlayer, stateFactory) => IsBaseState = true;

        public override void EnterState()
        {
            Player.walkParticle.Stop();
        }

        protected override void UpdateState()
        {
            // If I change rigidbody gravity gonna be right here
            CheckTransition();
        }

        protected override void ExitState() => Player.PlayerAnimator.SetBool(Player.IsJumpingHash, false);

        public override void CheckTransition()
        {
            if (Player.Rigid.velocity.y > PlayerController.GroundedVelocityLimit)
                SwitchState(StateFactory.Grounded());
        }

        public override void InitSubState() { }
    }
}
