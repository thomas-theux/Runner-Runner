using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachedFinishSprint : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Character") {
            TimeManager.RunEnds(other.gameObject);
        }
    }

}
