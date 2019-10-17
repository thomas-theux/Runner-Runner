using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachedFinish : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Character") {

            switch (GameSettings.SelectedGameMode) {

                // Sprint
                case 0:
                    SprintFinish(other);
                    break;

                // Best Time
                case 1:
                    BestTimeFinish(other);
                    break;
            }
        }
    }


    private void SprintFinish(Collider other) {
        int winnerID = other.GetComponent<PlayerSheet>().playerID;
        GameManager.RankingsArr[0] = winnerID;
        // GameManager.RankingsArr.Add(winnerID);

        CheckForBestTime(other);

        TimeManager.RundEnds();
    }


    private void BestTimeFinish(Collider other) {
        CheckForBestTime(other);

        // Trigger sorting function
        TimeManager.SortBestTimesArray();

        // Update all player ranks
        TimeManager.UpdatePlayerRanks();

        other.GetComponent<CharacterLifeHandler>().KillCharacter(true);
    }


    private void CheckForBestTime(Collider other) {
        DisplayTimer displayTimerScript = other.GetComponent<CharacterLifeHandler>().DisplayTimerScript.GetComponent<DisplayTimer>();
        PlayerSheet playerSheetScript = other.GetComponent<PlayerSheet>();

        float newTime = displayTimerScript.CurrentRunTimes;
        float bestTime = playerSheetScript.BestRunTime;

        AudioManager.instance.Play("ReachFinish");

        if (newTime < bestTime) {
            AudioManager.instance.Play("NewBest");

            // Overwrite the current best time of the player
            playerSheetScript.BestRunTime = newTime;
            displayTimerScript.BestTime.text = TimeManager.FormatBestTime(playerSheetScript.BestRunTime);

            // Set the new best time also in the best times array in the TimeManager script
            TimeManager.PlayersBestTimesArr[playerSheetScript.playerID] = playerSheetScript.BestRunTime;
        }
    }

}
