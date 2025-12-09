using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFactory : MonoBehaviour
{
    public AudioSource sfxSource;

    //yes this is terrible design but like fuck it we ball
    //=========================================================================================
    //ALEX STUFF
    //=========================================================================================
    public AudioClip[] alexLines;
    public BossController baseContext;
    [Header("alex speeds")]
    public float roamSpeed;
    public float blockSpeed;
    public float chargeSpeed;
    public float pokeSpeed;
    public float meteorSpeed;

    [Header("alex state timers")]
    public float blockTime;
    [Header("roam")]
    public float damageToRoamSwapBlock;
    public float chanceRoamCharge;
    public float chanceRoamSlam;
    public float chanceRoamPoke;
    public float minRoamTime;
    public float maxRoamTime;
    public BossAttackHitbox roamBox;
    [Header("charge stuff")]
    public float chargePunchWindup;
    public float chargePunchAimDelay;
    public float chargePunchDuration;
    public float chargePunchForce;
    public float chargePunchDamage;
    public float chargePunchOverdash;
    public BossAttackHitbox chargePunchHitbox;

    [Header("poke state")]
    public float pokeStartCD;
    public int pokeVolleyCount;
    public int pokeShotsPerVolley;
    public float pokeShotCD;
    public float pokeVolleyCD;
    public MissleLauncher pokeLauncher;
    [Header("slam state")]
    public float slamBounceTime;
    public float slamWindTime;
    public float slamJumpTime;
    public float slamPunchTime;
    public Transform slamPunchPos;
    public BossAttackHitbox slamHitbox;
    public float slamDamage;
    public float slamKnockback;
    [Header ("juggle")]
    public Rigidbody juggleRigid;
    public float juggleJumpForce;
    public float juggleJumpCooldown;
    public BossAttackHitbox juggleHitBox;
    public float jugglePunchCD;
    public float juggleDamage;
    public float juggleKnock;
    public float swapJuggleMeteor;
    [Header ("meteor")]
    public BossAttackHitbox meteorPunchHitbox;
    public BossAttackHitbox meteorCraterHitbox;
    public float meteorDuration;
    public float meteorDamage;
    public float metoerImobileFor;


    public BossBaseState Block() {
        sfxSource.clip = alexLines[7];
        sfxSource.loop = false;
        sfxSource.Play();
        return new ABBlockState(baseContext, this, blockSpeed, blockTime);
    }

    public BossBaseState Roam() {
        return new ABRoamState(baseContext, this, roamSpeed, damageToRoamSwapBlock, chanceRoamCharge, chanceRoamSlam, chanceRoamPoke, minRoamTime, maxRoamTime, roamBox);
    }

    public BossBaseState Charge() {
        sfxSource.clip = alexLines[0];
        sfxSource.loop = false;
        sfxSource.Play();
        return new ABChargeState(baseContext, this, chargeSpeed, chargePunchWindup, chargePunchAimDelay, chargePunchDuration, chargePunchForce, chargePunchDamage, chargePunchOverdash, chargePunchHitbox);
    }


    public BossBaseState Poke() {
        // sfxSource.clip = alexLines[9];
        sfxSource.loop = false;
        sfxSource.Play();
        return new ABPokeState(baseContext, this, pokeSpeed, pokeVolleyCount, pokeShotsPerVolley, pokeShotCD, pokeVolleyCD, pokeLauncher, pokeStartCD);
    }

    public BossBaseState Slam() {
        sfxSource.clip = alexLines[1];
        sfxSource.loop = false;
        sfxSource.Play();
        return new ABSlamState(baseContext, this, roamSpeed, slamBounceTime, slamWindTime, slamJumpTime, slamPunchTime, slamPunchPos, slamHitbox, slamDamage, slamKnockback);
    }

    public BossBaseState Juggle() {
        sfxSource.clip = alexLines[2];
        sfxSource.loop = false;
        sfxSource.Play();
        return new ABJuggleState(baseContext, this, 0f, juggleRigid, juggleJumpForce, juggleJumpCooldown, juggleHitBox, jugglePunchCD, juggleDamage, juggleKnock, swapJuggleMeteor);
    }

    public BossBaseState Meteor() {
        sfxSource.clip = alexLines[4];
        sfxSource.loop = false;
        sfxSource.Play();
        return new ABMeteorState(baseContext, this, meteorSpeed, meteorPunchHitbox, meteorCraterHitbox, meteorDuration, meteorDamage, metoerImobileFor);
    }

    //=========================================================================================
    //BEN STUFF
    //=========================================================================================
    public AudioClip[] bensLines;

    [Header ("Ben State speeds")]
    public float cloneSpeed;
    public float engageSpeed;
    public float benPokeSpeed;
    public float smokeSpeed;
    public float marchSpeed;
    [Header ("Clone manager")]
    public CloneManager cloneManager;
    [Header("clone")]
    public float cloneAnimTime;
    [Header("engage")]
    public float benDashPastMag;
    public float benDashTime;
    public WhirlCollider benDashCollider;
    public float dashDamage;
    [Header("poke")]
    public Arm benArm;
    public float benPokeWindupTime;
    [Header("smoke")]
    public float benSmokeMinReappearTime;
    public float benSmokeMaxReappearTime;
    public float benSmokeDuration;
    public GameObject benBossModel;
    public GameObject benSmokeParticles;
    public LayerMask benWalls;
    [Header("march")]
    public WhirlCollider benWhirlColldier; 
    public float benWhirlCooldown;
    public float benWhirlDamage;
    public float swapMarchEngage;
    public float swapMarchClone;
    public float swapMarchPoke;
    public float damageToSwapSmoke;
    public float minMarchTime;
    public float maxMarchTime;

    public BossBaseState Clone() {
        sfxSource.clip = bensLines[0];
        sfxSource.loop = false;
        sfxSource.Play();
        return new BenCloneState(baseContext, this, cloneSpeed, cloneManager, cloneAnimTime);
    }

    public BossBaseState Engage() {
        sfxSource.clip = bensLines[6];
        sfxSource.loop = false;
        sfxSource.Play();
        return new BenEngageState(baseContext, this, engageSpeed, cloneManager, benDashPastMag, benDashTime, benDashCollider, dashDamage);
    }

    public BossBaseState BenPoke() {
        sfxSource.clip = bensLines[2];
        sfxSource.loop = false;
        sfxSource.Play();
        return new BenPokeState(baseContext, this, benPokeSpeed, cloneManager, benArm, benPokeWindupTime);
    }

    public BossBaseState Smoke() {
        sfxSource.clip = bensLines[1];
        sfxSource.loop = false;
        sfxSource.Play();
        return new BenSmokeState(baseContext, this, smokeSpeed, cloneManager, benSmokeMinReappearTime, benSmokeMaxReappearTime, benSmokeDuration, benBossModel, benSmokeParticles, benWalls);
    }

    public BossBaseState March(bool canEngage) {
        sfxSource.clip = bensLines[5];
        sfxSource.loop = false;
        sfxSource.Play();
        return new BenMarchState(baseContext, this, marchSpeed, cloneManager, benWhirlColldier, benWhirlCooldown, benWhirlDamage, swapMarchEngage, swapMarchClone, swapMarchPoke, damageToSwapSmoke, minMarchTime, maxMarchTime, canEngage);
    }



    //=========================================================================================
    //CHLOE STUFF
    //=========================================================================================
    public AudioClip[] chloeLines;

    [Header ("Chloe State speeds")]
    public float airStrikeSpeed;
    public float IdleSpeed;
    public float SnipeSpeed;
    public float SummonSpeed;
    public float SurgeSpeed;
    [Header ("Chloe all state stuff")]
    public Turret chloeTurret;
    public LayerMask chloeWalls;
    public float chloeRandomPointDistance;
    [Header("AirStrike")]
    public int shots;
    public float timeBetweenFire;
    public float timeBetweenFireAndLand;
    public float timeBetweenLand;
    public GameObject shell;
    public GameObject shellIndicator;
    [Header("Idle")]
    public float minIdle;
    public float maxIdle;
    public float swapIdleAir;
    public float swapIdleSnipe;
    public float swapIdleSurge;
    
    [Header("Snipe")]
    public LineRenderer chloeTracer;
    public float chloeSnipeTimer;
    public Transform chloeSnipePoint;
    public LayerMask chloeSnipeLayerMask;
    public float sniperDamage;
    [Header("Summon")]
    public float ignoreMe;
    [Header("Surge")]
    public float chloeDashTime;
    public float chloeStunTime;
    public float chloeOverdashMag;
    public float surgeDamage;
    public BossAttackHitbox surgeHitbox;

    public BossBaseState AirStrike() {
        sfxSource.clip = chloeLines[4];
        sfxSource.loop = false;
        sfxSource.Play();
        return new ChloeAirStrikeState(baseContext, this, airStrikeSpeed, chloeTurret, chloeWalls, chloeRandomPointDistance, shots, timeBetweenFire, timeBetweenFireAndLand, timeBetweenLand, shell, shellIndicator);
    }

    public BossBaseState Idle() {

        return new ChloeIdleState(baseContext, this, IdleSpeed, chloeTurret, chloeWalls, chloeRandomPointDistance, minIdle, maxIdle, swapIdleAir, swapIdleSnipe, swapIdleSurge);
    }

    public BossBaseState Snipe() {
        sfxSource.clip = chloeLines[1];
        sfxSource.loop = false;
        sfxSource.Play();
        return new ChloeSnipeState(baseContext, this, SnipeSpeed, chloeTurret, chloeWalls, chloeRandomPointDistance, chloeTracer, chloeSnipeTimer, chloeSnipePoint, chloeSnipeLayerMask, sniperDamage);
    }

    public BossBaseState Summon() {
        sfxSource.clip = chloeLines[2];
        sfxSource.loop = false;
        sfxSource.Play();
        return new ChloeSummonState(baseContext, this, SummonSpeed, chloeTurret, chloeWalls, chloeRandomPointDistance);
    }

    public BossBaseState Surge() {
        sfxSource.clip = chloeLines[3];
        sfxSource.loop = false;
        sfxSource.Play();
        return new ChloeSurgeState(baseContext, this, SurgeSpeed, chloeTurret, chloeWalls, chloeRandomPointDistance, chloeDashTime, chloeStunTime, chloeOverdashMag, surgeDamage, surgeHitbox);
    }



    //=========================================================================================
    //RACHEL STUFF
    //=========================================================================================
    public AudioClip[] rachelLines;
    [Header ("Rachel State speeds")]
    public float bombardSpeed;
    public float flySpeed;
    public float humSpeed;
    public float mineSpeed;
    public float singSpeed;
    public float tetherSpeed;
    [Header ("Rachel Player distance")]
    public float bombardDist;
    public float flyDist;
    public float humDist;
    public float mineDist;
    public float singDist;
    public float tetherDist;
    [Header("bombard")]
    public GameObject bombardNotes;
    public GameObject bombardDeathBox;
    public float bombardDuration;
    public float bombardBoxDamageTime;
    [Header("hum")]
    public int humRingsToShoot;
    public float humCooldownBetweenRing;
    public Mouth humMouth;
    [Header("mine")]
    public GameObject mineMine;
    public float timeBetweenMine;
    public float mineDashTime;
    public float mineEnterAnimTime;
    public float mineExitAnimTime;
    public Transform mineSpawnTransform;
    public float mineOverDash;
    [Header("sing")]
    public int singRingsToShoot;
    public float singTimeBetweenRing;
    public float singDashTime;
    public Mouth singMouth;
    public float singDownTime;
    public float swapSingSing;
    [Header("tether")]
    public float tetherDuration;
    public float tetherAnimationDuration;
    public Tether tether;
    public float swapTetherBombard;
    public float swapTetherHum;
    [Header("fly")]
    public float swapFlyBombard;
    public float swapFlyTether;
    public float swapFlyHum;
    public float minFlyTime;
    public float maxFlyTime;

    public BossBaseState Bombard() {
        // hear this boss music? i wrote it bih !
        sfxSource.clip = rachelLines[0];
        sfxSource.loop = false;
        sfxSource.Play();
        return new RachelBombardState(baseContext, this, bombardSpeed, bombardDist, bombardNotes, bombardDeathBox, bombardDuration, bombardBoxDamageTime);
    }

    public BossBaseState Fly() {

        return new RachelFlyState(baseContext, this, flySpeed, flyDist, swapFlyTether, swapFlyHum, swapFlyBombard, minFlyTime, maxFlyTime);
    }

    public BossBaseState Hum() {
        sfxSource.clip = rachelLines[1];
        sfxSource.loop = false;
        sfxSource.Play();
        return new RachelHumState(baseContext, this, humSpeed, humDist, humRingsToShoot, humCooldownBetweenRing, humMouth);
    }

    public BossBaseState Mine() {
        sfxSource.clip = rachelLines[2];
        sfxSource.loop = false;
        sfxSource.Play();
        return new RachelMineState(baseContext, this, mineSpeed, mineDist, mineMine, timeBetweenMine, mineDashTime, mineEnterAnimTime, mineExitAnimTime, mineSpawnTransform, mineOverDash);
    }

    public BossBaseState Sing() {
        sfxSource.clip = rachelLines[6];
        sfxSource.loop = false;
        sfxSource.Play();
        return new RachelSingState(baseContext, this, singSpeed, singDist, singRingsToShoot, singTimeBetweenRing, singDashTime, singMouth, singDownTime, swapSingSing);
    }

    public BossBaseState Tether() {
        sfxSource.clip = rachelLines[4];
        sfxSource.loop = false;
        sfxSource.Play();
        return new RachelTetherState(baseContext, this, tetherSpeed, tetherDist, tetherDuration, tetherAnimationDuration, tether, swapTetherBombard, swapTetherHum);
    }


    //=========================================================================================
    //RACKET STUFF
    //=========================================================================================
}
