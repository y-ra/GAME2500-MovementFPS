using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChloeBaseState : BossBaseState
{
    protected Turret turret;
    public float randomPointDistance;
    public LayerMask walls;

    public ChloeBaseState(BossController baseContext, BossFactory factory, float speed, Turret turret, LayerMask walls, float randomPointDistance) : base(baseContext, factory, speed){
        this.turret = turret;
        this.walls = walls;
        this.randomPointDistance = randomPointDistance;
    }



    public override Vector3 setTarget() {
        // return baseContext.player.transform.position;

        if((this.baseContext.agent.destination - this.baseContext.transform.position).magnitude < 3f) {
            while(true) {
                Vector3 newDest = this.baseContext.transform.position + Random.insideUnitSphere * this.randomPointDistance;
                newDest.y = this.baseContext.transform.position.y;

                if(!Physics.Raycast(this.baseContext.transform.position, (this.baseContext.player.transform.position - this.baseContext.transform.position),(newDest-this.baseContext.transform.position).magnitude, this.walls)) {
                    return newDest;
                }
            }
        }
        return this.baseContext.agent.destination;
    }

    public override Vector3 setSpeed(Vector3 desiredVelocity) {
        return desiredVelocity.normalized * this.maxSpeed;
    }
}
