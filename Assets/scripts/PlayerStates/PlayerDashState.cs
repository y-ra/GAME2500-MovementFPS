using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents the state when the player is sprinting
public class PlayerDashState : PlayerBaseState
{
    private Vector3 direction;
    private float timer;
    private float startTime;
    public PlayerDashState(PlayerController context, PlayerStateFactory factory, Rigidbody rigid, Vector3 direction, float timer, float speed)  : base (context, factory, speed, rigid){
        this.direction = direction;
        this.timer = timer;
    }

    public override void enterState(){
        this.startTime = Time.time;
        this.rigid.drag = 0;
        this.rigid.useGravity = false;
        this.rigid.velocity = Vector3.zero;
        this.rigid.AddForce(this.direction * this.maxSpeed, ForceMode.Impulse);
    }

    public override void updateState(){
        checkSwitchStates();
        //slowdash stuff here
    }

    public override void exitState(){
        rigid.drag = this.baseContext.groundDrag;
        this.rigid.useGravity = true;
    }

    //dash cannot be cancled
    public override bool requestSwitchStates(PlayerBaseState swapTo) {
        return false;
    }

    public override void checkSwitchStates(){
        if(Time.time - this.startTime > timer && !this.baseContext.grounded) {
            this.switchStates(this.baseFactory.Air());
        } else if(Time.time - this.startTime > timer && this.baseContext.sprintKeyHeld) {
            this.switchStates(this.baseFactory.Run());
        } else if(Time.time - this.startTime > timer) {
            this.switchStates(this.baseFactory.Walk());
        }
    }

    //can't jump while dashing
    public override void jump() {}

    //no control when dashing
    public override void movePlayer(Vector3 desired) {}

    public override Vector3 capSpeed() {
        //if dash slow down implace, should drop speed rapidly to something??
        return this.rigid.velocity; //PLACE HOLDER

    }
}