using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDismountWallState : PlayerAirState
{

    public PlayerDismountWallState(PlayerController context, PlayerStateFactory factory, Rigidbody rigid, float speed, float airStrafe, float maxFallSpeed, float maxClimbSpeed) : base(context, factory, rigid, speed, airStrafe, maxFallSpeed, maxClimbSpeed){}

    public override bool requestSwitchStates(PlayerBaseState swapTo) {
        return false;
    }

    public override void checkSwitchStates(){
        
        if(!this.baseContext.wallCheck.onWall) { //I should be locked up for this one
            this.switchStates(this.baseFactory.Air());
        }
    }
}
