using UnityEngine;

namespace Player.StateMachine
{
    public class PlayerJumpState : PlayerBaseState
    {
        public PlayerJumpState(PlayerStateMachine currentPlayer, PlayerStateFactory playerStateFactory)
            : base(currentPlayer, playerStateFactory) => IsBaseState = true;

        public override void EnterState()
        {
            Player.PlayerAnimator.SetBool(Player.IsJumpingHash, true);
            Player.walkParticle.Stop();
            Player.jumpParticle.Play();
            
            int jumpType = Player.JumpCount % Player.jumpForces.Count;
            float jumpForce = Player.jumpForces[jumpType] * Player.Rigid.mass;
            
            Player.Rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Player.JumpCount++;
        }

        protected override void UpdateState() => CheckTransition();

        protected override void ExitState() => Player.PlayerAnimator.SetBool(Player.IsJumpingHash, false);

        public override void CheckTransition()
        {
            if (Player.OnGround || Player.OnWalkableEnvironment) 
                SwitchState(StateFactory.Grounded());
        }

        public sealed override void InitSubState()
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
