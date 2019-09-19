using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayResults : MonoBehaviour {

    private int charID = 0;
    private bool resultsDisplayed = false;

    public GameObject ResultsUI;
    public TMP_Text rankingsText;


    private void Awake() {

    }


    private void Update() {
        if (TimeManager.ShowResults && !resultsDisplayed) {
            ShowResults();
        }
    }


    private void ShowResults() {
        resultsDisplayed = true;
        ResultsUI.SetActive(true);

        charID = this.transform.parent.GetComponent<PlayerSheet>().playerID;

        switch (GameSettings.SelectedGameMode) {
            case 0:
                if (GameManager.RankingsArr[0] == charID) {
                    rankingsText.text = "Winner!";
                } else {
                    rankingsText.text = "Loser!";
                }
                break;
        }
    }

}
