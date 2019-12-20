using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DEVInitialLoad : MonoBehaviour {

    public int PlayerCount = 3;


    private void Awake() {
        // Set the player count manually via the editor
        GameSettings.ConnectedGamepads = PlayerCount;

        Cursor.visible = false;

        SceneManager.LoadScene("1 Main Menu");
    }

}
