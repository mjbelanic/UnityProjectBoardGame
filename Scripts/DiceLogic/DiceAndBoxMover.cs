using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceAndBoxMover : MonoBehaviour
{
    public Vector3[] dice1Coordinates = new Vector3[3];
    public Vector3 boxCoordinates;
    public GameObject dicePrefab;
    public GameObject diceBoxPrefab;
    
    GameObject[] dices = new GameObject[3];
    GameObject diceBox;
    int numberOfDice;

    // Start is called before the first frame update
    void Start()
    {
        dice1Coordinates[0] = new Vector3(75,25,0);
        dice1Coordinates[1] = new Vector3(75, 25, 15);
        dice1Coordinates[2] = new Vector3(75, 25, -15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void CreateDice(int playerId, int numOfDices)
    {
        numberOfDice = numOfDices;
        for(int i = 0; i < numOfDices; i++)
        {
            GameObject dice = Instantiate(dicePrefab, dice1Coordinates[i], Quaternion.identity);
            dices[i] = dice;
        }
    }

    internal void CreateBox()
    {
        diceBox = Instantiate(diceBoxPrefab, boxCoordinates, Quaternion.identity);
    }

    internal void MoveBoxAndDiceIntoView()
    {

    }

    internal void MoveBoxAndDiceOutOfView()
    {

    }

    internal void DestroyDicesAndBox()
    {
        for (int i = 0; i < numberOfDice; i++)
        {
            Destroy(dices[i]);
        }
        Destroy(diceBox);
    }
}
