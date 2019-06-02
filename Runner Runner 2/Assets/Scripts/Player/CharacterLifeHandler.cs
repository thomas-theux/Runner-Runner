using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLifeHandler : MonoBehaviour {

    private GameObject spawnGO;

    private PlayerSheet playerSheetScript;
    private Rigidbody rb;
    private GameObject characterModel;

    private float respawnDelayTime = 1.0f;


    private void Awake() {
        spawnGO = GameObject.Find("Start Spawn");

        playerSheetScript = this.GetComponent<PlayerSheet>();
        rb = GetComponent<Rigidbody>();
        characterModel = this.gameObject.transform.GetChild(0).gameObject;
    }


    public void KillCharacter() {
        this.playerSheetScript.isDead = true;
        this.rb.isKinematic = true;
        this.characterModel.SetActive(false);

        StartCoroutine(RespawnDelay());
    }


    private IEnumerator RespawnDelay() {
        yield return new WaitForSeconds(respawnDelayTime);

        if (TimeManager.TimerIndex > 0) {
            this.transform.position = spawnGO.transform.position;
            this.playerSheetScript.isDead = false;
            this.rb.isKinematic = false;
            this.characterModel.SetActive(true);
        }
    }
}
