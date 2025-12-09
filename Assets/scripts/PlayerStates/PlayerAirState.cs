using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents the state when the player is sprinting
public class PlayerAirState : PlayerBaseState
{
    private float airStrafe;
    private float maxClimbSpeed; //added this to stop player from getting laucnehd at mach 5
    private float maxFallSpeed;

    public PlayerAirState(PlayerController context, PlayerStateFactory factory, Rigidbody rigid, float speed, float airStrafe, float maxFallSpeed, float maxClimbSpeed)  : base (context, factory, speed, rigid){
        this.airStrafe = airStrafe;
        this.maxFallSpeed = maxFallSpeed;
        this.maxClimbSpeed = maxClimbSpeed;
    }

    public override void enterState(){
        this.rigid.drag = 0;
    }

    public override void updateState(){
        this.rigid.drag = 0;
        checkSwitchStates();
    }

    public override void exitState(){
        this.rigid.drag = this.baseContext.groundDrag;
    }

    public override void checkSwitchStates(){
        if(this.baseContext.grounded && this.baseContext.sprintKeyHeld) {
            this.switchStates(this.baseFactory.Run());
        } else if(this.baseContext.grounded && !this.baseContext.sprintKeyHeld) {
            this.switchStates(this.baseFactory.Walk());
        } else if (this.baseContext.crouchKeyHeld && this.baseContext.nextSlideTime < Time.time) {
            this.switchStates(this.baseFactory.Slide());
        }
    }

    public override bool requestSwitchStates(PlayerBaseState swapTo) {
        this.switchStates(swapTo);
        return true;
    }


    //can't jump while in air
    public override void jump() {}

    public override void movePlayer(Vector3 desiredDirection) {
        rigid.AddForce(desiredDirection.normalized * this.maxSpeed * 10f * airStrafe, ForceMode.Force);
    }

    public override Vector3 capSpeed() {
        Vector3 airVel = new Vector3(this.rigid.velocity.x, 0, this.rigid.velocity.z);
        airVel = (airVel.magnitude > this.maxSpeed) ? airVel.normalized * this.maxSpeed : airVel;
        float yVel = (this.rigid.velocity.y < -maxFallSpeed) ? -maxFallSpeed: this.rigid.velocity.y;
        Vector3 outt = new Vector3(airVel.x, yVel, airVel.z);
        Debug.Log("AIR VEL MAG: " + outt.magnitude + ", " + outt);
        Debug.Log("????:" + maxFallSpeed + ", " + this.maxSpeed);
        return new Vector3(airVel.x, yVel, airVel.z);
    }
}
