using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour {

    public GameObject CharacterGO;
    private GameObject spawnGO;

    public Material[] CharacterColors;

    private Camera characterCam;

    private float basicHeightMarginSingle = 0;
    private float basicHeightMarginDouble = 0;

    private float basicWidthMarginSingle = 0;
    private float basicWidthMarginDouble = 0;
    private float specialMarginBottom = 0;

    private float basicPixelMarginSingle = 10;
    private float basicPixelMarginDouble = 20;
    private float specialPixelMarginBottom = 60;


    private void Awake() {
        // Calculate basic width margin for camera's viewport based on screen width
        basicWidthMarginSingle = 1 / (Screen.width / basicPixelMarginSingle);
        basicWidthMarginDouble = 1 / (Screen.width / basicPixelMarginDouble);

        // Calculate basic height margin for camera's viewport based on screen height
        basicHeightMarginSingle = 1 / (Screen.height / basicPixelMarginSingle);
        basicHeightMarginDouble = 1 / (Screen.height / basicPixelMarginDouble);

        // Calculate special bottom margin for camera's viewport based on screen height
        specialMarginBottom = 1 / (Screen.height / specialPixelMarginBottom);
    }


    public void SpawnCharacters() {
        spawnGO = GameObject.Find("Start Spawn");

        for (int i = 0; i < GameSettings.PlayerCount; i++) {
            GameObject newCharacter = Instantiate(CharacterGO);
            newCharacter.transform.position = spawnGO.transform.position;
            newCharacter.GetComponent<PlayerSheet>().playerID = i;

            // Color head and body of character to according player color
            newCharacter.GetComponent<PlayerSheet>().CharacterHead.GetComponent<Renderer>().material = CharacterColors[i];
            newCharacter.GetComponent<PlayerSheet>().CharacterBody.GetComponent<Renderer>().material = CharacterColors[i];

            // Add newly spawned character to an AllPlayers array
            GameManager.AllPlayers.Add(newCharacter);

            characterCam = newCharacter.transform.GetChild(2).GetComponent<Camera>();

            AdjustCameras(characterCam, i);
        }
    }


    private void AdjustCameras(Camera characterCam, int playerID) {
        // Calculate new width with margins
        float camWidth = 0.5f - (basicWidthMarginDouble + (basicWidthMarginSingle / 2));
        float camHeight = 0.5f - (basicHeightMarginDouble + (basicHeightMarginSingle / 2) + (specialMarginBottom / 2));

        // float camWidth = 0.5f;
        // float camHeight = 0.5f;
        float camPosX = 0;
        float camPosY = 0;

        switch (playerID) {
            case 0:
                // camPosX = 0.0f;
                // camPosY = 0.5f;
                camPosX = basicWidthMarginDouble;
                camPosY = 0.5f + (basicHeightMarginSingle / 2) + (specialMarginBottom / 2);
                break;
            case 1:
                // camPosX = 0.5f;
                // camPosY = 0.5f;
                camPosX = 0.5f + (basicWidthMarginSingle / 2);
                camPosY = 0.5f + (basicHeightMarginSingle / 2) + (specialMarginBottom / 2);
                break;
            case 2:
                // camPosX = 0.0f;
                // camPosY = 0.0f;
                camPosX = basicWidthMarginDouble;
                camPosY = basicHeightMarginDouble + specialMarginBottom;

                // Display the 3rd player camera in the bottom center (3 players only)
                // if (GameSettings.PlayerCount == 3) { camPosX = 0.25f; }
                break;
            case 3:
                // camPosX = 0.5f;
                // camPosY = 0.0f;
                camPosX = 0.5f + (basicWidthMarginSingle / 2);
                camPosY = basicHeightMarginDouble + specialMarginBottom;
                break;
        }

        if (GameSettings.PlayerCount == 2) {
            camPosY = 0.0f + basicHeightMarginDouble + specialMarginBottom;
            camHeight = 1.0f - (basicHeightMarginDouble * 2) - specialMarginBottom;
        }

        // Single player camera for dev testing
        if (GameSettings.PlayerCount == 1) {
            // camPosX = 0.0f;
            // camPosY = 0.0f;
            // camWidth = 1.0f;
            // camHeight = 1.0f;

            camPosX = basicWidthMarginDouble;
            camPosY = basicHeightMarginDouble + specialMarginBottom;
            camWidth = 1.0f - (basicWidthMarginDouble * 2);
            camHeight = 1.0f - (basicHeightMarginDouble * 2) - specialMarginBottom;
        }

        characterCam.rect = new Rect(camPosX, camPosY, camWidth, camHeight);
        characterCam.GetComponent<CameraFollow>().RootParent();
    }

}
