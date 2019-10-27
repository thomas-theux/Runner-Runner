using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Rewired;

public class CouchSessionManager : MonoBehaviour {

    public MenuManager MenuManagerScript;

    private List<int> optionsIndexes = new List<int>();
    private int[] maxOptionsIndexes = {2, 2, 0, 4};

	private float maxThreshold = 0.5f;
    private bool axisXActive;

    private List<List<string>> couchSessionNavTexts = new List<List<string>>();
    public TMP_Text LevelTitle;

    public TMP_Text[] SelectorContent;
    public TMP_Text GameModeDescription;

    public Image MapPreview;
    public Sprite[] MapPreviewImages;

    private List<string> gameModeTexts = new List<string>{
        "Sprint",
        "Best Time"
    };

    private List<string> levelTypeTexts = new List<string>{
        "Platforms",
        "Parcours"
    };

    private List<string> levelTimeTexts = new List<string>{
        "60 Seconds",
        "180 Seconds",
        "300 Seconds",
        "600 Seconds"
    };

    private List<int> levelTimeInts = new List<int>{
        60,
        180,
        300,
        600,
    };

    private List<string> levelLengthTexts = new List<string>{
        "Short",
        "Medium",
        "Long",
        "Insane"
    };

    private List<int> levelLengthInts = new List<int>{
        0,
        1,
        2,
        3
    };

    private List<string> levelSelectTexts = new List<string>{
        "Left And Right",
        "Pillars"
    };

    private List<string> levelTitleTexts = new List<string>{
        "Level Length",
        "Select Level"
    };

    private List<string> gameModeDescriptionTexts = new List<string>{
        "Sprint from A to B and be the first to win the run.",
        "Run as many times as you want in the time limit to get the best time."
    };

    // REWIRED
    private bool arrowLeft = false;
    private bool arrowRight = false;

    private float lsHorizontal = 0.0f;

    private bool startButton = false;


    private void Awake() {
        couchSessionNavTexts.Add(gameModeTexts);
        couchSessionNavTexts.Add(levelTypeTexts);
        couchSessionNavTexts.Add(levelLengthTexts);
        couchSessionNavTexts.Add(levelTimeTexts);

        for (int i = 0; i < MenuManagerScript.MenuItemsArr.Count; i++) {
            optionsIndexes.Add(0);
        }

        GameModeDescription.text = gameModeDescriptionTexts[optionsIndexes[0]];
    }


    private void OnEnable() {
        // Reset ALL indexes
        for (int i = 0; i < optionsIndexes.Count; i++) {
            optionsIndexes[i] = 0;
        }

        DisplayProperSelectorTitle();
        DisplaySelectorNavTexts();
        DisplayMapImage();
    }


    private void Update() {
        GetInput();
        NavigateCouchSessionNav();
    }


    private void GetInput() {
        arrowLeft = ReInput.players.GetPlayer(0).GetButtonDown("DPad Left");
        arrowRight = ReInput.players.GetPlayer(0).GetButtonDown("DPad Right");

        lsHorizontal = ReInput.players.GetPlayer(0).GetAxis("LS Horizontal");

        startButton = ReInput.players.GetPlayer(0).GetButtonDown("Options");
    }


    private void NavigateCouchSessionNav() {
        // UI navigation with the D-Pad buttons
        // RIGHT
        if (arrowRight) {
            AudioManager.instance.PlayRandom("NavigateUI", 1.1f, 1.1f);

            if (optionsIndexes[MenuManagerScript.CurrentNavIndex] < maxOptionsIndexes[MenuManagerScript.CurrentNavIndex] - 1) {
                optionsIndexes[MenuManagerScript.CurrentNavIndex]++;
            } else {
                optionsIndexes[MenuManagerScript.CurrentNavIndex] = 0;
            }

            UpdateLevelTexts();
            DisplayProperSelectorTitle();
            DisplaySelectorNavTexts();
            DisplayMapImage();
        }

        // LEFT
        if (arrowLeft) {
            AudioManager.instance.PlayRandom("NavigateUI", 0.9f, 0.9f);

            if (optionsIndexes[MenuManagerScript.CurrentNavIndex] > 0) {
                optionsIndexes[MenuManagerScript.CurrentNavIndex]--;
            } else {
                optionsIndexes[MenuManagerScript.CurrentNavIndex] = maxOptionsIndexes[MenuManagerScript.CurrentNavIndex] - 1;
            }

            UpdateLevelTexts();
            DisplayProperSelectorTitle();
            DisplaySelectorNavTexts();
            DisplayMapImage();
        }

        ///////////////////////////////////////////////////////////////////

        // UI navigation with the analog sticks
        // RIGHT
        if (ReInput.players.GetPlayer(0).GetAxis("LS Horizontal") > maxThreshold && !axisXActive) {
            axisXActive = true;

            AudioManager.instance.PlayRandom("NavigateUI", 1.1f, 1.1f);

            if (optionsIndexes[MenuManagerScript.CurrentNavIndex] < maxOptionsIndexes[MenuManagerScript.CurrentNavIndex] - 1) {
                optionsIndexes[MenuManagerScript.CurrentNavIndex]++;
            } else {
                optionsIndexes[MenuManagerScript.CurrentNavIndex] = 0;
            }

            UpdateLevelTexts();
            DisplayProperSelectorTitle();
            DisplaySelectorNavTexts();
            DisplayMapImage();
        }

        // LEFT
        if (ReInput.players.GetPlayer(0).GetAxis("LS Horizontal") < -maxThreshold && !axisXActive) {
            axisXActive = true;

            AudioManager.instance.PlayRandom("NavigateUI", 0.9f, 0.9f);

            if (optionsIndexes[MenuManagerScript.CurrentNavIndex] > 0) {
                optionsIndexes[MenuManagerScript.CurrentNavIndex]--;
            } else {
                optionsIndexes[MenuManagerScript.CurrentNavIndex] = maxOptionsIndexes[MenuManagerScript.CurrentNavIndex] - 1;
            }

            UpdateLevelTexts();
            DisplayProperSelectorTitle();
            DisplaySelectorNavTexts();
            DisplayMapImage();
        }

        ///////////////////////////////////////////////////////////////////

        // Reset Y-Axis bool
        if (ReInput.players.GetPlayer(0).GetAxis("LS Horizontal") <= maxThreshold && ReInput.players.GetPlayer(0).GetAxis("LS Horizontal") >= -maxThreshold) {
            axisXActive = false;
        }

        ///////////////////////////////////////////////////////////////////

        if (startButton) {
            AudioManager.instance.Play("StartRunUI");

            GameSettings.SelectedGameMode = optionsIndexes[0];
            GameSettings.SelectedLevelType = optionsIndexes[1];

            string selectedScene = "";

            if (optionsIndexes[1] == 0) {
                GameSettings.SelectedLevelSize = levelLengthInts[optionsIndexes[2]];
                selectedScene = "Platforms";
            } else {
                selectedScene = levelSelectTexts[optionsIndexes[2]];
            }

            GameSettings.LevelDuration = levelTimeInts[optionsIndexes[3]];

            SceneManager.LoadScene(selectedScene);
        }
    }


    private void DisplayProperSelectorTitle() {
        switch (optionsIndexes[1]) {
            case 0:
                maxOptionsIndexes[2] = 4;
                couchSessionNavTexts[2] = levelLengthTexts;
                break;
            case 1:
                maxOptionsIndexes[2] = 2;
                couchSessionNavTexts[2] = levelSelectTexts;
                break;
        }

        LevelTitle.text = levelTitleTexts[optionsIndexes[0]];
    }


    private void DisplaySelectorNavTexts() {
        for (int i = 0; i < optionsIndexes.Count; i++) {
            SelectorContent[i].text = couchSessionNavTexts[i][optionsIndexes[i]];
            // MenuManagerScript.NavigationContainer.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = couchSessionNavTexts[i][optionsIndexes[i]];
        }
    }


    private void DisplayMapImage() {
        if (optionsIndexes[1] == 0) {
            MapPreview.sprite = MapPreviewImages[0];
        } else {
            // MapPreview.sprite = MapPreviewImages[1];
            int fixedIndex = optionsIndexes[2] + 1;
            MapPreview.sprite = MapPreviewImages[fixedIndex];
        }
    }


    private void UpdateLevelTexts() {
        if (MenuManagerScript.CurrentNavIndex == 0) {
            GameModeDescription.text = gameModeDescriptionTexts[optionsIndexes[0]];
        }

        if (MenuManagerScript.CurrentNavIndex == 1) {
            optionsIndexes[2] = 0;
        }
    }

}
