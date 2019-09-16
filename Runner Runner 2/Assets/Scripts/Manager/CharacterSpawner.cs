using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour {

    public GameObject CharacterGO;
    private GameObject spawnGO;

    private Camera characterCam;


    public void SpawnCharacters() {
        spawnGO = GameObject.Find("Start Spawn");

        for (int i = 0; i < GameSettings.PlayerCount; i++) {
            GameObject newCharacter = Instantiate(CharacterGO);
            newCharacter.transform.position = spawnGO.transform.position;
            newCharacter.GetComponent<PlayerSheet>().playerID = i;

            characterCam = newCharacter.transform.GetChild(2).GetComponent<Camera>();

            AdjustCameras(characterCam, i);
        }
    }


    private void AdjustCameras(Camera characterCam, int playerID) {
        float camWidth = 0.5f;
        float camHeight = 0.5f;
        float camPosX = 0;
        float camPosY = 0;

        switch (playerID) {
            case 0:
                camPosX = 0.0f;
                camPosY = 0.5f;
                break;
            case 1:
                camPosX = 0.5f;
                camPosY = 0.5f;
                break;
            case 2:
                camPosX = 0.0f;
                camPosY = 0.0f;
                break;
            case 3:
                camPosX = 0.5f;
                camPosY = 0.0f;
                break;
        }

        if (GameSettings.PlayerCount == 2) {
            camPosY = 0.0f;
            camHeight = 1.0f;
        }

        // Single player camera for dev testing
        if (GameSettings.PlayerCount == 1) {
            camPosX = 0.0f;
            camPosY = 0.0f;
            camHeight = 1.0f;
            camWidth = 1.0f;
        }

        characterCam.rect = new Rect(camPosX, camPosY, camWidth, camHeight);
        characterCam.GetComponent<CameraFollow>().RootParent();
    }

}
