using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachedFinishParkour : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Character") {
            DisplayTimer displayTimerScript = other.GetComponent<CharacterLifeHandler>().DisplayTimerScript.GetComponent<DisplayTimer>();
            PlayerSheet playerSheetScript = other.GetComponent<PlayerSheet>();

            float newTime = displayTimerScript.CurrentRunTimes;
            float bestTime = playerSheetScript.BestRunTime;

            FindObjectOfType<AudioManager>().Play("ReachFinish");

            if (newTime < bestTime) {
                FindObjectOfType<AudioManager>().Play("NewBest");

                // Overwrite the current best time of the player
                playerSheetScript.BestRunTime = newTime;
                displayTimerScript.BestTime.text = FormatBestTime(playerSheetScript.BestRunTime);

                // Set the new best time also in the best times array in the TimeManager script
                TimeManager.PlayersBestTimesArr[playerSheetScript.playerID] = playerSheetScript.BestRunTime;

                // Trigger sorting function
                TimeManager.SortBestTimesArray();

                // Update all player ranks
                TimeManager.UpdatePlayerRanks();
            }

            other.GetComponent<CharacterLifeHandler>().KillCharacter(true);
        }
    }


    private string FormatBestTime(float bestTime) {
        int minutes = (int) bestTime / 60;
        int seconds = (int) bestTime - 60 * minutes;
        int milliseconds = (int) (100 * (bestTime - minutes * 60 - seconds));

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

}
