using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class DEVControllerVibration : MonoBehaviour {

    private float motorLevel = 1.0f; // full motor speed
    private float duration = 0.2f; // 0.1 seconds

    private Player player;

    // REWIRED
    private bool interactBtn = false;


    private void Update() {
        GetInput();
        SetVibration();
    }


    private void GetInput() {
        interactBtn = ReInput.players.GetPlayer(0).GetButtonDown("X");
    }


    // THIS SEEMS TO ONLY WORK FOR A WIRED PS4 CONTROLLER (AS OF RIGHT NOW)
    private void SetVibration() {
        if (interactBtn) {
            print("button press");

            // Set vibration for a certain duration
            foreach(Joystick j in ReInput.players.GetPlayer(0).controllers.Joysticks) {
                if(!j.supportsVibration) continue;
                if(j.vibrationMotorCount > 0) {
                    print("Vibrate");
                    j.SetVibration(0, motorLevel, duration);
                    j.SetVibration(1, motorLevel, duration);
                }
            }
        }
    }

}
