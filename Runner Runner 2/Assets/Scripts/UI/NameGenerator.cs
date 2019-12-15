using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Rewired;

public class NameGenerator : MonoBehaviour {

    public int playerID = -1;
    public TMP_Text randomNameText;


    // REWIRED
    private bool randomizeName = false;


    private void Start() {
        playerID = GetComponent<SelectionInterface>().InterfaceID;
        RandomizeName();
    }


    private void Update() {
        GetInput();

        if (randomizeName) {
            RandomizeName();
        }
    }


    private void GetInput() {
        randomizeName = ReInput.players.GetPlayer(playerID).GetButtonDown("Square");
    }


    private void RandomizeName() {
		int titleChance = Random.Range(0, 100);
		int adjectiveChance = Random.Range(0, 100);

		if (titleChance >= 50 && adjectiveChance >= 50) {
			titleChance = Random.Range(0, 100);
			adjectiveChance = Random.Range(0, 100);
		}

		string addTitle = "";
		string addAdjective = "";
		string addName = "";

		// 50% chance of adding a title to the name
		if (titleChance < 50) {
			int rndTitle = Random.Range(0, NameGeneratorContent.titleTexts.Length);
			addTitle = NameGeneratorContent.titleTexts[rndTitle] + " ";
		}

		// 50% chance of adding a adjective to the name
		if (adjectiveChance < 50) {
			int rndPrefix = Random.Range(0, NameGeneratorContent.adjectiveTexts.Length);
			addAdjective = NameGeneratorContent.adjectiveTexts[rndPrefix] + " ";
		}

		int rndName = Random.Range(0, NameGeneratorContent.nameTexts.Length);
		addName = NameGeneratorContent.nameTexts[rndName];

		// Display randomized name
		randomNameText.text = addTitle + addAdjective + addName;

		// Write name in SettingsHolder array
		// SettingsHolder.heroNames[charID] = randomNameText.text;
	}

}
