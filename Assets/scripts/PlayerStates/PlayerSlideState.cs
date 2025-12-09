using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents the state when the player is sliding
public class PlayerSlideState : PlayerBaseState
{
    private float drag;
    private float grav;
    private float strafe;
    private float minSlopeAngle;

    private float minSpeedToLeave;

    private Vector3 regScale;

    public float cooldown;
    //maybe replace me with speed requirement to swap into slide?

    public PlayerSlideState(PlayerController context, PlayerStateFactory factory, Rigidbody rigid, float speed, float slideGrav, float slideStrafe, float minAngle, float minSpeedToLeave, float cooldown)  : base (context, factory, speed, rigid){
        this.grav = slideGrav;
        this.strafe = slideStrafe;
        this.minSlopeAngle = minAngle;
        this.minSpeedToLeave = minSpeedToLeave;
        this.cooldown = cooldown;
    }

    public override void enterState(){
        Vector3 forceDirection = this.rigid.velocity;
        forceDirection.y = 0;
        this.rigid.AddForce(forceDirection.normalized * 2f, ForceMode.Impulse);
        drag = this.rigid.drag;
        this.rigid.drag = 1f; //hard code lol
        this.regScale = this.baseContext.gameObject.transform.localScale;
        this.baseContext.gameObject.transform.localScale = new Vector3(this.regScale.x, this.regScale.y/2, this.regScale.z);
    }

    public override void updateState(){
        checkSwitchStates();
    }

    public override void exitState(){
        this.rigid.drag = drag;
        this.baseContext.nextSlideTime = Time.time + cooldown;
        this.baseContext.gameObject.transform.localScale = this.regScale;
    
    }

    public override void movePlayer(Vector3 desiredDirection) {
        Vector3 moveDir = new Vector3(this.rigid.velocity.x, 0, this.rigid.velocity.z);
        Vector3 adjustDir = Vector3.ProjectOnPlane(desiredDirection, moveDir);
        adjustDir.y = 0;
        rigid.AddForce(adjustDir.normalized * maxSpeed * 10f * strafe, ForceMode.Force);
        rigid.AddForce(-Vector3.up * this.grav, ForceMode.Force); 
    }

    public override Vector3 capSpeed() {
        // Vector3 planeSpeed = Vector3.ProjectOnPlane(this.rigid.velocity, this.baseContext.slopeHitNormal);
        // Vector3 normalSpeed = this.rigid.velocity - planeSpeed;

        // if(planeSpeed.magnitude > this.maxSpeed) {
        //     planeSpeed = planeSpeed.normalized * this.maxSpeed;
        // }

        // return planeSpeed + normalSpeed;

        //slope gravity should apply :D
        return (this.rigid.velocity.magnitude > this.maxSpeed) ? this.rigid.velocity.normalized * this.maxSpeed : this.rigid.velocity;
    }

    public override bool requestSwitchStates(PlayerBaseState swapTo) {
        this.switchStates(swapTo);
        return true;
    }

    public override void checkSwitchStates(){
        //too slow -> swap

        //crouch released
        if((!this.baseContext.crouchKeyHeld || shouldSwap()) && this.baseContext.sprintKeyHeld) {
            this.switchStates(this.baseFactory.Run());
        } else if(!this.baseContext.crouchKeyHeld || shouldSwap()) {
            this.switchStates(this.baseFactory.Walk());
        } 
    }
    //if not on slope and slow
    public bool shouldSwap() {
        Debug.Log(Vector3.Angle(this.baseContext.slopeHitNormal, Vector3.up) + "<" + minSlopeAngle);
        return Vector3.Angle(this.baseContext.slopeHitNormal, Vector3.up) < minSlopeAngle 
                && this.rigid.velocity.magnitude < minSpeedToLeave;
    }

}
