using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RachelBaseState : BossBaseState
{
    protected float rangeAroundPlayer;
    public RachelBaseState(BossController baseContext, BossFactory factory, float speed, float rangeAroundPlayer) : base(baseContext, factory, speed){
        this.rangeAroundPlayer = rangeAroundPlayer;
    }



    public override Vector3 setTarget() {
        if((baseContext.transform.position - baseContext.agent.destination).magnitude < 1.1) {
            while(true) {
                Vector3 rnd = Random.insideUnitCircle * this.rangeAroundPlayer;
                rnd.z = rnd.y;
                rnd.y = 0f;
                Vector3 dest = baseContext.player.transform.position + rnd;

                //ok so this part seems to cause an infinite loop
                
                // UnityEngine.AI.NavMeshHit hit;
                // if (UnityEngine.AI.NavMesh.SamplePosition(dest, out hit, 1f, UnityEngine.AI.NavMesh.AllAreas))
                // {
                //     return hit.position;
                // }
                return dest;
            }
        }
        return baseContext.agent.destination;
    }

    public override Vector3 setSpeed(Vector3 desiredVelocity) {
        return desiredVelocity.normalized * this.maxSpeed;
    }
}
