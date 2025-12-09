using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : Enemy
{
    public enum FSMStates
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead
    }

    public AudioSource shootingSFX;
    public FSMStates currentState;
    public GameObject hpPrefab;
    public float hpBoost;
    public float enemySpeed = 5;
    public float chaseDistance = 10;
    public GameObject player;
    public GameObject bullet;
    public float bulletSpeed = 50f;
    public Transform bulletSpawnPoint;
    public GameObject hand;

    public GameObject[] wanderPoints;

    public float shootRate = 2;
    public float attackDistance = 5;
    public float fieldOfView = 45f;

    Vector3 nextDestination;

    float distanceToPlayer;
    float elapsedTime = 0;

    int currentDestinationIndex = 0;

    Transform deadTransform;

    bool isDead;
    NavMeshAgent agent;

    public float currentHealth = 25;
    public Slider healthSlider;

    void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
    }

    void Start()
    {
        shootingSFX = GetComponent<AudioSource>();

        isDead = false;

        agent = GetComponent<NavMeshAgent>();

        currentState = FSMStates.Patrol;

        FindNextPoint();
    }

    // Update is called once per frame
    void Update()
    {

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        switch (currentState)
        {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;

            case FSMStates.Chase:
                UpdateChaseState();
                break;

            case FSMStates.Attack:
                UpdateAttackState();
                break;

            case FSMStates.Dead:
                UpdateDeadState();
                break;
        }

        elapsedTime += Time.deltaTime;
    }

    void UpdatePatrolState()
    {
        // print("Patrolling!");

        // anim.SetInteger("animState", 1);

        agent.stoppingDistance = 0;
        agent.speed = 3.5f;

        // https://docs.unity3d.com/Manual/nav-AgentPatrol.html Module code was not working
        if (Vector3.Distance(transform.position, nextDestination) < 3)
        {
            FindNextPoint();
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }

        FaceTarget(nextDestination);

        agent.SetDestination(nextDestination);

    }

    void UpdateChaseState()
    {
        print("Chasing!");

        nextDestination = player.transform.position;

        agent.stoppingDistance = attackDistance;
        agent.speed = 5;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            FindNextPoint();
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
    }

    void UpdateAttackState()
    {
        // print("Attacking!");

        nextDestination = player.transform.position;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }
        // anim.SetInteger("animState", 3);

        EnemySpellCast();
    }

    void UpdateDeadState()
    {
        isDead = true;

        deadTransform = gameObject.transform;

        Destroy(gameObject, 3f);
    }

    void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;
        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;

        agent.SetDestination(nextDestination);

    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    void EnemySpellCast()
    {
        if (!isDead)
        {
            if (elapsedTime >= shootRate)
            {

                ShootAtPlayer();
                elapsedTime = 0.0f;
            }
        }
    }


    private void OnDestroy()
    {
        Instantiate(hpPrefab, deadTransform.position, deadTransform.rotation);
    }

    private void OnDrawGizmos()
    {
        // attack distance
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        // chase
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }

    private void ShootAtPlayer() 
    {
        GameObject obj = Instantiate(bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        // shootingSFX.Play();
        ThrowableDisk disk = obj.GetComponent<ThrowableDisk>();
        disk.player = this.player.GetComponent<Player>();
        disk.deploy();
    }

    public override void takeDamage(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            deadTransform = gameObject.transform;
            Instantiate(hpPrefab, deadTransform.position, deadTransform.rotation);
            Destroy(this.gameObject);
        }

        Debug.Log("Current health: " + currentHealth);

    }



}
