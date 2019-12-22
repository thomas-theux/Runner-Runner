using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour {

    public GameObject CharacterSelectorGO;
    private GameObject charSelParentGO;

    public GameObject BlueishBG;

    public List<GameObject> characterSelectionUI = new List<GameObject>();

    private float rectWidthSubtract = 0;
    private float rectHeightSubtract = 0;


    private void Awake() {
        charSelParentGO = this.gameObject;
    }


    private void OnEnable() {
        BlueishBG.SetActive(false);

        MenuManager.CharacterSelectionOn = true;

		// Instantiate player UIs
        for (int i = 0; i < GameSettings.ConnectedGamepads; i++) {
            InstantiateUI(i);
        }

        for (int j = 0; j < GameSettings.ConnectedGamepads; j++) {
            ArrangeCharacterSelectorUIs(j);
        }
	}


    private void OnDisable() {
        // BlueishBG.SetActive(true);

        MenuManager.CharacterSelectionOn = false;

        for (int i = 0; i < characterSelectionUI.Count; i++) {
            Destroy(characterSelectionUI[i].gameObject);
        }

        characterSelectionUI.Clear();
    }


    public void InstantiateUI(int canvasIndex) {
        if (characterSelectionUI.Count < GameSettings.ConnectedGamepads) {
            GameObject newCharacterSelectorUI = Instantiate(CharacterSelectorGO);
            newCharacterSelectorUI.transform.SetParent(charSelParentGO.transform);

            characterSelectionUI.Add(newCharacterSelectorUI);

            // Give instantiated interfaces their proper ID
            newCharacterSelectorUI.GetComponent<SelectionInterface>().InterfaceID = canvasIndex;

            // Color the dude
            newCharacterSelectorUI.GetComponent<SelectionInterface>().DudeIMG.color = ColorManager.CharacterColors[canvasIndex];
        }
    }


    public void ArrangeCharacterSelectorUIs(int canvasIndex) {

        float newPosX = 0;
        float newPosY = 0;

        // print(canvasIndex);
        // print("hängt bei " + characterSelectionUI[canvasIndex]);

        Canvas newCanvas = characterSelectionUI[canvasIndex].transform.GetChild(0).transform.GetChild(0).GetComponent<Canvas>();
        RectTransform newCanvasRect = newCanvas.GetComponent<RectTransform>();

        newCanvas.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);

        // Transform child canvas rect
        RectTransform newSelectorRect = newCanvas.GetComponent<RectTransform>();

        // Setting the size of the selector interface
        switch (GameSettings.ConnectedGamepads) {
            case 1:
                rectWidthSubtract = 40;
                rectHeightSubtract = 100;
                break;
            case 2:
                rectWidthSubtract = 50;
                rectHeightSubtract = 100;
                break;
            case 3:
                rectWidthSubtract = 50;
                rectHeightSubtract = 110;
                break;
            case 4:
                rectWidthSubtract = 50;
                rectHeightSubtract = 110;
                break;
        }

        newSelectorRect.sizeDelta = new Vector2(
            newSelectorRect.rect.width - rectWidthSubtract,
            newSelectorRect.rect.height - rectHeightSubtract
        );

        switch(canvasIndex) {
            case 0:
                // newBackground.color = ColorManager.CharacterColors[0];      // DEF STUFF
                newPosX = -newCanvasRect.rect.width / 4;
                newPosY = newCanvasRect.rect.height / 4;

                newSelectorRect.anchorMin = new Vector2(0, 1);
                newSelectorRect.anchorMax = new Vector2(0, 1);
                newSelectorRect.pivot = new Vector2(0, 1);
                break;
            case 1:
                // newBackground.color = ColorManager.CharacterColors[1];      // DEF STUFF
                newPosX = newCanvasRect.rect.width / 4;
                newPosY = newCanvasRect.rect.height / 4;

                newSelectorRect.anchorMin = new Vector2(1, 1);
                newSelectorRect.anchorMax = new Vector2(1, 1);
                newSelectorRect.pivot = new Vector2(1, 1);
                break;
            case 2:
                // newBackground.color = ColorManager.CharacterColors[2];      // DEF STUFF
                newPosX = -newCanvasRect.rect.width / 4;
                newPosY = -newCanvasRect.rect.height / 4;

                newSelectorRect.anchorMin = new Vector2(0, 0);
                newSelectorRect.anchorMax = new Vector2(0, 0);
                newSelectorRect.pivot = new Vector2(0, 0);
                break;
            case 3:
                // newBackground.color = ColorManager.CharacterColors[3];      // DEF STUFF
                newPosX = newCanvasRect.rect.width / 4;
                newPosY = -newCanvasRect.rect.height / 4;

                newSelectorRect.anchorMin = new Vector2(1, 0);
                newSelectorRect.anchorMax = new Vector2(1, 0);
                newSelectorRect.pivot = new Vector2(1, 0);
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
            newCanvas.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            newCanvasRect.sizeDelta = new Vector2(newCanvasRect.rect.width / 2, newCanvasRect.sizeDelta.y);
        }

        if (GameSettings.ConnectedGamepads == 1) {
            newPosX = 0;
            newPosY = 0;
            newCanvas.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        newCanvasRect.localPosition = new Vector3(newPosX, newPosY, 0);
        newSelectorRect.anchoredPosition = new Vector3(0, 0, 0);
    }

}
