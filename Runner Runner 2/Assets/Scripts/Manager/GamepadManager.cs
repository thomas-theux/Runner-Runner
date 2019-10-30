using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class GamepadManager : MonoBehaviour {

	public Image[] GamepadIcons;
	public bool MainMenuOn = false;


	private void OnEnable() {
		if (MainMenuOn) {
			InitializeGamepads();
			UpdateGamepads();
		}
	}


	public void InitializeGamepads() {
		ReInput.ControllerConnectedEvent += OnControllerConnected;
		ReInput.ControllerDisconnectedEvent += OnControllerDisconnected;

		// DEV STUFF
		// GameSettings.ConnectedGamepads = ReInput.controllers.joystickCount;
		GameSettings.ConnectedGamepads = 2;

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


	void OnControllerConnected(ControllerStatusChangedEventArgs args) {
		if (GameSettings.ConnectedGamepads < GameSettings.PlayerMax) {
			GameSettings.ConnectedGamepads = ReInput.controllers.joystickCount;

			if (MainMenuOn) {
				UpdateGamepads();
			}

		} else {
			print("No more controllers allowed");
		}
	}


	void OnControllerDisconnected(ControllerStatusChangedEventArgs args) {
		if (GameSettings.ConnectedGamepads > 0) {
			GameSettings.ConnectedGamepads = ReInput.controllers.joystickCount;

			if (MainMenuOn) {
				UpdateGamepads();
			} else {
				// Throw gamepad disconnect dialog
				print("Player " + (args.controllerId + 1) + " got disconnected!");
			}

		} else {
			print("No more controllers to disconnect");
		}
	}

}
