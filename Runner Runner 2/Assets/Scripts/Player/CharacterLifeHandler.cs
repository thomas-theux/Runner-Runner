using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLifeHandler : MonoBehaviour {

    private GameObject spawnGO;

    private PlayerSheet playerSheetScript;
    public DisplayTimer DisplayTimerScript;

    private Rigidbody rb;
    private GameObject characterModel;

    private float respawnDelayTime = 1.0f;


    private void Awake() {
        spawnGO = GameObject.Find("Start Spawn");

        playerSheetScript = this.GetComponent<PlayerSheet>();
        rb = GetComponent<Rigidbody>();
        characterModel = this.gameObject.transform.GetChild(0).gameObject;

        // Set different respawn times for different game modes
        switch (GameSettings.SelectedGameMode) {
            case 0:
                respawnDelayTime = 1.0f;
                break;
            case 1:
                respawnDelayTime = 0.1f;
                break;
        }
    }


    public void KillCharacter(bool didFinish) {
        StartCoroutine(KillDelay(didFinish));
    }


    private IEnumerator KillDelay(bool didFinish) {
        yield return new WaitForSeconds(0.1f);

        DisableCharacter(didFinish);
    }


    private void DisableCharacter(bool didFinish) {
        this.playerSheetScript.isDead = true;
        this.rb.isKinematic = true;

        if (!didFinish) {
            this.characterModel.SetActive(false);
        }

        StartCoroutine(RespawnDelay(didFinish));
    }


    private IEnumerator RespawnDelay(bool didFinish) {
        float delayRespawn = respawnDelayTime;

        // If the player finished the level, there will be a bigger delay until respawn
        if (didFinish) {
            delayRespawn = GameSettings.RespawnDelayTime;
            yield return new WaitForSeconds(delayRespawn);
            AudioManager.instance.Play("ResetCharacter");
        } else {
            AudioManager.instance.Play("ResetCharacter");
            yield return new WaitForSeconds(delayRespawn);
        }

        if (TimeManager.TimerIndex > 0) {
            this.transform.position = spawnGO.transform.position;
            this.playerSheetScript.isDead = false;
            this.rb.isKinematic = false;
            this.characterModel.SetActive(true);

            this.playerSheetScript.groundToRespawn = true;

            // Reset current run timer when dying or resetting
            DisplayTimerScript.CurrentRunTimes = 0;

            playerSheetScript.isRespawning = true;
        }
    }
}
