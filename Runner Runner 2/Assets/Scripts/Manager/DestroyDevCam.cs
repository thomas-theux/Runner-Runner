using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDevCam : MonoBehaviour {

    // public Camera DevCam;

    private void Awake() {
        // Destroy(this.DevCam);
        Destroy(this.gameObject);
    }
}
