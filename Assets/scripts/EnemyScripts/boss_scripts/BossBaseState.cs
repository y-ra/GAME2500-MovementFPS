using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossBaseState
{

    protected BossController baseContext;
    protected BossFactory baseFactory;
    protected float maxSpeed;

    public BossBaseState(BossController baseContext, BossFactory fac, float speed) {
        this.baseContext = baseContext;
        this.baseFactory = fac;
        this.maxSpeed = speed;
    }

    public abstract void enterState();
    
    public abstract void updateState();

    public abstract void exitState();

    public abstract void checkSwitchStates();

    public abstract bool requestSwitchStates(BossBaseState swapTo);

    public virtual Vector3 setTarget() {
        return baseContext.player.transform.position;
    }

    public virtual Vector3 setSpeed(Vector3 desiredVelocity) {
        return (desiredVelocity.magnitude > this.maxSpeed) ? desiredVelocity.normalized * this.maxSpeed : desiredVelocity;
    }

    protected void switchStates(BossBaseState newState){
        exitState();
        newState.enterState();
        
        baseContext.state = newState;

        Debug.Log("baseContext" + baseContext.state.debuginfo());

        baseContext.informStateChange();
    }

    public abstract string debuginfo();
}
