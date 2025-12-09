using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//non enrage state where boss takes reduced damage but powers up
public class ABBlockState : BossBaseState
{
    private float blockTime;
    private float timeEntered;

    public ABBlockState(BossController baseContext, BossFactory factory, float speed, float blockTime) : base(baseContext, factory, speed){
        this.blockTime = blockTime;
    }

    public override void enterState(){
        baseContext.animator.SetTrigger("block");
        timeEntered = Time.time;
    }
    

    public override void updateState(){
        checkSwitchStates();
    }

    public override void exitState(){}

    public override void checkSwitchStates(){
        if(timeEntered + blockTime < Time.time) {
            this.switchStates(this.baseFactory.Roam());
        }
    }

    public override bool requestSwitchStates(BossBaseState swapTo){
        return false;
    }

    public override string debuginfo() {
        return "block";
    }
}
