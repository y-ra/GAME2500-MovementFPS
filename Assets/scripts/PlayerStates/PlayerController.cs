using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents the player context from which state behavior can be determined
public class PlayerController : MonoBehaviour
{
    private Rigidbody rigid;

    private Vector3 desiredMoveDirection;

    [Header ("Movement Vars")]
    public PlayerStateFactory factory;
    public float groundDrag;
    public float jumpForce;
    public Vector3 slopeHitNormal;
    public float maxSlopeAngle;
    public float jumpCooldown;

    [Header ("Context Info")]
    public bool grounded;
    public bool wallHeight;
    public bool sprintKeyHeld;
    public bool crouchKeyHeld;
    public float groundAngle;

    [Header ("Needed variables")]
    public float playerHeight;

    public float wallRideHeight;
    public LayerMask groundLayer;
    public Transform orientation;
    public Transform camDirection;

    public WallCollision wallCheck;
    public float nextSlideTime;
    public List<GrapplePoint> points;

    [Header ("State")]
    public PlayerBaseState state;

    private bool canJump;

    public AudioClip[] soundEffects;    // 0 is jump, 1 is run, 2 is walk
    private AudioSource audioSource;

    public void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Awake() {
        this.state = factory.Walk();

        rigid = GetComponent<Rigidbody>();
        rigid.freezeRotation = true;
        nextSlideTime = 0;
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        //update context info
        collectInfo();
        
        //get user input
        desiredMoveDirection = orientation.forward * Input.GetAxisRaw("Vertical") + orientation.right * Input.GetAxisRaw("Horizontal");
        //if on slope, project movement vector onto the plane
        desiredMoveDirection = Vector3.ProjectOnPlane(desiredMoveDirection, slopeHitNormal).normalized;

        if (desiredMoveDirection.magnitude > 0)
        {
            if (this.state == factory.Walk()) {
            audioSource.clip = soundEffects[2];
            audioSource.loop = true;
            audioSource.Play();

                if (this.state != factory.Walk())
                {
                audioSource.Stop();
                audioSource.loop = false;
                }
            }
            if (this.state == factory.Run()) {
            audioSource.clip = soundEffects[1];
            audioSource.loop = true;
            audioSource.Play();

               if (this.state != factory.Run())
               {
                audioSource.Stop();
                audioSource.loop = false;
               }
            }
        }

        if(Input.GetKey(KeyCode.Space) && canJump && grounded)
            jump();
            if (grounded)
            {
                audioSource.clip = soundEffects[0];
                audioSource.Play();
            }
        if(!grounded && this.wallCheck.onWall && !wallHeight) {
            this.wallRun();
        }
        

        //cap speed
        rigid.velocity = this.state.capSpeed();

        //swap to next states
        this.state.updateState();

        // Debug.Log("gtavity stuff: " + rigid.velocity.y + ", grounded? " + grounded + ", " + rigid.useGravity + ", STATE: " + state);
        // Debug.Log("velocity: " + rigid.velocity);
        Debug.Log("STATE:" + this.state);
    }

    //collects info about player's position in the enviroment
    private void collectInfo() 
    {
        //is player on ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f, groundLayer);
        wallHeight =  Physics.Raycast(transform.position, Vector3.down, wallRideHeight * .5f + .2f, groundLayer);
        

        sprintKeyHeld = Input.GetKey(KeyCode.LeftShift);

        crouchKeyHeld = Input.GetKey(KeyCode.LeftControl);
        //is player next to wallride wall

        RaycastHit slopeHit;
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * .5f + .3f)) {
            slopeHitNormal = slopeHit.normal;
        } else {
            slopeHitNormal = Vector3.up;
        }
        
    }

    private void jump()
    {
        canJump = false;

        state.jump();
        Invoke(nameof(resetJump), jumpCooldown);

    }

    private void resetJump() 
    {
        canJump = true;
    }

    private void FixedUpdate() {
        state.movePlayer(desiredMoveDirection);

        if(Input.GetKey(KeyCode.Q)) {
            for(int i = 0; i < points.Count; i++) {
                if(points[i].canGrapple) {
                    state.requestSwitchStates(factory.Grapple(points[i]));
                }
            }
            
        }
    }


    // when called teleports the player, returns if movement request was succesful
    public bool teleport(Vector3 position) {
        transform.position = position;
        //check if stuck in wall or something maybe
        return true;
    }

    // when called, attempts to enter a dash, returns if the dash was triggered or not
    public bool dash(Vector3 direction, float timer) {
        return this.state.requestSwitchStates(this.factory.Dash(direction, timer));
    } 

    public bool wallRun() {
        if(!this.grounded)
            return this.state.requestSwitchStates(this.factory.Wallrun());
        return false;
    }

    public void launch(Vector3 direction, float forceMagnitude) {
        this.rigid.AddForce(direction.normalized*forceMagnitude, ForceMode.Impulse);
        this.rigid.drag = 0f; //this is such a stupid idea
        Invoke(nameof(returndrag),5f);
    } 
    
    public void returndrag() {
        this.rigid.drag = groundDrag;
    }
}