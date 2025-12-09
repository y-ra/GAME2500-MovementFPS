using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

//represents the state when the player is sprinting
public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerController context, PlayerStateFactory factory, Rigidbody rigid, float speed)  : base (context, factory, speed, rigid){
        
    }

    public override void enterState(){
    }

    public override void updateState(){
        checkSwitchStates();
    }

    public override void exitState(){}

    public override bool requestSwitchStates(PlayerBaseState swapTo) {
        this.switchStates(swapTo);
        return true;
    }

    public override void checkSwitchStates(){
        if(!this.baseContext.grounded) {
          this.switchStates(this.baseFactory.Air());
        } else if(this.baseContext.crouchKeyHeld && this.baseContext.nextSlideTime < Time.time) {
          this.switchStates(this.baseFactory.Slide());
        } else if(!this.baseContext.sprintKeyHeld) {
          this.switchStates(this.baseFactory.Walk());
        } 
    }
}
