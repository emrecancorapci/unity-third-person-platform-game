namespace Player.StateMachine
{
    public sealed class PlayerGroundedState : PlayerBaseState
    {
        public PlayerGroundedState(PlayerStateMachine currentPlayer, PlayerStateFactory playerStateFactory)
            : base(currentPlayer, playerStateFactory) => IsBaseState = true;

        public override void EnterState() { }

        protected override void UpdateState() => CheckTransition();

        protected override void ExitState() { }

        public override void CheckTransition()
        {
            if (Player.IsJumpPressed)
            {
                SwitchState(StateFactory.Jump());
            }
        }

        public override void InitSubState()
        {
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
