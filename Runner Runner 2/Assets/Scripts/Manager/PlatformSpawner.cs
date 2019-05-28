using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

    public GameObject PlatformGO;
    private GameObject levelGround;
    private GameObject platformContainer;

    private int spawnAmount = 20;
    private float levelPadding = 5.0f;


    private void Awake() {
        levelGround = GameObject.Find("Level Ground");
        platformContainer = GameObject.Find("Platform Container");

        spawnAmount = Mathf.RoundToInt(levelGround.transform.localScale.x * levelGround.transform.localScale.z);

        float groundX = levelGround.transform.position.x;
        float groundZ = levelGround.transform.position.z;

        float levelX = levelGround.transform.localScale.x * 10;
        float levelZ = levelGround.transform.localScale.z * 10;

        float minX = 0 - levelX / 2 + levelPadding; // -5
        float maxX = 0 + levelX / 2 - levelPadding; // +5
        float minZ = 0 - levelZ / 2 + levelPadding; // -45
        float maxZ = 0 + levelZ / 2 - levelPadding; // +45

        for (int i = 0; i < spawnAmount; i++) {
            float rndX = groundX + Random.Range(minX, maxX);
            float rndZ = groundZ + Random.Range(minZ, maxZ);

            GameObject newPlatform = Instantiate(PlatformGO);
            newPlatform.transform.parent = platformContainer.transform;
            newPlatform.transform.localPosition = new Vector3(rndX, newPlatform.transform.position.y, rndZ);
        }
    }

}
