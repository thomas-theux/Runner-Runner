﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    // 0 = game didn't start yet or is already finished
    // 1 = level start countdown
    // 2 = level started
    // 3 = last seconds are counting down
    public static float CurrentTime;
    public static int TimerIndex = 0;

    public static List<float> PlayersBestTimesArr = new List<float>();
    public static List<float> SortedBestTimesArr = new List<float>();

    public static bool ShowResults = false;

    private bool isTicking = false;

    private float levelCountdown;
    private float levelDuration;
    private float lastSeconds;


    public void StartLevelTimer() {
        // Add the initial best time for each player
        for (int i = 0; i < GameSettings.PlayerCount; i++) {
            float initialBestRunTime = GameManager.AllPlayers[i].GetComponent<PlayerSheet>().BestRunTime;
            PlayersBestTimesArr.Add(initialBestRunTime);
            SortedBestTimesArr.Add(initialBestRunTime);
        }

        levelCountdown = GameSettings.LevelCountdown + GameSettings.AdditionalTime;
        levelDuration = GameSettings.LevelDuration + GameSettings.AdditionalTime;
        lastSeconds = GameSettings.LastSeconds + GameSettings.AdditionalTime;

        StartCoroutine(LevelStartDelay());
    }


    private IEnumerator LevelStartDelay() {
        yield return new WaitForSeconds(GameSettings.InitialDelay);

        TimerIndex = 1;
    }


    private void Update() {
        if (TimerIndex == 1) {
            if (!isTicking) {
                isTicking = true;
                StartCoroutine(TickingSound());
            }

            LevelCountdown();
        }

        if (TimerIndex == 2) {
            isTicking = false;
            LevelDuration();
        }

        if (TimerIndex == 3) {
            LastSeconds();
        }
    }


    private void LevelCountdown() {
        levelCountdown -= Time.deltaTime * GameSettings.InitialCountdownMultiplier;
        CurrentTime = levelCountdown;

        if (levelCountdown <= 1.0f) {
            TimerIndex = 2;
            FindObjectOfType<AudioManager>().Play("LevelStart");
        }
    }


    private void LevelDuration() {
        levelDuration -= Time.deltaTime;
        CurrentTime = levelDuration;

        if (levelDuration <= lastSeconds) {
            TimerIndex = 3;
        }
    }


    private void LastSeconds() {
        lastSeconds -= Time.deltaTime;
        CurrentTime = lastSeconds;

        if (lastSeconds <= 1.0f) {
            TimerIndex = 0;
        }
    }


    public static void SortBestTimesArray() {
        for (int i = 0; i < PlayersBestTimesArr.Count; i++) {
            SortedBestTimesArr[i] = PlayersBestTimesArr[i];
        }

        SortedBestTimesArr.Sort();
    }


    public static void UpdatePlayerRanks() {
        for (int i = 0; i < PlayersBestTimesArr.Count; i++) {
            int getRank = SortedBestTimesArr.IndexOf(PlayersBestTimesArr[i]);
            // print(getRank);
            GameManager.AllPlayers[i].GetComponent<CharacterLifeHandler>().DisplayTimerScript.PlayerRank.text = getRank + 1 + "/" + GameSettings.PlayerCount;
        }
    }


    public static void RunEnds(GameObject raceWinner) {
        FindObjectOfType<AudioManager>().Play("LevelEnd");

        TimerIndex = 0;
        ShowResults = true;

        int winnerID = raceWinner.GetComponent<PlayerSheet>().playerID;
        // print("Player " + winnerID + " wins!");
        GameManager.RankingsArr.Add(winnerID);
    }


    private IEnumerator TickingSound() {
        while (isTicking) {
            FindObjectOfType<AudioManager>().Play("TimerTicking");
            yield return new WaitForSeconds(1.0f / GameSettings.InitialCountdownMultiplier);
        }
    }

}