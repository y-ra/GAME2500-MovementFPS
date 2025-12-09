using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//dash into ranged attack
public class RachelSingState : RachelBaseState
{
    
    private float dashTime;
    private float downTime;
    private int numRings;
    private float timeBetweenRings;
    private Mouth mouth;
    private float chanceSwapSing;

    public RachelSingState(BossController baseContext, BossFactory factory, float speed, float rangeAroundPlayer, int numRings, float timeBetweenRings, float dashTime, Mouth mouth, float downTime, float chanceSwapSing) : base(baseContext, factory, speed, rangeAroundPlayer){
        this.numRings = numRings;
        this.timeBetweenRings = timeBetweenRings;
        this.dashTime = dashTime;
        this.mouth = mouth;
        this.downTime = downTime;
        this.chanceSwapSing = chanceSwapSing;
    }

    public override void enterState(){
        this.baseContext.mono.PissBabyCoroutine(new CoroutineableMethod(dash));
    }

    private IEnumerator dash() {
        Vector3 rnd = Random.insideUnitCircle * this.rangeAroundPlayer;
        rnd.z = rnd.y;
        rnd.y = 0f;
        Vector3 dest = baseContext.player.transform.position + rnd;
        Vector3 start = baseContext.transform.position;
        float time = Time.time;

        while(time + dashTime > Time.time) {
            this.baseContext.transform.position = Vector3.Lerp(start, dest, (Time.time - time)/(dashTime));
            yield return null;
        }

        shootRings();
    }

    private void shootRings() {
        if(numRings <= 0) {
            this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(checkSwitchStates, downTime),downTime);
            return;
        }

        mouth.screetch();
        this.numRings--;
        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(shootRings, timeBetweenRings),timeBetweenRings);
    }
    

    public override void updateState(){
        
    }

    public override void exitState(){}

    public override void checkSwitchStates(){
        //baseContext.mineCount += 1
        if(this.numRings <= 0 & Random.Range(0f,1f) < chanceSwapSing) {
            this.switchStates(this.baseFactory.Sing());
        } else {
            this.switchStates(this.baseFactory.Mine());
        }
    }

    public override bool requestSwitchStates(BossBaseState swapTo){
        return false;
    }

    public override string debuginfo() {
        return "sing";
    }

    public override Vector3 setSpeed(Vector3 desiredVelocity) {
        return Vector3.zero;
    }
}
