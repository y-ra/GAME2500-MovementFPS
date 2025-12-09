using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//smoke bomb
//invisible untargetable heeheehaha
public class BenSmokeState : BenBaseState
{
    private GameObject model;
    private GameObject smoke;
    private float smokeTime;
    private float reappearTime;
    private float enteredTime;
    private LayerMask walls;
    public BenSmokeState(BossController baseContext, BossFactory factory, float speed, CloneManager manager, float minReappearTime, float maxReappearTime, float smokeTime, GameObject model, GameObject smoke, LayerMask walls) : base(baseContext, factory, speed, manager){
        this.reappearTime = Random.Range(minReappearTime, maxReappearTime);
        this.model = model;
        this.smoke = smoke;
        this.smokeTime = smokeTime;
        this.walls = walls;
    }

    public override void enterState(){

        Vector3 newDest = this.baseContext.transform.position + Random.insideUnitSphere * 20f;
        while(true) {
            newDest.y = this.baseContext.transform.position.y;

            if(!Physics.Raycast(this.baseContext.transform.position, (this.baseContext.player.transform.position - this.baseContext.transform.position),(newDest-this.baseContext.transform.position).magnitude, this.walls)) {
                break;
            }
        }
        this.baseContext.agent.destination = newDest;
        

        enteredTime = Time.time;
        this.smoke.SetActive(true);
        this.model.SetActive(false);
        this.cloneManager.cloneSleep();

        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(removeSmoke, smokeTime),smokeTime);
        
    }

    private void removeSmoke() {
        this.smoke.SetActive(false);
    }
    

    public override void updateState(){
        checkSwitchStates();
    }

    public override void exitState(){
        this.smoke.SetActive(true);
        this.model.SetActive(true);
        this.cloneManager.cloneWake();


        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(removeSmoke, smokeTime),smokeTime);
    }

    public override void checkSwitchStates(){
        if(Time.time > enteredTime + reappearTime) {
            this.switchStates(this.baseFactory.March(true));
        }
    }

    public override bool requestSwitchStates(BossBaseState swapTo){
        return false;
    }

    public override string debuginfo() {
        return "Smoke";
    }

    public override Vector3 setTarget() {
        if((this.baseContext.agent.destination - this.baseContext.transform.position).magnitude < 3f) {
            while(true) {
                Vector3 newDest = this.baseContext.transform.position + Random.insideUnitSphere * 20f;
                newDest.y = this.baseContext.transform.position.y;

                if(!Physics.Raycast(this.baseContext.transform.position, (this.baseContext.player.transform.position - this.baseContext.transform.position),(newDest-this.baseContext.transform.position).magnitude, this.walls)) {
                    return newDest;
                }
            }
        }
        return this.baseContext.agent.destination;
    }
}
