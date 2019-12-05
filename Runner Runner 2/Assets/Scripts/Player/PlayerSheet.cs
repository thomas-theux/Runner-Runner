using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSheet : MonoBehaviour {

    public GameObject CharacterHead;
    public GameObject CharacterBody;

    public int playerID = 0;
    public bool isDead = false;
    public bool isRespawning = false;

    public float BestRunTime = 99999;


    private void Awake() {
        BestRunTime = 99999;
    }

}
