using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneManager : MonoBehaviour
{
    public GameObject clonePrefab;
    public List<BenClone> clones;

    public void Start() {
        clones = new List<BenClone>();
    }


    public void spawnClone(Vector3 clonePos) {
        GameObject clone = Instantiate(clonePrefab, clonePos, Quaternion.identity);
        BenClone ben = clone.GetComponent<BenClone>();
        clones.Add(ben);
    }

    public void cloneWhirl() {
        for(int i = 0; i < clones.Count; i++) {
            clones[i].whirl();
        }
    }

    public bool checkCloneWhirl() {
        for(int i = 0; i < clones.Count; i++) {
            if(clones[i].shouldWhirl()) {
                return true;
            }
        }
        return false;
    }

    public void cloneDash(Vector3 target) {
        for(int i = 0; i < clones.Count; i++) {
            clones[i].dash(target);
        }
    }

    public void cloneToss() {
        for(int i = 0; i < clones.Count; i++) {
            clones[i].toss();
        }
    }

    public void cloneSleep() {
        for(int i = 0; i < clones.Count; i++) {
            clones[i].sleep();
        }
    }

    public void cloneWake() {
        for(int i = 0; i < clones.Count; i++) {
            clones[i].wake();
        }
    }

    public void wipe() {
        for(int i = clones.Count-1; i >= 0; i--) {
            Destroy(clones[i].gameObject);
        }

        this.clones = new List<BenClone>();
    }
}
