using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public virtual void takeDamage(float damage) {
        Debug.Log("I AM HIT FOR" + damage);
    }

}
