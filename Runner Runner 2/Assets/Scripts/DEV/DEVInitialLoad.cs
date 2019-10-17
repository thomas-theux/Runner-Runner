using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DEVInitialLoad : MonoBehaviour {

    private void Awake() {
        // SceneManager.LoadScene("2 Couch Session");
        SceneManager.LoadScene("1 Main Menu");
    }

}
