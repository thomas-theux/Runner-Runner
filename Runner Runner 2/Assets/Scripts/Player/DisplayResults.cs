using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayResults : MonoBehaviour {

    private PlayerSheet playerSheetScript;

    private int charID = 0;

    public GameObject ResultsUI;
    public GameObject RankingBox;

    public TMP_Text rankingsText;
    public TMP_Text bestTimeText;


    private void Awake() {
        playerSheetScript = transform.parent.GetComponent<PlayerSheet>();
    }


    public void ShowResults() {
        RandomRotateRankings();
        ResultsUI.SetActive(true);

        charID = this.transform.parent.GetComponent<PlayerSheet>().playerID;

        // Check if there is any rank at all – it can happen that there is no ranking at all because no player finished the run
        if (GameManager.RankingsArr[0] < GameManager.AllPlayers.Count + 1) {
            switch (GameSettings.SelectedGameMode) {
                case 0:
                    ShowSprintResults();
                    break;
                case 1:
                    ShowBestTimeResults();
                    break;
            }
        } else {
            CheckRankings();
        }
    }


    private void ShowSprintResults() {
        rankingsText.fontSize = FontManager.FontH2;

        if (GameManager.RankingsArr[0] == charID) {
            rankingsText.text = "Winner!";
            rankingsText.color = ColorManager.KeyYellow;
            bestTimeText.color = ColorManager.KeyYellow;
        } else {
            rankingsText.text = "Loser!";
        }

        ShowBestTime();
    }


    private void ShowBestTimeResults() {
        rankingsText.fontSize = FontManager.FontH1;

        // int getRank = TimeManager.SortedBestTimesArr.IndexOf(TimeManager.PlayersBestTimesArr[charID]);
        int getRank = GameManager.RankingsArr[charID] + 1;
        string rankAddition = "";

        switch (getRank) {
            case 1:
                rankAddition = "st";
                rankingsText.color = ColorManager.KeyYellow;
                bestTimeText.color = ColorManager.KeyYellow;
                break;
            case 2:
                rankAddition = "nd";
                break;
            case 3:
                rankAddition = "rd";
                break;
            case 4:
                rankAddition = "th";
                break;
        }

        rankingsText.text = getRank + rankAddition;

        ShowBestTime();
    }


    private void ShowBestTime() {
        if (playerSheetScript.BestRunTime < 99999) {
            bestTimeText.text = TimeManager.FormatBestTime(playerSheetScript.BestRunTime);
        } else {
            bestTimeText.text = "DNF";
        }
    }


    private void RandomRotateRankings() {
        // Randomize ranking box rotation
        int rndAngle = Random.Range(-4, 4);
        RankingBox.transform.Rotate(new Vector3(0, 0, rndAngle));
    }


    private void CheckRankings() {
        rankingsText.fontSize = FontManager.FontH3;
        rankingsText.text = "Y'all suck!";
        bestTimeText.text = "No-one finished..";
    }

}