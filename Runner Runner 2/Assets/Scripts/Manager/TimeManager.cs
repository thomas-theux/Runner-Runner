using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public static int CurrentTimer = 0;

    // public static bool StartLevel = false;
    // public static bool StartRun = false;
    // public static bool EndRun = false;

    public static float CurrentTime = 0;

    private float levelCountdown;
    private float levelDuration;
    private float lastSeconds;


    public void StartLevelTimer() {
        CurrentTimer = 1;

        levelCountdown = SettingsManager.LevelCountdown;
        levelDuration = SettingsManager.LevelDuration;
        lastSeconds = SettingsManager.LastSeconds;
    }


    private void Update() {
        if (CurrentTimer == 1) {
            LevelCountdown();
        }

        if (CurrentTimer == 2) {
            LevelDuration();
        }

        if (CurrentTimer == 3) {
            LastSeconds();
        }
    }


    private void LevelCountdown() {
        levelCountdown -= Time.deltaTime;
        CurrentTime = levelCountdown;

        if (levelCountdown <= 0.5f) {
            CurrentTimer = 2;
        }
    }


    private void LevelDuration() {
        levelDuration -= Time.deltaTime;
        CurrentTime = levelDuration;

        if (levelDuration <= 0.5f) {
            CurrentTimer = 3;
        }
    }


    private void LastSeconds() {
        lastSeconds -= Time.deltaTime;
        CurrentTime = lastSeconds;

        if (lastSeconds <= 0.5f) {
            CurrentTimer = 0;
        }
    }

}
