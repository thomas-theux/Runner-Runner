﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DEVLevelSelect : MonoBehaviour {

    private void Awake() {
        switch (GameSettings.SelectedGameMode) {
            case 0:
                 SceneManager.LoadScene("SprintLevel");
                break;
            case 1:
                 SceneManager.LoadScene("TestLevel");
                break;
        }
    }

}
