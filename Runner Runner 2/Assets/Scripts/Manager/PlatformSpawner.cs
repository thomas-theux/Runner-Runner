﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

    public GameObject PlatformGO;
    public GameObject StartGO;

    public GameObject FinishSprintGO;
    // public GameObject FinishParcoursGO;

    private GameObject levelGround;
    private GameObject platformContainer;

    private float levelSizeZ = 0;

    private int spawnAmount = 20;
    private float levelPadding = 5.0f;


    public void SpawnPlatforms() {
        switch (GameSettings.SelectedLevelType) {
            case 0:
                SprintMode();
                break;
            case 1:
                ParcoursMode();
                break;
        }
    }


    private void SprintMode() {
        levelGround = GameObject.Find("Level Ground");
        platformContainer = GameObject.Find("Platform Container");

        // Change scale of level ground according to game settings
        switch (GameSettings.SelectedLevelSize) {
            case 0:
                levelSizeZ = 6;
                break;
            case 1:
                levelSizeZ = 12;
                break;
            case 2:
                levelSizeZ = 24;
                break;
            case 3:
                levelSizeZ = 100;
                break;
        }

        levelGround.transform.localScale = new Vector3(
            levelGround.transform.localScale.x,
            levelGround.transform.localScale.y,
            levelSizeZ
        );

        spawnAmount = Mathf.RoundToInt(levelGround.transform.localScale.x * levelGround.transform.localScale.z);

        float groundX = levelGround.transform.position.x;
        float groundZ = levelGround.transform.position.z;

        float levelX = levelGround.transform.localScale.x * 10;
        float levelZ = levelGround.transform.localScale.z * 10;

        float minX = 0 - levelX / 2 + levelPadding; // -5
        float maxX = 0 + levelX / 2 - levelPadding; // +5
        float minZ = 0 - levelZ / 2 + levelPadding; // -45
        float maxZ = 0 + levelZ / 2 - levelPadding; // +45


        // Spawn start and finish platforms
        float startFinishZ = levelZ / 2 - levelPadding;

        GameObject newStart = Instantiate(StartGO);
        newStart.transform.parent = platformContainer.transform;
        newStart.transform.localPosition = new Vector3(0, newStart.transform.position.y, -startFinishZ);

        GameObject newFinish = Instantiate(FinishSprintGO);
        newFinish.transform.parent = platformContainer.transform;
        newFinish.transform.localPosition = new Vector3(0, newFinish.transform.position.y, startFinishZ);


        for (int i = 0; i < spawnAmount; i++) {
            float rndX = groundX + Random.Range(minX, maxX);
            float rndZ = groundZ + Random.Range(minZ, maxZ);

            GameObject newPlatform = Instantiate(PlatformGO);
            newPlatform.transform.parent = platformContainer.transform;
            newPlatform.transform.localPosition = new Vector3(rndX, newPlatform.transform.position.y, rndZ);
        }
    }


    private void ParcoursMode() {
    }

}