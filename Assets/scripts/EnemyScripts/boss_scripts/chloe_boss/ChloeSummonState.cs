using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChloeSummonState : ChloeBaseState
{
    //get locations, check if they are on mesh, spawn enemies there, swap
    public ChloeSummonState(BossController baseContext, BossFactory factory, float speed, Turret turret, LayerMask walls, float randomPointDistance) : base(baseContext, factory, speed, turret, walls, randomPointDistance){

    }

    public override void enterState(){
    }
    

    public override void updateState(){
        checkSwitchStates();
    }

    public override void exitState(){}

    public override void checkSwitchStates(){
    }

    public override bool requestSwitchStates(BossBaseState swapTo){
        return false;
    }

    public override string debuginfo() {
        return "Summon";
    }
}
