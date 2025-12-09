using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AlexBossBaseState
{

    // protected alex_boss_controller baseContext;
    // protected ABFactory baseFactory;
    // protected float maxSpeed;

    // public AlexBossBaseState(alex_boss_controller baseContext, ABFactory fac, float speed) {
    //     this.baseContext = baseContext;
    //     this.baseFactory = fac;
    //     this.maxSpeed = speed;
    // }

    // public abstract void enterState();
    
    // public abstract void updateState();

    // public abstract void exitState();

    // public abstract void checkSwitchStates();

    // public abstract bool requestSwitchStates(PlayerBaseState swapTo);

    // public virtual Vector3 setTarget() {
    //     return baseContext.player.position;
    // }

    // public virtual Vector3 setSpeed(Vector3 desiredVelocity) {
    //     return (desiredVelocity.magnitude > this.maxSpeed) ? desiredVelocity.normalized * this.maxSpeed : desiredVelocity;
    // }

    // protected void switchStates(AlexBossBaseState newState){
    //     exitState();
    //     newState.enterState();
        
    //     baseContext.state = newState;
    //     baseContext.informStateChange();
    // }
}
