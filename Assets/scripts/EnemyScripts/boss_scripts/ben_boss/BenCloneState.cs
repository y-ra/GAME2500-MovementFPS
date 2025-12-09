using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenCloneState : BenBaseState
{
    private float cloneTime;

    public BenCloneState(BossController baseContext, BossFactory factory, float speed, CloneManager manager, float cloneTime) : base(baseContext, factory, speed, manager){
        
    }

    public override void enterState(){
        Vector3 cloneSpawnPos = this.baseContext.transform.position + (this.baseContext.player.transform.position - this.baseContext.transform.position)/2;
        cloneSpawnPos.y = this.baseContext.transform.position.y;
        this.cloneManager.spawnClone(cloneSpawnPos);

        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(checkSwitchStates,cloneTime),cloneTime);
    }
    

    public override void updateState(){
        checkSwitchStates();
    }

    public override void exitState(){}

    public override void checkSwitchStates(){
        this.switchStates(this.baseFactory.March(true));
    }

    public override bool requestSwitchStates(BossBaseState swapTo){
        return false;
    }

    public override string debuginfo() {
        return "clone";
    }
}
