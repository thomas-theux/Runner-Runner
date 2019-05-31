using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class GamepadManager : MonoBehaviour {

    public void InitializeGamepads() {
        ReInput.ControllerConnectedEvent += OnControllerConnected;
		ReInput.ControllerDisconnectedEvent += OnControllerDisconnected;

		// connectedGamepads = ReInput.controllers.joystickCount;
		GameManager.ConnectedGamepads = 4;

        GameManager.PlayerCount = GameManager.ConnectedGamepads;
    }


	void OnControllerConnected(ControllerStatusChangedEventArgs args) {
		if (GameManager.ConnectedGamepads < SettingsManager.PlayerMax) {
			// connectedGamepads = ReInput.controllers.joystickCount;
		} else {
			print("No more controllers allowed");
		}
	}


	void OnControllerDisconnected(ControllerStatusChangedEventArgs args) {
		if (GameManager.ConnectedGamepads > 0) {
			// connectedGamepads = ReInput.controllers.joystickCount;
		} else {
			print("No more controllers to disconnect");
		}
	}

}
