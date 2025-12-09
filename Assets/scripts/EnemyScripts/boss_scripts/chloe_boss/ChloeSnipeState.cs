using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChloeSnipeState : ChloeBaseState
{
    private float timeTillSniped;
    private float timeEntered;
    private LineRenderer snipeTracer;
    private bool damagePlayer;
    private Transform firePoint;
    private LayerMask snipeLayerMask;
    private float damageAmount;

    public ChloeSnipeState(BossController baseContext, BossFactory factory, float speed, Turret turret, LayerMask walls, float randomPointDistance, LineRenderer tracer, float timeTillSniped, Transform firePoint, LayerMask snipeLayerMask, float damageAmount) : base(baseContext, factory, speed, turret, walls, randomPointDistance){
        this.snipeTracer = this.baseContext.mono.PissBabyInstantiate(tracer.gameObject, this.baseContext.transform.position, Quaternion.identity).GetComponent<LineRenderer>();
        this.snipeTracer.positionCount = 2;

        this.timeTillSniped = timeTillSniped;
        this.firePoint = firePoint;
        this.snipeLayerMask = snipeLayerMask;
        this.damageAmount = damageAmount;
    }
    // public AudioSource audioSource = GetComponent<AudioSource>();

    public override void enterState(){
        timeEntered = Time.time;
        damagePlayer = true;
        this.snipeTracer.gameObject.SetActive(true);
        // audiosource.loop = true;
        // audioSource.Play();
    }
    

    public override void updateState(){
        checkSwitchStates();
        
        

        if(Time.time > timeEntered + timeTillSniped) {
            this.requestSwitchStates(this.baseFactory.Idle());
        } else if(damagePlayer) {
            RaycastHit hit;
            Physics.Raycast(this.firePoint.position, (this.baseContext.player.transform.position - this.firePoint.position).normalized, out hit, (this.baseContext.player.transform.position - this.firePoint.position).magnitude+1, snipeLayerMask);

            snipeTracer.SetPosition(0,this.firePoint.position);
            snipeTracer.SetPosition(1,hit.point);

            if(hit.collider.gameObject.GetComponentInParent<PlayerController>() == null) {
                this.damagePlayer = false;
                this.snipeTracer.gameObject.SetActive(false);
            }

            
        } 
    }

    public override void exitState(){
        //snipe player
        if(this.damagePlayer) {
            //deal damage
            Debug.Log("GOING TO DAMAGE PLAYER");
            this.baseContext.player.TakeDamage(damageAmount);
        }
        this.baseContext.mono.PissBabyDestroy(snipeTracer.gameObject);

        // audioSource.Stop();
        
    }

    public override void checkSwitchStates(){
    }

    public override bool requestSwitchStates(BossBaseState swapTo){
        this.switchStates(swapTo);
        return true;
    }

    public override string debuginfo() {
        return "Snipe";
    }
}
