using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpawner : MonoBehaviour {

    public GameObject PolaroidBG;
    public GameObject ButtonsContainer;


    private void Awake() {
        GameObject newPolaroidBG = Instantiate(PolaroidBG);
        ButtonsContainer = newPolaroidBG.transform.GetChild(0).transform.GetChild(2).gameObject;
    }
}
