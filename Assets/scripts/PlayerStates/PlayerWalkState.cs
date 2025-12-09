using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents movement state in which the player is moving but not sprinting
public class PlayerWalkState : PlayerBaseState
{

    public PlayerWalkState(PlayerController context, PlayerStateFactory factory, Rigidbody rigid, float speed) : base (context, factory, speed, rigid){

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
     } else if(this.baseContext.sprintKeyHeld) {
          this.switchStates(this.baseFactory.Run());
     } 
   }
}
