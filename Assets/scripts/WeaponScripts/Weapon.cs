using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [Header("weapon stats")]
    public int magSize;
    public float firerate;
    public float reloadTime;
    public float falloff_start;
    public int falloff_end;
    public float damage;

    [Header("spread and recoil")]
    public bool hasSpread = false;
    public Vector3 spread = new Vector3(0,0,0);

    [Header("physics smile")]
    public Transform origin;

    [Header("cosmetic")]
    public Animator animator;
    public Transform weaponTip;
    public ParticleSystem gunFlash;
    public ParticleSystem hitMarker;
    public LineRenderer tracer;
    
    [Header("not private :)")]
    public int ammo;
    public bool reloading = false;
    public float prevShot;
    public AudioSource audioSource;
    public AudioClip[] soundEffects;    // 0 is shoot, 1 is reload
    

    public void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void Update() {
        if (Input.GetMouseButtonDown(0)) {
            shoot();
        } else if(Input.GetKey(KeyCode.R) && prevShot + firerate < Time.time) {
            this.reload();
        }
    }

    public virtual void shoot() 
    {
        if(prevShot + firerate < Time.time  && ammo > 0 && !reloading) {
            audioSource.clip = soundEffects[0];
            audioSource.Play();
            ammo-=1;
            prevShot = Time.time;

            // gunFlash.Play();
            animator.SetTrigger("PlayShoot");

            RaycastHit hit;
            if (Physics.Raycast(origin.position, origin.forward, out hit, falloff_end))
            {
                LineRenderer trail = Instantiate(tracer, weaponTip.position, Quaternion.identity);
                trail.positionCount = 2;
                StartCoroutine(makeTracer(trail, hit.point, 1f));
                Enemy enemy = hit.collider.GetComponentInParent<Enemy>();


                if (enemy != null)
                {
                    enemy.takeDamage(damage);
                }
            } else {
                LineRenderer trail = Instantiate(tracer, weaponTip.position, Quaternion.identity);
                trail.positionCount = 2;
                StartCoroutine(makeTracer(trail, weaponTip.position + falloff_end*origin.forward, 1f));
            }
        } else if(ammo <= 0 && prevShot + firerate < Time.time) {
            reload();
        }
        
    }

    public virtual void reload() {
        audioSource.clip = soundEffects[1];
        audioSource.Play();
        if(!reloading) {
            animator.SetTrigger("PlayReload");
            reloading = true;
            Invoke(nameof(handleReload), reloadTime);
        }
    }
    public virtual void handleReload() {
        this.reloading = false;
        this.ammo = magSize;
    }

    protected virtual IEnumerator makeTracer(LineRenderer line, Vector3 tracerEnd, float duration) {
        float time = 0;
        line.SetPosition(0,weaponTip.position);
        line.SetPosition(1,tracerEnd);

        Color initialColor = line.material.color;

        while(time < duration)
        {
            time += Time.deltaTime;
            line.material.color = new Color(initialColor.r, initialColor.g,initialColor.b, initialColor.a * (1 - time/duration));
            yield return null;
        }
        if(time >= duration) {
            Destroy(line.gameObject);
        }
        
    }

}