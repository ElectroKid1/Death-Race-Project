﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    GameManager o_gameManager;

    // Panel ref for Single player 
    public GameObject o_PanelMainMenu;
    public GameObject o_PanelModTrackSelectionMenu;
    public GameObject o_PanelModTrackSelectMP;
    public GameObject o_PanelCarSelectionMenu;
    public GameObject o_PanelLeaderboardMenu;
    public GameObject o_PanelOptionsMenu;

    public RawImage o_RawImageTrackSelected;
    public RawImage o_RawImageModeSelected;
    public RawImage o_RawImageCarSelected;

    

    // textboxes below rawImage component describing the name of the value selected
    // NOT WORKING
    public TMP_Text o_TrackSelectedTextboxSP , o_ModeSelectedTextboxSP , o_CarSelectedTextboxSP;

    public Texture[] o_trackImages;
    public Texture[] o_carImages;

    // --- This are for the Options Menu ----------
    public GameObject o_PanelControls;
    public GameObject o_PanelHelp;

    public RawImage o_RawImageControls;
    public Texture[] o_controlMenuImages;
    public int o_currentControlImageIndex = 0;

    [SerializeField] Button o_prevControlBtn;
    [SerializeField] Button o_nextControlBtn;
    [SerializeField] Button o_ShowControlsBtn;
    [SerializeField] Button o_ShowHelpBtn;

    Color showingColor = new Color(0.992156f,0.733333f,0f);
    // ---------------------------------------------

    private int o_currentTrackTextureIndex = 0;
    private int o_currentCarTextureIndex = 0;

    void Start() {
        o_gameManager = FindObjectOfType<GameManager>();

    }

    // --------------- Main Menu SINGLE PLAYER-----------------
    public void o_SetSinglePlayerGotoTrackSelection()
    {
        o_gameManager.o_gameMode = "Singleplayer";
        o_gameManager.o_totalPlayerCount = 1;

        // Turn off Main menu panel and enable track and mode selection panel.
        o_PanelMainMenu.SetActive(false);
        o_PanelModTrackSelectionMenu.SetActive(true);
    }

    public void o_SetMultiPlayerGotoTrackSelection() {
        o_gameManager.o_gameMode = "Multiplayer";
        o_gameManager.o_totalPlayerCount = 2;       // Taking 2 players by DEFAULT
        o_gameManager.o_carsSelectedMP = new string[o_gameManager.o_totalPlayerCount]; // Initializing the cars array to DEFAULT

        // Turn off Main menu panel and enable track and mode selection panel for MP.
        o_PanelMainMenu.SetActive(false);
        o_PanelModTrackSelectMP.SetActive(true);

    }

    public void o_ShowLearderboard() {
        o_PanelMainMenu.SetActive(false);
        o_PanelLeaderboardMenu.SetActive(true);


    }

    public void o_LoadOptionsPanel() {
        // Here we load options panel and hide main panel
        o_PanelMainMenu.SetActive(false);
        o_PanelOptionsMenu.SetActive(true);
    }


    public void o_ExitGame()
    {
        // Here we Exit the game.
        Application.Quit();
    }

    // -------------------------Main menu over END------------------------------

    // ------------------------ Mode and Track Slection menu START-----------------
    public void o_GotoMainMenu()
    {

        o_PanelModTrackSelectionMenu.SetActive(false);
        o_PanelMainMenu.SetActive(true);
        
    }

    public void o_GotoCarSelectionMenu()
    {
        // save the current mode and track in gamemanagerObj
        // It stores the name of the texture assigned to the rawImage.
        o_gameManager.o_trackSelected = o_RawImageTrackSelected.texture.name;
        // KEEP THE NAME OF SCENE AND TRACK IMAGE SAME.

        o_PanelModTrackSelectionMenu.SetActive(false);
        o_PanelCarSelectionMenu.SetActive(true);
    }

    public void o_ShowNextTrack()
    {

        if (o_currentTrackTextureIndex < o_trackImages.Length - 1) {
            o_RawImageTrackSelected.texture = o_trackImages[o_currentTrackTextureIndex + 1];
            o_TrackSelectedTextboxSP.text = o_trackImages[o_currentTrackTextureIndex + 1].name;
            o_currentTrackTextureIndex ++;
        }
        else
        {
            o_currentTrackTextureIndex = 0;
            o_RawImageTrackSelected.texture = o_trackImages[o_currentTrackTextureIndex];
            o_TrackSelectedTextboxSP.text = o_trackImages[o_currentTrackTextureIndex].name;
        }
    }

    public void o_ShowPrevTrack()
    {
        if (o_currentTrackTextureIndex > 0)
        {
            o_RawImageTrackSelected.texture = o_trackImages[o_currentTrackTextureIndex - 1];
            o_TrackSelectedTextboxSP.text = o_trackImages[o_currentTrackTextureIndex - 1].name;
            o_currentTrackTextureIndex--;
        }
        else
        {
            o_currentTrackTextureIndex = o_trackImages.Length - 1;
            o_RawImageTrackSelected.texture = o_trackImages[o_currentTrackTextureIndex];
            o_TrackSelectedTextboxSP.text = o_trackImages[o_currentTrackTextureIndex].name;
        }


    }
    // ------------------------ Mode and Track Slection menu END-----------------

    // ------------------------- Car Selection Menu START ------------------------------

    public void o_GotoTrackSelection() {


        o_PanelCarSelectionMenu.SetActive(false);
        o_PanelModTrackSelectionMenu.SetActive(true);
    }

    public void o_ShowNextCar()
    {
        if (o_currentCarTextureIndex < o_carImages.Length - 1)
        {
            o_currentCarTextureIndex++;
            o_RawImageCarSelected.texture = o_carImages[o_currentCarTextureIndex];
            o_CarSelectedTextboxSP.text = o_carImages[o_currentCarTextureIndex].name;
        }
        else
        {
            o_currentCarTextureIndex = 0;
            o_RawImageCarSelected.texture = o_carImages[o_currentCarTextureIndex];
            o_CarSelectedTextboxSP.text = o_carImages[o_currentCarTextureIndex].name;

        }
    }
    public void o_ShowPrevCar()
    {
        if (o_currentCarTextureIndex > 0)
        {
            o_currentCarTextureIndex--;
            o_RawImageCarSelected.texture = o_carImages[o_currentCarTextureIndex];
            o_CarSelectedTextboxSP.text = o_carImages[o_currentCarTextureIndex].name;

        }
        else
        {
            o_currentCarTextureIndex = o_carImages.Length - 1;
            o_RawImageCarSelected.texture = o_carImages[o_currentCarTextureIndex];
            o_CarSelectedTextboxSP.text = o_carImages[o_currentCarTextureIndex].name;

        }
    }

    public void o_StartRace() {
        // set car in the gamemanager.
        o_gameManager.o_carSelected = o_RawImageCarSelected.texture.name;

        // check which scene needs to be loaded.


        // Load the scene corresponding to the selected track in the gamemanager.
        o_DecideSceneToLoad();
    }

    private void o_DecideSceneToLoad() {
        string track_selected = o_gameManager.o_trackSelected;


        SceneManager.LoadScene(track_selected);


    }


    // ------------------------- Car Selection Menu END------------------------------

    // -------------------------- LEADERBOARD PANEL ---------------------------------

    public void o_GotoMainMenuFromLeaderboard() {
        o_PanelLeaderboardMenu.SetActive(false);
        o_PanelMainMenu.SetActive(true);
    }

    // -----------------------------LEADERBOARD CODE ENDS HERE ---------------------

    // ----------------------------- OPTIONS MENU CODE HERE ------------------------

    public void o_ExitOptionsMenu() {
        o_PanelOptionsMenu.SetActive(false);
        o_PanelMainMenu.SetActive(true);
    }

    public void o_ShowControlsPanel() 
    {
        //o_ShowHelpBtn.GetComponent<Button>().colors.normalColor = Color.white;
        o_ShowHelpBtn.GetComponent<Image>().color = Color.white;
        try
        {
            o_ShowControlsBtn.GetComponent<Image>().color = showingColor;
        }
        catch (System.Exception )
        {
            throw;
            
        }
        

        o_PanelHelp.SetActive(false);
        o_PanelControls.SetActive(true);
    }

    public void o_ShowHelpPanel() 
    {
        o_ShowHelpBtn.GetComponent<Image>().color = showingColor;
        o_ShowControlsBtn.GetComponent<Image>().color = Color.white;

        o_PanelControls.SetActive(false);
        o_PanelHelp.SetActive(true);
    }

    public void o_NextControl() {
        if (o_currentControlImageIndex < o_controlMenuImages.Length - 1)
        {
            o_currentControlImageIndex++;
            o_RawImageControls.texture = o_controlMenuImages[o_currentControlImageIndex];

            if (o_currentControlImageIndex == o_controlMenuImages.Length - 1) {
                // Diable the Next control button.
                o_nextControlBtn.interactable = false;
            }

            // Enable the prv control button if it is Disabled.
            o_prevControlBtn.interactable = true;
        }
    }

    public void o_PrevControl() {
        if (o_currentControlImageIndex > 0) 
        {
            o_currentControlImageIndex--;
            o_RawImageControls.texture = o_controlMenuImages[o_currentControlImageIndex];

            if (o_currentControlImageIndex == 0)
            {
                // Diable the Prev control button.
                o_prevControlBtn.interactable = false;
            }

            // Enable the prv control button if it is Disabled.
            o_nextControlBtn.interactable = true;
        }
    }

    // -----------------------------OPTIONS MENUE CODE END -------------------------
}
