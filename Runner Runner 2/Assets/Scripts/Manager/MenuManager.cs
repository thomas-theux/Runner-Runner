using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

public class MenuManager : MonoBehaviour {

    public GameObject[] AllMenus;

    public GameObject NavigationContainer;
    public List<GameObject> MenuItemsArr = new List<GameObject>();

    private int overallMenuIndex = 0;
    private int currentNavIndex = 0;

    // REWIRED
    private bool arrowLeft = false;
    private bool arrowRight = false;
    private bool arrowUp = false;
    private bool arrowDown = false;

    private bool interactBtn = false;
    private bool cancelBtn = false;


    private void Awake() {
        for (int i = 0; i < NavigationContainer.transform.childCount; i++) {
            MenuItemsArr.Add(NavigationContainer.transform.GetChild(i).gameObject);
        }

        DisplayRightMenu();
        UpdateNavStates();
    }


    private void Update() {
        GetInput();

        NavigateMainMenu();
    }


    private void GetInput() {
        arrowLeft = ReInput.players.GetPlayer(0).GetButtonDown("DPad Left");
        arrowRight = ReInput.players.GetPlayer(0).GetButtonDown("DPad Right");
        arrowUp = ReInput.players.GetPlayer(0).GetButtonDown("DPad Up");
        arrowDown = ReInput.players.GetPlayer(0).GetButtonDown("DPad Down");

        interactBtn = ReInput.players.GetPlayer(0).GetButtonDown("X");
        cancelBtn = ReInput.players.GetPlayer(0).GetButtonDown("Circle");
    }


    private void NavigateMainMenu() {
        if (arrowUp) {
            if (currentNavIndex > 0) {
                currentNavIndex--;
            } else {
                currentNavIndex = MenuItemsArr.Count - 1;
            }

            UpdateNavStates();
        }

        if (arrowDown) {
            if (currentNavIndex < MenuItemsArr.Count - 1) {
                currentNavIndex++;
            } else {
                currentNavIndex = 0;
            }

            UpdateNavStates();
        }

        if (interactBtn) {
            if (overallMenuIndex == 0) {
                overallMenuIndex = currentNavIndex + 1;
                DisplayRightMenu();
            }
        }

        if (cancelBtn) {
            if (overallMenuIndex > 0) {
                overallMenuIndex = 0;
                DisplayRightMenu();
            }
        }
    }


    private void UpdateNavStates() {
        for (int i = 0; i < MenuItemsArr.Count; i++) {
            MenuItemsArr[i].transform.GetChild(0).GetComponent<TMP_Text>().color = ColorManager.KeyWhite;
        }

        MenuItemsArr[currentNavIndex].transform.GetChild(0).GetComponent<TMP_Text>().color = ColorManager.KeyYellow;
    }


    private void DisplayRightMenu() {
        for (int i = 0; i < AllMenus.Length; i++) {
            AllMenus[i].SetActive(false);
        }

        AllMenus[overallMenuIndex].SetActive(true);
    }

}
