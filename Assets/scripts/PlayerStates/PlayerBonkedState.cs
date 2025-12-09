using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents the state when the player is sprinting
public class PlayerBonkedState : PlayerBaseState
{

    private float bonkEntered;
    private float stunTime;

    public PlayerBonkedState(PlayerController context, PlayerStateFactory factory, Rigidbody rigid, float speed, float stunTime)  : base (context, factory, speed, rigid){
        this.stunTime = stunTime;
    }

    public override void enterState(){
        bonkEntered = Time.time;
    }

    public override void updateState(){
        checkSwitchStates();
    }

    public override void exitState(){
    }

    public override void checkSwitchStates(){
        if(bonkEntered + stunTime > Time.time) {
            this.switchStates(this.baseFactory.Air());
        } 
    }

    public override bool requestSwitchStates(PlayerBaseState swapTo) {
        return false;
    }


    //can't jump while stunned
    public override void jump() {}

    public override void movePlayer(Vector3 desiredDirection) {
        //teehee you're stunned
    }

    public override Vector3 capSpeed() {
        return this.rigid.velocity; //teehee no speedcap
    }
}
