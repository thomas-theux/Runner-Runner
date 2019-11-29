using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour {

    public GameObject CharacterSelectorGO;
    private GameObject charSelParentGO;

    private List<GameObject> characterSelectionUI = new List<GameObject>();


    private void Awake() {
        charSelParentGO = this.gameObject;
    }


    private void OnEnable() {
		// Instantiate player UIs
        for (int i = 0; i < GameSettings.ConnectedGamepads; i++) {
            print("Spawn UI #" + i);

            GameObject newCharacterSelectorUI = Instantiate(CharacterSelectorGO);
            newCharacterSelectorUI.transform.SetParent(transform.root);
            characterSelectionUI.Add(newCharacterSelectorUI);

            // Root new char sel UI (remove from camer parent GO)
            // GameObject charSelUI = newCharacterSelectorUI.transform.GetChild(0).gameObject;
            // charSelUI.transform.SetParent(charSelParentGO.transform);

            ArrangeCharacterSelectorUIs(i);
        }
	}


    public void ArrangeCharacterSelectorUIs(int camIndex) {
        float camPosX = 0;
        float camPosY = 0;
        float camWidth = 0.5f;
        float camHeight = 0.5f;

        switch(camIndex) {
            case 0:
                camPosX = 0.0f;
                camPosY = 0.5f;
                break;
            case 1:
                camPosX = 0.5f;
                camPosY = 0.5f;
                break;
            case 2:
                camPosX = 0.0f;
                camPosY = 0.0f;
                break;
            case 3:
                camPosX = 0.5f;
                camPosY = 0.0f;
                break;
        }

        if (GameSettings.ConnectedGamepads == 2) {
            camPosY = 0.0f;
            camHeight = 1.0f;
        }

        if (GameSettings.ConnectedGamepads == 1) {
            camPosX = 0.0f;
            camPosY = 0.0f;
            camWidth = 1.0f;
            camHeight = 1.0f;
        }

        characterSelectionUI[camIndex].transform.GetChild(1).GetComponent<Camera>().rect = new Rect(camPosX, camPosY, camWidth, camHeight);
    }

}
