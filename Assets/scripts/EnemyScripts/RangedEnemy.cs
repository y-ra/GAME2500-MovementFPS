using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Player player;
    public float visionConeAngle;
    public RangedEnemyManager manager;
    public GameObject projectile;
    public Transform leftArm;
    public Transform rightArm; //raycast from both of these to determine i can see player

    [Header("state transition stuff")]
    public bool gainAgro; //heard noise within earshot or can see player within eyesight distance
    public Vector3 positionToVisit; //position that potentially has player
    public float distanceToShoot;
    public List<Transform> patrolPoints;
    public int patrolIndex;
    public float patrolCloseEnough;
    public int shotsToSurge;
    public int shotsTaken;
    

    [Header("enemy weapon")]
    public float projectileSpeed;
    public float projectileSpread;
    public float shootCooldown;
    public int shotsPerVolley;
    public float volleyCooldown;
    private int shots;
    private float saveSpeed;
    public float health = 15f;

    public enum RangedStates //lazy so enum
    {
        Idle, //big chillin like a villain (the ai is evil)
        Target, //player position is known, going to try to attack
        Attack, //can attack
        Surge, //if player isn't dying full sprint
        Dead //dead
    }

    public RangedStates currentState;

    public void Start() {
        agent.destination = transform.position;
        gainAgro = false;
        patrolIndex = patrolPoints.Count;
        patrolPoints.Add(this.transform);
        shots = shotsPerVolley;
    }

    public override void takeDamage(float damage) {
        this.health -= damage;
    }

    public void Update() {
        transform.LookAt(player.transform);
        transform.Rotate(0,-90,0);
        switch(currentState) {
            case RangedStates.Idle:
                checkVisionCone();
                //if count is 1 then no point to cycle to the same point repeatedly
                if(this.patrolPoints.Count > 1 && (this.transform.position - patrolPoints[patrolIndex].position).magnitude < patrolCloseEnough) {
                    this.patrolIndex = (this.patrolIndex + 1) % this.patrolPoints.Count;
                    this.agent.destination = this.patrolPoints[this.patrolIndex].position;
                }
                break;
            case RangedStates.Target:
                if((this.transform.position - positionToVisit).magnitude < patrolCloseEnough) {
                    //means we made it to where the player should be and no one was there????
                    //so we chose another random position nearby, try to go there and see if we can find the player

                }
                //move towards position it has learned
                break;
            case RangedStates.Attack:
                //post up fire, move post up fire, till break los
                break;
            case RangedStates.Surge:   
                this.agent.destination = player.transform.position; 
                //after n amount of fires start shooting and running at player
                break;
            case RangedStates.Dead:
                //do nothing idk man
                break;
        }

        checkSwitchStates();

        if(health < 0) {
            this.currentState = RangedStates.Dead;
            Destroy(this.gameObject);
        }
    }

    public void checkSwitchStates() {
        switch(currentState) {
            case RangedStates.Idle:
                if(gainAgro) {
                    this.exitState();
                    this.enterState(RangedStates.Target);
                }
                break;
            case RangedStates.Target:
                if((this.transform.position - this.player.transform.position).magnitude < distanceToShoot || this.checkVisionCone()) {
                    this.exitState();
                    this.enterState(RangedStates.Attack);
                }
                break;
            case RangedStates.Attack:
            if(shotsTaken > shotsToSurge) {
                    this.exitState();
                    this.enterState(RangedStates.Surge);
                }
                break;
            case RangedStates.Surge:    
                break;
            case RangedStates.Dead:
                break;
        }
    }

    public void enterState(RangedStates state) {
        this.currentState = state;
        switch(state) {
            case RangedStates.Idle:
                this.agent.destination = patrolPoints[patrolIndex].position;
                break;
            case RangedStates.Target:
                Debug.Log("targeting");
                this.agent.destination = positionToVisit;
                break;
            case RangedStates.Attack:
                shotsTaken = 0;
                saveSpeed = this.agent.speed;
                this.shootAt();
                break;
            case RangedStates.Surge:
                this.agent.destination = player.transform.position;
                this.chase();
                break;
            case RangedStates.Dead:
                break;
        }
    }

    public void exitState() {
        switch(currentState) {
            case RangedStates.Idle:
                break;
            case RangedStates.Target:
                break;
            case RangedStates.Attack:
                break;
            case RangedStates.Surge:    
                break;
            case RangedStates.Dead:
                break;
        }
    }

    public void checkPosition(Vector3 position) {
        this.positionToVisit = position;
        this.gainAgro = true;
    }

    public void shootAt() {
        if(shots > 0 && this.currentState == RangedStates.Attack) {
            Debug.Log("shooting at the player!");
            this.agent.speed = 0f;
            GameObject project = Instantiate(projectile, this.leftArm.position, Quaternion.identity);

            Rigidbody rigid = project.GetComponent<Rigidbody>();
            Bullet bullet = project.GetComponent<Bullet>();
            bullet.Deploy(1f, 4f);

            Vector3 r = Random.onUnitSphere;
            r = r.normalized * (this.player.transform.position - this.leftArm.position).magnitude/10f;

            Vector3 velocity = (this.player.transform.position - this.leftArm.position + r).normalized * projectileSpeed;

            rigid.AddForce(velocity, ForceMode.Impulse);

            this.shots--;
            Invoke(nameof(shootAt), shootCooldown);
        } else if(this.currentState == RangedStates.Attack){
            shotsTaken++;
            this.agent.speed = saveSpeed;
            this.shots = shotsPerVolley;
            this.scootTowards();
        }
    }

    public void catchPlayer() {
        this.agent.speed = saveSpeed;
        this.agent.destination = player.transform.position;
    }

    public void scootTowards() {
        Vector3 r = Random.onUnitSphere;
        r.y = 0f;
        this.agent.destination = player.transform.position + r * 5f;
        Invoke(nameof(shootAt),volleyCooldown);
    }

    public void chase() {
        if(shots > 0 && this.currentState == RangedStates.Surge) {
            this.agent.speed = saveSpeed;
            GameObject project = Instantiate(projectile, this.leftArm.position, Quaternion.identity);

            Rigidbody rigid = project.GetComponent<Rigidbody>();
            Bullet bullet = project.GetComponent<Bullet>();
            bullet.Deploy(1f, 4f);

            Vector3 r = Random.onUnitSphere;
            r = r.normalized * (this.player.transform.position - this.leftArm.position).magnitude/10f;

            Vector3 velocity = (this.player.transform.position - this.leftArm.position + r).normalized * projectileSpeed;

            rigid.AddForce(velocity, ForceMode.Impulse);

            this.shots--;
            Invoke(nameof(chase), shootCooldown*2f);
        } else {
            this.agent.speed = 0f;
            this.shots = shotsPerVolley;
            Invoke(nameof(chase), volleyCooldown);
        }
    }

    public bool checkVisionCone() {
        if(Vector3.Angle(this.transform.forward, (this.player.transform.position - this.leftArm.position)) < visionConeAngle && (this.player.transform.position - this.leftArm.position).magnitude < distanceToShoot) {
            return true;
        }
        return false;
    }

    public bool checkPlayerLos() {
        if(Physics.Raycast(leftArm.position, (this.player.transform.position - this.leftArm.position).normalized, distanceToShoot) && Physics.Raycast(rightArm.position, (this.player.transform.position - this.leftArm.position).normalized, distanceToShoot)) {
            return true;
        }
        return false;
    }
}