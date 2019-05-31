using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static int PlayerCount = 0;
    public static int ConnectedGamepads = 0;

    private GamepadManager gamepadManagerScript;
    private PlatformSpawner platformSpawnerScript;
    private CharacterSpawner characterSpawnerScript;


    private void Awake() {
        gamepadManagerScript = GetComponent<GamepadManager>();
        platformSpawnerScript = GetComponent<PlatformSpawner>();
        characterSpawnerScript = GetComponent<CharacterSpawner>();

        gamepadManagerScript.InitializeGamepads();
        platformSpawnerScript.SpawnPlatforms();
        characterSpawnerScript.SpawnCharacters();
    }

}
