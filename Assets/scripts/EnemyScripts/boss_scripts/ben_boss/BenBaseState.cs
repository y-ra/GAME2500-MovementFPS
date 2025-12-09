using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BenBaseState : BossBaseState
{
    protected CloneManager cloneManager;

    public BenBaseState(BossController baseContext, BossFactory factory, float speed, CloneManager cloneManager) : base(baseContext, factory, speed){
        this.cloneManager = cloneManager;
    }



    public override Vector3 setTarget() {
        return baseContext.player.transform.position;
    }

    public override Vector3 setSpeed(Vector3 desiredVelocity) {
        return desiredVelocity.normalized * this.maxSpeed;
    }
}
