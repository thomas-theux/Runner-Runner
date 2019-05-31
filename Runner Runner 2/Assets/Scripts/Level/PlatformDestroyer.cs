using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Platform") {
            print(other.name);
            Destroy(other.gameObject);
        }
    }

}
