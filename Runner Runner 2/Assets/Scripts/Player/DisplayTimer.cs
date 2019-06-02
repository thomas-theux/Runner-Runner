using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTimer : MonoBehaviour {

    public TMP_Text LevelTimer;


    private void Update() {
        if (TimeManager.TimerIndex > 0) {
            DisplayTimers();
        } else {
            LevelTimer.text = "";
        }
    }


    private void DisplayTimers() {
        switch (TimeManager.TimerIndex) {
            case 1:
                LevelTimer.fontSize = 140;
                LevelTimer.color = ColorManager.KeyWhite;
                break;
            case 2:
                LevelTimer.fontSize = 80;
                LevelTimer.color = ColorManager.KeyWhite;
                break;
            case 3:
                LevelTimer.fontSize = 140;
                LevelTimer.color = ColorManager.KeyRed;
                break;
        }

        LevelTimer.text = Mathf.Floor(TimeManager.CurrentTime) + "";
    }

}
