using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class GamepadManager : MonoBehaviour {

	public CharacterSelection CharacterSelectionScript;

	public Image[] GamepadIcons;

	// Define the gamepad type
	// 0 = PS4; 1 = XBOX; 2 = anything else
	public static int GamepadType = 2;


	private void OnEnable() {
		if (MenuManager.MainMenuOn) {
			InitializeGamepads();
			UpdateGamepads();
		}
	}


	public void InitializeGamepads() {
		ReInput.ControllerConnectedEvent += OnControllerConnected;
		ReInput.ControllerDisconnectedEvent += OnControllerDisconnected;

		// DEV STUFF
		// GameSettings.ConnectedGamepads = ReInput.controllers.joystickCount;
		// GameSettings.ConnectedGamepads = 3;

		GameSettings.PlayerCount = GameSettings.ConnectedGamepads;
	}


	private void UpdateGamepads() {
		for (int i = 0; i < 4; i++) {
			GamepadIcons[i].color = ColorManager.KeyBlack20;
		}

		for (int j = 0; j < GameSettings.ConnectedGamepads; j++) {
			GamepadIcons[j].color = ColorManager.KeyBlack;
		}
	}


	private void AddPlayerUI() {

		CharacterSelectionScript.InstantiateUI(GameSettings.ConnectedGamepads - 1);

		for (int i = 0; i < GameSettings.ConnectedGamepads; i++) {
            CharacterSelectionScript.ArrangeCharacterSelectorUIs(i);
        }
	}


	void OnControllerConnected(ControllerStatusChangedEventArgs args) {
		if (GameSettings.ConnectedGamepads < GameSettings.PlayerMax) {
			GameSettings.ConnectedGamepads = ReInput.controllers.joystickCount;

			// Save the proper gamepad type into an int
			if (args.controllerId == 0) {
				switch (args.name) {
					case "Sony DualShock 4":
						GamepadType = 0;
						break;
					case "Xbox One Controller":
					case "Xbox 360 Controller":
						GamepadType = 1;
						break;
				}
			}

			if (MenuManager.MainMenuOn) {
				UpdateGamepads();
			}

			if (MenuManager.CharacterSelectionOn) {
				AddPlayerUI();
			}

		} else {
			print("No more controllers allowed");
		}
	}


	void OnControllerDisconnected(ControllerStatusChangedEventArgs args) {
		if (GameSettings.ConnectedGamepads > 0) {
			GameSettings.ConnectedGamepads = ReInput.controllers.joystickCount;

			if (MenuManager.MainMenuOn) {
				UpdateGamepads();
			} else {
				// Throw "gamepad disconnected" dialog
				print("Player " + (args.controllerId + 1) + " got disconnected!");
			}

			if (MenuManager.CouchSessionMenuOn) {
				// RemovePlayerUIs();
			}

		} else {
			print("No more controllers to disconnect");
		}
	}

}
