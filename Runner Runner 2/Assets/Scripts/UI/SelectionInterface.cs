﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

public class SelectionInterface : MonoBehaviour {

    public GameObject ReadyTagGO;
    public GameObject ReadyTextGO;

    private Image readyTagIMG;
    public Image DudeIMG;

    public int InterfaceID = 0;

    private bool characterIsReady = false;

    public float readyFloat = 0;
    private float increaseSpeed = 2.0f;
    private float decreaseSpeed = 4.0f;

    private bool inputEnabled = false;
    private bool readyButtonPressed = false;

    // REWIRED
    private bool readyButton = false;
    private bool cancelButton = false;


    private void Awake() {
        readyTagIMG = ReadyTagGO.GetComponent<Image>();

        StartCoroutine(EnableInput());
    }


    private void OnDisable() {
        inputEnabled = false;
    }


    private void Update() {
        if (inputEnabled) {
            GetInput();
        }
        SetStatus();
    }


    IEnumerator EnableInput() {
        yield return new WaitForSeconds(0.3f);
        inputEnabled = true;
    }


    private void GetInput() {
        readyButton = ReInput.players.GetPlayer(InterfaceID).GetButton("X");
        cancelButton = ReInput.players.GetPlayer(InterfaceID).GetButtonDown("Circle");
    }


    private void SetStatus() {
        if (!characterIsReady) {
            if (readyButton) {
                if (!readyButtonPressed) {
                    readyButtonPressed = true;
                    AudioManager.instance.Play("SelectUI");
                }

                if (readyFloat < 100) {
                    readyFloat += increaseSpeed;
                    readyTagIMG.fillAmount = readyFloat / 100;
                } else {
                    ActivateReadyStatus();
                }
            } else {
                if (readyButtonPressed) {
                    readyButtonPressed = false;
                }

                if (readyFloat > 0) {
                    readyFloat -= decreaseSpeed;
                    readyTagIMG.fillAmount = readyFloat / 100;
                } else if (readyFloat < 0) {
                    readyFloat = 0;
                }
            }
        } else {
            if (cancelButton) {
                AudioManager.instance.Play("CancelUI");

                characterIsReady = false;
                ReadyTextGO.SetActive(false);

                CouchSessionManager.PlayerReadyStatus--;

                if (InterfaceID == 0) {
                    MenuManager.PlayerOneReady = false;
                }
            }
        }
    }


    private void ActivateReadyStatus() {
        AudioManager.instance.Play("ActivateReadyStatus");

        characterIsReady = true;
        ReadyTextGO.SetActive(true);

        CouchSessionManager.PlayerReadyStatus++;

        if (InterfaceID == 0) {
            MenuManager.PlayerOneReady = characterIsReady;
        }
    }

}
