using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTimer : MonoBehaviour {

    public PlayerSheet PlayerSheetScript;

    public TMP_Text StartTimer;
    public TMP_Text LevelTimer;
    public TMP_Text BestTime;
    public TMP_Text CurrentRunTimer;

    public float CurrentRunTimes = 0f;

    private float levelCountdown;


    public void Awake() {
        levelCountdown = GameSettings.LevelCountdown + GameSettings.AdditionalTime;
    }


    private void Update() {
        if (TimeManager.TimerIndex > 0) {
            DisplayTimers();
        } else {
            StartTimer.text = "";
            LevelTimer.text = "";
            CurrentRunTimer.text = "";
        }

        if (PlayerSheetScript.isRespawning) {
            RespawnCountdown();
        }
    }


    private void DisplayTimers() {
        if (TimeManager.TimerIndex == 1) {
            StartTimer.text = Mathf.Floor(TimeManager.CurrentTime) + "";
        }

        if (TimeManager.TimerIndex > 1) {
            if (!PlayerSheetScript.isDead && !PlayerSheetScript.isRespawning) {
                CurrentRunTimes += Time.deltaTime;
            }

            StartTimer.text = "";
            LevelTimer.text = FormatLevelTime(CurrentRunTimes);
            CurrentRunTimer.text = FormatCurrentRunTime();
        }

        if (TimeManager.TimerIndex == 3) {
            LevelTimer.fontSize = 140;
            LevelTimer.color = ColorManager.KeyRed;
        }
    }


    private void RespawnCountdown() {
        if (levelCountdown <= 1.0f) {
            levelCountdown = GameSettings.LevelCountdown + GameSettings.AdditionalTime;
        }
        
        levelCountdown -= Time.deltaTime * GameSettings.InitialCountdownMultiplier;
        StartTimer.text = Mathf.Floor(levelCountdown) + "";

        if (levelCountdown <= 1.0f) {
            PlayerSheetScript.isRespawning = false;
            StartTimer.text = "";
        }
    }


    private string FormatLevelTime(float time) {
        int minutes = (int) TimeManager.CurrentTime / 60;
        int seconds = (int) TimeManager.CurrentTime - 60 * minutes;

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    private string FormatCurrentRunTime() {
        int minutes = (int) CurrentRunTimes / 60;
        int seconds = (int) CurrentRunTimes - 60 * minutes;
        int milliseconds = (int) (100 * (CurrentRunTimes - minutes * 60 - seconds));

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

}
