using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpawner : MonoBehaviour {

    public GameObject PolaroidBG;


    private void Awake() {
        GameObject newPolaroidBG = Instantiate(PolaroidBG);
    }
}
