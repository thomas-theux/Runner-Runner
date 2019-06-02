using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTimer : MonoBehaviour {

    public TMP_Text LevelTimer;

    private void Update() {
        if (TimeManager.CurrentTimer > 0) {
            LevelTimer.text = TimeManager.CurrentTime.ToString("F0");
        }
    }

}
