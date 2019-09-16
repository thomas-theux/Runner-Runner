using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    private float initialDelay = 1.0f;
    private float initialCountdownMultiplier = 1.0f;
    private float additionalTime = 0.9f;

    // 0 = game didn't start yet or is already finished
    // 1 = level start countdown
    // 2 = level started
    // 3 = last seconds are counting down
    public static float CurrentTime;
    public static int TimerIndex = 0;

    public static bool ShowResults = false;

    private float levelCountdown;
    private float levelDuration;
    private float lastSeconds;


    public void StartLevelTimer() {
        levelCountdown = SettingsManager.LevelCountdown + additionalTime;
        levelDuration = SettingsManager.LevelDuration + additionalTime;
        lastSeconds = SettingsManager.LastSeconds + additionalTime;

        StartCoroutine(LevelStartDelay());
    }


    private IEnumerator LevelStartDelay() {
        yield return new WaitForSeconds(initialDelay);

        TimerIndex = 1;
    }


    private void Update() {
        if (TimerIndex == 1) {
            LevelCountdown();
        }

        if (TimerIndex == 2) {
            LevelDuration();
        }

        if (TimerIndex == 3) {
            LastSeconds();
        }
    }


    private void LevelCountdown() {
        levelCountdown -= Time.deltaTime * initialCountdownMultiplier;
        CurrentTime = levelCountdown;

        if (levelCountdown <= 1.0f) {
            TimerIndex = 2;
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


    public static void RunEnds(GameObject raceWinner) {
        TimerIndex = 0;
        ShowResults = true;

        int winnerID = raceWinner.GetComponent<PlayerSheet>().playerID;
        // print("Player " + winnerID + " wins!");
        GameManager.RankingsArr.Add(winnerID);
    }

}