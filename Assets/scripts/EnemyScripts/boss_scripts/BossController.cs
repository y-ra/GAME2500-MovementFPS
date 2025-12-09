using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract boss controller
public class BossController : Boss
{
    public BossFactory factory; //factory of all possible states yes all im lazy
    public Player player; //player 
    public UnityEngine.AI.NavMeshAgent agent; //our ai can be turned off to leave mesh
    public Animator animator; //animator :)
    public PissBabyMono mono; //mono >:)

    public float enrageHealthGate; //health that is needed to swap into enrage states

    [Header("state change vars")]
    public bool facePlayer = true; //whether or not we look at player

    public BossBaseState state; //initializing me is not this scripts responsibility (should be obvius if not)
    public float damageSinceSwap = 0;
    public Transform respawnPoint;


    void Update() {
        Debug.Log("baseContext curstate" + state.debuginfo());
        state.updateState();

        if(agent.enabled) { //agent can be disabled sometimes
            agent.destination = state.setTarget();
            agent.velocity = state.setSpeed(agent.desiredVelocity);
        }
    }

    void FixedUpdate() {
        if(facePlayer) {
            faceDirection();
        }
        
    }

    public override void takeDamage(float damage) {
        base.takeDamage(damage);
        damageSinceSwap += damage;
    }

    protected virtual void faceDirection() {
        transform.LookAt(player.transform);
        transform.Rotate(0,-90,0);
    }

    //called by base state when state flips to let boss know
    public virtual void informStateChange() {
        damageSinceSwap = 0;
    }

    public override void defeat(){
        this.mono.PissBabyInvoke(new InvokeableMethod(warpPlayer, 5f),5f);
        base.defeat();
    }

    private void warpPlayer() {
        player.controller.teleport(respawnPoint.position);
    }
}
