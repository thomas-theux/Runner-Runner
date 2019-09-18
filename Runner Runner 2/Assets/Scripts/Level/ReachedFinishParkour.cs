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

            if (newTime < bestTime) {
                playerSheetScript.BestRunTime = newTime;
                displayTimerScript.BestTime.text = FormatBestTime(playerSheetScript.BestRunTime);
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
