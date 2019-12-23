using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class TimeManager : MonoBehaviour {

    // 0 = game didn't start yet or is already finished
    // 1 = level start countdown
    // 2 = level started
    // 3 = last seconds are counting down
    public static float CurrentTime;
    public static int TimerIndex = 0;
    public static bool LevelEnd = false;

    public static List<float> PlayersBestTimesArr = new List<float>();
    public static List<float> SortedBestTimesArr = new List<float>();

    public static bool IsShowingResults = false;

    private bool isTicking = false;

    private float levelCountdown;
    private float levelDuration;
    private float lastSeconds;

    // REWIRED
    private bool quitLevelBtn = false;
    private bool replayLevelBtn = false;
    private bool nextLevelBtn = false;


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

        LevelEnd = false;

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

        if (LevelEnd) {
            GetPlayerInput();
            LevelEndNavigation();
        }
    }


    private void LevelCountdown() {
        levelCountdown -= Time.deltaTime * GameSettings.InitialCountdownMultiplier;
        CurrentTime = levelCountdown;

        if (levelCountdown <= 1.0f) {
            TimerIndex = 2;
            AudioManager.instance.Play("LevelStart");
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
            RunEnds();
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

            // Save all ranks to rankings array
            GameManager.RankingsArr[i] = getRank;

            // Update current ranking in all player UIs
            GameManager.AllPlayers[i].GetComponent<CharacterLifeHandler>().DisplayTimerScript.PlayerRank.text = getRank + 1 + "/" + GameSettings.PlayerCount;
        }
    }


    public void RunEnds() {
        AudioManager.instance.Play("LevelEnd");

        this.GetComponent<BGSpawner>().ButtonsContainer.SetActive(true);

        LevelEnd = true;
        TimerIndex = 0;
        IsShowingResults = true;

        for (int i = 0; i < GameManager.AllPlayers.Count; i++) {
            GameManager.AllPlayers[i].GetComponent<CharacterLifeHandler>().DisplayTimerScript.gameObject.GetComponent<DisplayResults>().ShowResults();
        }

        // int winnerID = raceWinner.GetComponent<PlayerSheet>().playerID;
        // GameManager.RankingsArr.Add(winnerID);
    }


    private IEnumerator TickingSound() {
        while (isTicking) {
            AudioManager.instance.Play("TimerTicking");
            yield return new WaitForSeconds(1.0f / GameSettings.InitialCountdownMultiplier);
        }
    }


    public static string FormatBestTime(float bestTime) {
        int minutes = (int) bestTime / 60;
        int seconds = (int) bestTime - 60 * minutes;
        int milliseconds = (int) (100 * (bestTime - minutes * 60 - seconds));

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }


    private void GetPlayerInput() {
        quitLevelBtn = ReInput.players.GetPlayer(0).GetButtonDown("Circle");
        replayLevelBtn = ReInput.players.GetPlayer(0).GetButtonDown("Square");
        nextLevelBtn = ReInput.players.GetPlayer(0).GetButtonDown("X");
    }


    private void LevelEndNavigation() {
        if (quitLevelBtn) {
            QuitLevel();
        }

        if (replayLevelBtn) {
            ReplayLevel();
        }

        if (nextLevelBtn) {
            NextLevel();
        }
    }


    private void QuitLevel() {
        AudioManager.instance.Play("EndRunUI");

        ResetAllVariables();

        SceneManager.LoadScene("1 Main Menu");
    }


    private void ReplayLevel() {
        AudioManager.instance.Play("StartRunUI");

        ResetVariables();

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }


    private void NextLevel() {
        AudioManager.instance.Play("StartRunUI");

        print("next");
    }


    private void ResetVariables() {
        GameManager.AllPlayers.Clear();
        GameManager.RankingsArr.Clear();

        TimerIndex = 0;
        LevelEnd = false;
        PlayersBestTimesArr.Clear();
        SortedBestTimesArr.Clear();
        IsShowingResults = false;
        isTicking = false;
    }


    private void ResetAllVariables() {
        GameManager.AllPlayers.Clear();
        GameManager.RankingsArr.Clear();

        GameSettings.PlayerCount = 0;
        GameSettings.SelectedGameMode = 1;
        GameSettings.SelectedLevelType = 0;
        GameSettings.SelectedLevelSize = 1;

        CouchSessionManager.PlayerReadyStatus = 0;

        TimerIndex = 0;
        LevelEnd = false;
        PlayersBestTimesArr.Clear();
        SortedBestTimesArr.Clear();
        IsShowingResults = false;
        isTicking = false;

        MenuManager.MainMenuOn = false;
        MenuManager.CouchSessionMenuOn = false;
        MenuManager.CharacterSelectionOn = false;
        MenuManager.PlayerOneReady = false;
    }

}