using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenPokeState : BenBaseState
{
    private Arm arm;
    private float windupTime;
    private float timeEntered;

    public BenPokeState(BossController baseContext, BossFactory factory, float speed, CloneManager manager, Arm arm, float windupTime) : base(baseContext, factory, speed, manager){
        this.arm = arm;
        this.windupTime = windupTime;
    }

    public override void enterState(){
        timeEntered = Time.time;
        this.baseContext.animator.SetTrigger("toss");
    }

    public override void updateState(){
        checkSwitchStates();
    }

    public override void exitState(){
        arm.deploy();
        this.cloneManager.cloneToss();
    }

    public override void checkSwitchStates(){
        if(timeEntered + windupTime < Time.time) {
            this.switchStates(this.baseFactory.March(true));
        }
    }

    public override bool requestSwitchStates(BossBaseState swapTo){
        return false;
    }

    public override string debuginfo() {
        return "ben-poke";
    }
}
