using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingFloor : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Character") {
            AudioManager.instance.Play("KillCharacter");
            other.GetComponent<CharacterLifeHandler>().KillCharacter(false);
        }
    }

}
