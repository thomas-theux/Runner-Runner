﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTimer : MonoBehaviour {

    public PlayerSheet PlayerSheetScript;

    public TMP_Text StartTimer;
    public TMP_Text LevelTimer;
    public TMP_Text BestTime;
    public TMP_Text PlayerRank;
    public TMP_Text CurrentRunTimer;

    public GameObject StartTimerGO;
    public GameObject LevelTimerGO;
    public GameObject BestTimeGO;
    public GameObject PlayerRankGO;
    public GameObject CurrentRunTimerGO;

    public float CurrentRunTimes = 0f;

    private float levelCountdown;
    public bool isTicking = false;


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
            if (!isTicking) {
                isTicking = true;
                StartCoroutine(TickingSound());
            }

            RespawnCountdown();
        }
    }


    private void DisplayTimers() {
        if (TimeManager.TimerIndex == 1) {
            // Display start time label
            if (!StartTimerGO.activeSelf) { StartTimerGO.SetActive(true); }
            StartTimer.text = Mathf.Floor(TimeManager.CurrentTime) + "";
        }

        if (TimeManager.TimerIndex > 1) {
            if (!PlayerSheetScript.isDead && !PlayerSheetScript.isRespawning) {
                CurrentRunTimes += Time.deltaTime;
            }

            // Display level time label & disable start time label
            if (StartTimerGO.activeSelf) { StartTimerGO.SetActive(false); }
            if (!LevelTimerGO.activeSelf) { LevelTimerGO.SetActive(true); }
            if (!CurrentRunTimerGO.activeSelf) { CurrentRunTimerGO.SetActive(true); }
            if (!BestTimeGO.activeSelf) { BestTimeGO.SetActive(true); }
            if (!PlayerRankGO.activeSelf) { PlayerRankGO.SetActive(true); }

            StartTimer.text = "";
            LevelTimer.text = FormatLevelTime(CurrentRunTimes);
            CurrentRunTimer.text = FormatCurrentRunTime();
        }

        if (TimeManager.TimerIndex == 3) {
            // LevelTimer.fontSize = 140;
            LevelTimer.color = ColorManager.KeyRed;
        }
    }


    private void RespawnCountdown() {
        if (levelCountdown <= 1.0f) {
            levelCountdown = GameSettings.LevelCountdown + GameSettings.AdditionalTime;
        }

        levelCountdown -= Time.deltaTime * GameSettings.InitialCountdownMultiplier;
        if (!StartTimerGO.activeSelf) { StartTimerGO.SetActive(true); }
        StartTimer.text = Mathf.Floor(levelCountdown) + "";

        if (levelCountdown <= 1.0f) {
            isTicking = false;
            PlayerSheetScript.isRespawning = false;
            if (StartTimerGO.activeSelf) { StartTimerGO.SetActive(false); }
            StartTimer.text = "";
            AudioManager.instance.Play("LevelStart");
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


    private IEnumerator TickingSound() {
        while (isTicking) {
            AudioManager.instance.Play("TimerTicking");
            yield return new WaitForSeconds(1.0f / GameSettings.InitialCountdownMultiplier);
        }
    }

}
