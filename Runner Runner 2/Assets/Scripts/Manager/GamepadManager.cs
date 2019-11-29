using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class GamepadManager : MonoBehaviour {

	public Image[] GamepadIcons;


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
		GameSettings.ConnectedGamepads = 3;

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
		switch(GameSettings.ConnectedGamepads) {
			case 0:
				// Kein Controller ist connected
				print("no controller connected");
				break;
			case 1:
				print("1 controller connected");
				break;
			case 2:
				print("2 controller connected");
				break;
			case 3:
				print("3 controller connected");
				break;
		}
	}


	void OnControllerConnected(ControllerStatusChangedEventArgs args) {
		if (GameSettings.ConnectedGamepads < GameSettings.PlayerMax) {
			GameSettings.ConnectedGamepads = ReInput.controllers.joystickCount;

			if (MenuManager.MainMenuOn) {
				UpdateGamepads();
			}

			if (MenuManager.CouchSessionMenuOn) {
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
