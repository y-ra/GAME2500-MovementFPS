using UnityEngine;
//a factory to change the players state 
public class PlayerStateFactory : MonoBehaviour
{
    [Header ("Factory info")]
    public PlayerController context;
    public Rigidbody rigid;

    [Header ("Max State Speeds")]
    public float walkSpeed;
    public float runSpeed;
    public float airSpeed;
    public float dashSpeed;
    public float wallSpeed;
    public float slideSpeed;
    public float grappleSpeed;

    [Header ("Additional State Variables")]
    public float airStrafe;
    public float maxFallSpeed;
    public float maxClimbSpeed;
    public float wallJumpForce;
    public float extraSlideGrav;
    public float slideStrafe;
    public float slideMinAngle;
    public float leaveSlideSpeed;
    public float slideCooldownTimer;

    [Header ("Grapple state stuff")]
    public float grappleRopeLen;
    public float grappleYankForce;
    public float grappleStrafe;
    public float grappleSpringConstant;
    public float grappleSpringDamper;

    public PlayerStateFactory() {
    }


    public PlayerBaseState Walk(){
        return new PlayerWalkState(context, this, rigid, walkSpeed);
    }

    public PlayerBaseState Run(){
        return new PlayerRunState(context, this, rigid, runSpeed);
    }

    public PlayerBaseState Air(){
        return new PlayerAirState(context, this, rigid, airSpeed, airStrafe, maxFallSpeed, maxClimbSpeed);
    }

    public PlayerBaseState Dash(Vector3 direction, float timer){
        return new PlayerDashState(context, this, rigid, direction, timer, dashSpeed);
    }

    public PlayerBaseState Wallrun(){
        return new PlayerWallrunState(context, this, rigid, wallSpeed, wallJumpForce);
    }

    public PlayerBaseState Slide(){
        return new PlayerSlideState(context, this, rigid, slideSpeed, extraSlideGrav, slideStrafe, slideMinAngle, leaveSlideSpeed, slideCooldownTimer);
    }

    public PlayerBaseState WallDismount() {
        return new PlayerDismountWallState(context, this, rigid, airSpeed, airStrafe, maxFallSpeed, maxClimbSpeed);
    }

    public PlayerBaseState Grapple(GrapplePoint point) {
        return new PlayerGrappleState(context, this, rigid, grappleSpeed, point, grappleStrafe, grappleRopeLen, grappleYankForce, grappleSpringConstant, grappleSpringDamper);
    }

    public PlayerBaseState Bonk(float stunTime) {
        return new PlayerBonkedState(context, this, rigid, 0f, stunTime);
    }
}
