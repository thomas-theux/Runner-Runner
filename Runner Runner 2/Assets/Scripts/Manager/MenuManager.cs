using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

public class MenuManager : MonoBehaviour {

    public GameObject[] AllMenus;

    // Disabled Menus:
    // 0 Career Mode
    // 1 Couch Session
    // 2 Stats
    // 3 Options
    // 4 Credits
    private List<int> disabledMenus = new List<int>{0, 2, 3};
    private List<int> menusWithNoSubMenu = new List<int>{4};

    public GameObject NavigationContainer;
    public List<GameObject> MenuItemsArr = new List<GameObject>();

    private int overallMenuIndex = 0;
    public int CurrentNavIndex = 0;
    private int mainNavIndexSaveState;

	private float maxThreshold = 0.5f;
    private bool axisYActive;

    // REWIRED
    // private bool arrowLeft = false;
    // private bool arrowRight = false;
    private bool arrowUp = false;
    private bool arrowDown = false;

    private float lsVertical = 0.0f;

    private bool interactBtn = false;
    private bool cancelBtn = false;

    public static bool MainMenuOn = false;
    public static bool CouchSessionMenuOn = false;
    public static bool CharacterSelectionOn = false;
    public static bool PlayerOneReady = false;


    private void Awake() {
        LoadNewMenu();

        MainMenuOn = true;
    }


    private void Update() {
        GetInput();
        NavigateMainMenu();
    }


    private void LoadNewMenu() {
        UpdateMenuItemsArr();
        DisplayRightMenu();

        // Some menus don't have a sub menu (like credits) and don't need to be navigatible
        if (!menusWithNoSubMenu.Contains(mainNavIndexSaveState)) {
            UpdateNavStates();
        }
    }


    private void GetInput() {
        // arrowLeft = ReInput.players.GetPlayer(0).GetButtonDown("DPad Left");
        // arrowRight = ReInput.players.GetPlayer(0).GetButtonDown("DPad Right");
        arrowUp = ReInput.players.GetPlayer(0).GetButtonDown("DPad Up");
        arrowDown = ReInput.players.GetPlayer(0).GetButtonDown("DPad Down");

        lsVertical = ReInput.players.GetPlayer(0).GetAxis("LS Vertical");

        interactBtn = ReInput.players.GetPlayer(0).GetButtonDown("X");
        cancelBtn = ReInput.players.GetPlayer(0).GetButtonDown("Circle");
    }


    private void UpdateMenuItemsArr() {
        MenuItemsArr.Clear();

        GameObject labelNavContainer = NavigationContainer.transform.GetChild(overallMenuIndex).transform.GetChild(0).transform.GetChild(0).gameObject;

        for (int i = 0; i < labelNavContainer.transform.childCount; i++) {
            MenuItemsArr.Add(labelNavContainer.transform.GetChild(i).gameObject);
        }
    }


    private void NavigateMainMenu() {
        // UI navigation with the D-Pad buttons
        // UP
        if (arrowUp) {
            AudioManager.instance.PlayRandom("NavigateUI", 1.0f, 1.0f);

            if (CurrentNavIndex > 0) {
                CurrentNavIndex--;
            } else {
                CurrentNavIndex = MenuItemsArr.Count - 1;
            }

            UpdateNavStates();
        }

        // DOWN
        if (arrowDown) {
            AudioManager.instance.PlayRandom("NavigateUI", 1.0f, 1.0f);

            if (CurrentNavIndex < MenuItemsArr.Count - 1) {
                CurrentNavIndex++;
            } else {
                CurrentNavIndex = 0;
            }

            UpdateNavStates();
        }

        ///////////////////////////////////////////////////////////////////

        // UI navigation with the analog sticks
        // UP
        if (ReInput.players.GetPlayer(0).GetAxis("LS Vertical") > maxThreshold && !axisYActive) {
            axisYActive = true;
            AudioManager.instance.PlayRandom("NavigateUI", 1.0f, 1.0f);

            if (CurrentNavIndex > 0) {
                CurrentNavIndex--;
            } else {
                CurrentNavIndex = MenuItemsArr.Count - 1;
            }

            UpdateNavStates();
        }

        // DOWN
        if (ReInput.players.GetPlayer(0).GetAxis("LS Vertical") < -maxThreshold && !axisYActive) {
            axisYActive = true;
            AudioManager.instance.PlayRandom("NavigateUI", 1.0f, 1.0f);

            if (CurrentNavIndex < MenuItemsArr.Count - 1) {
                CurrentNavIndex++;
            } else {
                CurrentNavIndex = 0;
            }

            UpdateNavStates();
        }

        ///////////////////////////////////////////////////////////////////

        // Reset Y-Axis bool
        if (ReInput.players.GetPlayer(0).GetAxis("LS Vertical") <= maxThreshold && ReInput.players.GetPlayer(0).GetAxis("LS Vertical") >= -maxThreshold) {
            axisYActive = false;
        }

        ///////////////////////////////////////////////////////////////////

        if (!PlayerOneReady) {
            if (interactBtn) {

                // Disabled Menus are not selectable
                if (!disabledMenus.Contains(CurrentNavIndex)) {
                    // Enter/open menu
                    AudioManager.instance.Play("SelectUI");

                    if (overallMenuIndex == 0) {
                        overallMenuIndex = CurrentNavIndex + 1;

                        mainNavIndexSaveState = CurrentNavIndex;

                        CurrentNavIndex = 0;
                        LoadNewMenu();
                    }
                } else {
                    // Do nothing
                    AudioManager.instance.Play("CancelUI");
                }
                
            }

            if (cancelBtn) {
                if (!CharacterSelectionOn) {
                    AudioManager.instance.Play("CancelUI");

                    if (overallMenuIndex > 0) {
                        overallMenuIndex = 0;

                        CurrentNavIndex = mainNavIndexSaveState;
                        LoadNewMenu();
                    }
                }

            }
        }
    }


    private void UpdateNavStates() {
        for (int i = 0; i < MenuItemsArr.Count; i++) {
            MenuItemsArr[i].transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().color = ColorManager.KeyWhite;

            if (overallMenuIndex > 0) {
                MenuItemsArr[i].transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().color = ColorManager.KeyWhite;
                MenuItemsArr[i].transform.GetChild(2).gameObject.SetActive(false);
            }
        }

        MenuItemsArr[CurrentNavIndex].transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().color = ColorManager.KeyYellow;

        if (overallMenuIndex > 0) {
            MenuItemsArr[CurrentNavIndex].transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().color = ColorManager.KeyYellow;
            MenuItemsArr[CurrentNavIndex].transform.GetChild(2).gameObject.SetActive(true);
        }
    }


    private void DisplayRightMenu() {
        for (int i = 0; i < AllMenus.Length; i++) {
            AllMenus[i].SetActive(false);
        }

        AllMenus[overallMenuIndex].SetActive(true);
    }


    // private void DisplaySelectorArrows() {
    //     for (int i = 0; i < MenuItemsArr.Count; i++) {
    //         MenuItemsArr[i].transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
    //     }

    //     MenuItemsArr[CurrentNavIndex].transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
    // }

}
