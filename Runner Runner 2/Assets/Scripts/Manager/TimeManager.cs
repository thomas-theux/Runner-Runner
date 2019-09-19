using System.Collections;
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

    private float levelCountdown;
    private float levelDuration;
    private float lastSeconds;


    public void StartLevelTimer() {
        // Add the initial best time for each player
        for (int i = 0; i < GameSettings.PlayerCount; i++) {
            float initialBestRunTime = GameManager.AllPlayers[i].GetComponent<PlayerSheet>().BestRunTime;
            PlayersBestTimesArr.Add(initialBestRunTime);
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
        levelCountdown -= Time.deltaTime * GameSettings.InitialCountdownMultiplier;
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


    public static void SortBestTimesArray() {
        SortedBestTimesArr = PlayersBestTimesArr;
        SortedBestTimesArr.Sort();
    }


    public static void UpdatePlayerRanks() {
        for (int i = 0; i < PlayersBestTimesArr.Count; i++) {
            int getRank = SortedBestTimesArr.IndexOf(PlayersBestTimesArr[i]);
            GameManager.AllPlayers[i].GetComponent<CharacterLifeHandler>().DisplayTimerScript.PlayerRank.text = getRank + 1 + "/" + GameSettings.PlayerCount;
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