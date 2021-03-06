﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static List<GameObject> AllPlayers = new List<GameObject>();

    public static List<int> RankingsArr = new List<int>();

    private GamepadManager gamepadManagerScript;
    private PlatformSpawner platformSpawnerScript;
    private CharacterSpawner characterSpawnerScript;
    private TimeManager timeManagerScript;


    private void Start() {
        gamepadManagerScript = GetComponent<GamepadManager>();
        platformSpawnerScript = GetComponent<PlatformSpawner>();
        characterSpawnerScript = GetComponent<CharacterSpawner>();
        timeManagerScript = GetComponent<TimeManager>();

        gamepadManagerScript.InitializeGamepads();
        platformSpawnerScript.SpawnPlatforms();
        characterSpawnerScript.SpawnCharacters();
        timeManagerScript.StartLevelTimer();

        // Prepopulate rankings array with player count
        for (int i = 0; i < AllPlayers.Count; i++) {
            RankingsArr.Add(99);
        }
    }

}