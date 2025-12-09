using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//dashes low to the ground, leaving a lot of mines on the ground and shit
public class RachelMineState : RachelBaseState
{

    private GameObject mine;
    private float timeBetweenMine;
    private float dashTime;
    private bool dashing;
    private float enterAnimTime;
    private float exitAnimTime;
    private Transform bodyPos;
    private float overDashMagnitude;


    public RachelMineState(BossController baseContext, BossFactory factory, float speed, float rangeAroundPlayer, GameObject mine, float timeBetweenMine, float dashTime, float enterAnimTime, float exitAnimTime, Transform bodyPos, float overDashMagnitude) : base(baseContext, factory, speed, rangeAroundPlayer){
        this.mine = mine;
        this.timeBetweenMine = timeBetweenMine;
        this.dashTime = dashTime;
        this.enterAnimTime = enterAnimTime;
        this.exitAnimTime = exitAnimTime;
        this.bodyPos = bodyPos;
        this.overDashMagnitude = overDashMagnitude;
    }

    public override void enterState(){
        this.baseContext.animator.SetTrigger("startMine");
        dashing = false;
        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(startDash, enterAnimTime), enterAnimTime);
    }

    private void startDash() {
        this.baseContext.mono.PissBabyCoroutine(new CoroutineableMethod(dash));
        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(layMine, timeBetweenMine/2),timeBetweenMine/2);
    }
    
    private void layMine() {
        if(this.dashing) {
            this.baseContext.mono.PissBabyInstantiate(mine, this.bodyPos.position, Quaternion.identity);
            this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(layMine, timeBetweenMine),timeBetweenMine);
        }
    }

    private IEnumerator dash() {
        dashing = true;
        float time = Time.time;
        Vector3 flatP = this.baseContext.player.transform.position;
        flatP.y = 0;
        Vector3 flatC = this.baseContext.transform.position;
        flatC.y = 0;
        Vector3 dest = flatP + (flatP - flatC).normalized * ((flatC - flatP).magnitude + overDashMagnitude);

        while(time + dashTime > Time.time) {
            this.baseContext.transform.position = Vector3.Lerp(flatC, dest, (Time.time - time)/(dashTime));
            yield return null;
        }

        dashing = false;
        this.baseContext.animator.SetTrigger("endMine");
        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(checkSwitchStates, exitAnimTime), exitAnimTime);
    }

    public override void updateState(){
    }

    public override void exitState(){}

    public override void checkSwitchStates(){
        this.switchStates(this.baseFactory.Sing());
    }

    public override bool requestSwitchStates(BossBaseState swapTo){
        return false;
    }

    public override string debuginfo() {
        return "mine";
    }
}
