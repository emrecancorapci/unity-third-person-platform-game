using UnityEngine;

namespace Player.StateMachine
{
    public class JumpState : BaseState
    {
        public JumpState(PlayerController currentPlayer, StateFactory stateFactory)
            : base(currentPlayer, stateFactory) => IsBaseState = true;

        public override void EnterState()
        {
            Player.PlayerAnimator.SetBool(Player.IsJumpingHash, true);
            Player.walkParticle.Stop();
            Player.jumpParticle.Play();
            
           HandleJump();
        }
        
        protected override void UpdateState() => CheckTransition();

        protected override void ExitState() { /* Gonna use here for fall animation  */ }

        public override void CheckTransition()
        {
            /* We're gonna make a experiment hehe
            if (Player.OnGround || Player.OnWalkableEnvironment) 
                SwitchState(StateFactory.Grounded());
            */
            if (Player.Rigid.velocity.y < -0.02f)
            {
                SwitchState(StateFactory.Fall());
            }
        }
        
        public sealed override void InitSubState()
        {
            /* I think this is useless but let's not delete it for now.
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
            */
        }
        
        private void HandleJump()
        {
            // int jumpType = Player.JumpCount % Player.jumpForces.Count;
            float jumpForce = Player.jumpForces[0] * Player.Rigid.mass;

            Player.Rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Player.JumpCount++;
        }
    }
}
