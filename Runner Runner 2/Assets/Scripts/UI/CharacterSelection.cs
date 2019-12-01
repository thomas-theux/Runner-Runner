﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            GameObject newCharacterSelectorUI = Instantiate(CharacterSelectorGO);
            characterSelectionUI.Add(newCharacterSelectorUI);

            Canvas newSelectorCanvas = characterSelectionUI[i].transform.GetChild(0).GetComponent<Canvas>();

            // Root new char sel UI (remove from camera parent GO)
            // GameObject charSelUI = newCharacterSelectorUI.transform.GetChild(0).gameObject;
            // charSelUI.transform.SetParent(charSelParentGO.transform);

            ArrangeCharacterSelectorUIs(i, newSelectorCanvas);
        }
	}


    public void ArrangeCharacterSelectorUIs(int canvasIndex, Canvas newCanvas) {
        float newPosX = 0;
        float newPosY = 0;

        RectTransform newCanvasRect = newCanvas.GetComponent<RectTransform>();

        // DEF STUFF – This changes the color of the test backgrounds
        Image newBackground = newCanvas.transform.GetChild(0).GetComponent<Image>();

        switch(canvasIndex) {
            case 0:
                newBackground.color = ColorManager.KeyBlack20;
                newPosX = -newCanvasRect.rect.width / 4;
                newPosY = newCanvasRect.rect.height / 4;
                break;
            case 1:
                newBackground.color = ColorManager.KeyBlack20;
                newPosX = newCanvasRect.rect.width / 4;
                newPosY = newCanvasRect.rect.height / 4;
                break;
            case 2:
                newBackground.color = ColorManager.KeyBlack20;
                newPosX = -newCanvasRect.rect.width / 4;
                newPosY = -newCanvasRect.rect.height / 4;
                break;
            case 3:
                newBackground.color = ColorManager.KeyBlack20;
                newPosX = newCanvasRect.rect.width / 4;
                newPosY = -newCanvasRect.rect.height / 4;
                break;
        }

        // Center camera of player 3 when only 3 players are in the game
        if (GameSettings.ConnectedGamepads == 3) {
            if (canvasIndex == 2) {
                newPosX = 0;
            }
        }

        if (GameSettings.ConnectedGamepads == 2) {
            newPosY = 0;
            newCanvas.transform.localScale = new Vector3(0.5f, 1.0f, 1.0f);
        }

        if (GameSettings.ConnectedGamepads == 1) {
            newPosX = 0;
            newPosY = 0;
            newCanvas.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        newCanvasRect.localPosition = new Vector3(newPosX, newPosY, 0);
    }

}
