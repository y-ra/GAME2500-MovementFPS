using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChloeIdleState : ChloeBaseState
{
    private float time;
    private float minTime;
    private float maxTime;
    private float chanceSwapAirstrike;
    private float chanceSwapSnipe;
    private float chanceSwapSurge;
    public ChloeIdleState(BossController baseContext, BossFactory factory, float speed, Turret turret, LayerMask walls, float randomPointDistance, float minTime, float maxTime, float chanceSwapAirstrike, float chanceSwapSnipe, float chanceSwapSurge) : base(baseContext, factory, speed, turret, walls, randomPointDistance){
        this.minTime = minTime;
        this.maxTime = maxTime;
        time = Random.Range(minTime, maxTime);
        this.chanceSwapAirstrike = chanceSwapAirstrike;
        this.chanceSwapSnipe = chanceSwapSnipe;
        this.chanceSwapSurge = chanceSwapSurge;
    }

    public override void enterState(){
    }
    

    public override void updateState(){
        time -= Time.deltaTime;
        checkSwitchStates();
    }

    public override void exitState(){}

    public override void checkSwitchStates(){
        if(time <= 0) {
            this.randomStateSwap();
        }
    }

    private void randomStateSwap() {
        float rng = Random.Range(0f,1f);

        if(rng < chanceSwapAirstrike) {
            this.switchStates(this.baseFactory.AirStrike());
        } else if(rng < chanceSwapAirstrike + chanceSwapSurge) {
            this.switchStates(this.baseFactory.Surge());
        } else if(rng < chanceSwapAirstrike + chanceSwapSurge + chanceSwapSnipe) {
            this.switchStates(this.baseFactory.Snipe());
        } else {
            time = Random.Range(minTime, maxTime);
        }
    }

    public override bool requestSwitchStates(BossBaseState swapTo){
        return false;
    }

    public override string debuginfo() {
        return "Idle";
    }
}
