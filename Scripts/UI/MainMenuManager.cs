using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject StoryModePanel;
    public GameObject SkirmishModePanel;
    public GameObject OptionPanel;

    private bool StoryModePanelOn = false;
    private bool SkirmishModePanelOn = false;
    private bool OptionPanelOn = false;
    private bool MainMenuPanelOn = true;

    void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Main Menu")
        {
            MainMenuPanel.SetActive(MainMenuPanelOn);
            StoryModePanel.SetActive(StoryModePanelOn);
            SkirmishModePanel.SetActive(SkirmishModePanelOn);
            OptionPanel.SetActive(OptionPanelOn);
        }
    }

    // Brings up Story Panel
    public void ClickStoryModeButton()
    {
        if (MainMenuPanelOn) { SetPanelState(MainMenuPanel, ref MainMenuPanelOn); }
        if (SkirmishModePanelOn) { SetPanelState(SkirmishModePanel, ref SkirmishModePanelOn); }
        if (OptionPanelOn) { SetPanelState(OptionPanel, ref OptionPanelOn); }
        if (StoryModePanelOn && !MainMenuPanel) { return; }
        SetPanelState(StoryModePanel, ref StoryModePanelOn);
        return;
    }

    // Brings up Skirmish Mode Panel
    public void ClickSkirmishModeButton()
    {
        if (MainMenuPanelOn) { SetPanelState(MainMenuPanel, ref MainMenuPanelOn); }
        if (StoryModePanelOn) { SetPanelState(StoryModePanel, ref StoryModePanelOn); }
        if (OptionPanelOn) { SetPanelState(OptionPanel, ref OptionPanelOn); }
        if (SkirmishModePanelOn && !MainMenuPanel) { return; }
        SetPanelState(SkirmishModePanel, ref SkirmishModePanelOn);
        return;
    }

    // Brings up Option Panel
    public void ClickOptionsButton()
    {
        if (MainMenuPanelOn) { SetPanelState(MainMenuPanel, ref MainMenuPanelOn); }
        if (SkirmishModePanelOn) { SetPanelState(SkirmishModePanel, ref SkirmishModePanelOn); }
        if (StoryModePanelOn) { SetPanelState(StoryModePanel, ref StoryModePanelOn); }
        if (OptionPanelOn) { return; }
        SetPanelState(OptionPanel, ref OptionPanelOn);
        return;
    }

    // Closes Game
    public void ClickQuitButton()
    {
        Application.Quit();
    }

    // Starts a New Game
    public void ClickNewGameButton()
    {
        Debug.Log("New Game");
    }

    // Brings up Saved Games
    public void ClickLoadGameButton()
    {
        Debug.Log("Load Game");
    }

    // Brings up Main Menu Panel
    public void ClickBackButton()
    {
        Debug.Log(MainMenuPanelOn);
        if (StoryModePanelOn) { SetPanelState(StoryModePanel, ref StoryModePanelOn); }
        if (SkirmishModePanelOn) { SetPanelState(SkirmishModePanel, ref SkirmishModePanelOn); }
        if (OptionPanelOn) { SetPanelState(OptionPanel, ref OptionPanelOn); }
        if (MainMenuPanelOn) { return; }
        SetPanelState(MainMenuPanel, ref MainMenuPanelOn);
    }

    // Set Panel Status
    private void SetPanelState(GameObject panel, ref bool panelStatus)
    {
        panelStatus = !panelStatus;
        panel.SetActive(panelStatus);
    }
}
