using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Text turnText;
    public Button turnButton;
    public Text playerInitialRollText;
    public Text enemyInitialRollText;
    public Text[] rollTextBoxes = new Text[3];
    public DiceAndBoxMover diceAndBox;
    public GameObject cam;
    public bool buttonPressed;

    private void Start()
    {
        turnText.gameObject.SetActive(false);
        turnButton.gameObject.SetActive(false);
        playerInitialRollText.gameObject.SetActive(false);
        enemyInitialRollText.gameObject.SetActive(false);
        for (int i = 0; i < rollTextBoxes.Length; i++)
        {
            rollTextBoxes[i].gameObject.SetActive(false);
        }
        buttonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    internal void DisplayRollCommand(string player)
    {
        if (GameManager.Instance.turnNumber == 0)
        {
            turnText.text = player + ", Roll to see who goes first.";
            DisplayCorrectUIForCurrentPlayer(player);
        }
        else
        {
            turnText.text = player + ", Roll to move";
            DisplayCorrectUIForCurrentPlayer(player);
        }

    }

    private void DisplayCorrectUIForCurrentPlayer(string player)
    {
        if (player != "Computer")
        {
            turnText.gameObject.SetActive(true);
            turnButton.gameObject.SetActive(true);
        }
        else
        {
            turnText.gameObject.SetActive(true);
            StartCoroutine("WaitForTimePass");
        }
    }

    private IEnumerator WaitForTimePass()
    {
        yield return new WaitForSeconds(2.5f);
        BringOutDiceAndBox();
    }

    public void BringOutDiceAndBox()
    {
        //zoom out camera to max;
        cam.GetComponent<CameraManager>().FocusCameraToCenter();
        cam.GetComponent<CameraManager>().lockMovement = true;
        turnText.gameObject.SetActive(false);
        turnButton.gameObject.SetActive(false);
        diceAndBox.CreateDice(GameManager.Instance.CurrentPlayerId, 1);
        diceAndBox.CreateBox();
        return;
    }

    internal void SetPlayerFirstRollText(Player player)
    {
        if (player.name != "Computer")
        {
            playerInitialRollText.text = player.GetInitialRollValue().ToString();
            playerInitialRollText.gameObject.SetActive(true);
        }
        else
        {
            enemyInitialRollText.text = player.GetInitialRollValue().ToString();
            enemyInitialRollText.gameObject.SetActive(true);
        }
    }

    internal void ClearFirstRollTexts()
    {
        playerInitialRollText.gameObject.SetActive(false);
        enemyInitialRollText.gameObject.SetActive(false);
    }
}
