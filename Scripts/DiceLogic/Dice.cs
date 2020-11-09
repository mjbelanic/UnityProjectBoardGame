using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DiceState { NotRolled, Rolled }

public class Dice : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 diceVelocity;
    public int rollValue;
    DiceState currentState;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rollValue = 0;
        currentState = DiceState.NotRolled;
    }

    // Update is called once per frame
    void Update()
    {
        diceVelocity = rb.velocity;
        if (GameManager.Instance.CurrentPlayerId == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && 
                GameManager.Instance.state == GameStates.RollPhase && 
                currentState == DiceState.NotRolled)
            {
                RollDice();
            }
            if (rollValue != 0 && GameManager.Instance.turnNumber == 0)
            {
                GameManager.Instance.playerOne.SetInitialRoll(rollValue);
            }
        }
        else
        {
            if (GameManager.Instance.state == GameStates.RollPhase 
                && currentState == DiceState.NotRolled)
            {
                RollDice();
            }
            if (rollValue != 0 && GameManager.Instance.turnNumber == 0)
            {
                GameManager.Instance.enemy.SetInitialRoll(rollValue);
            }
        }
    }


    internal void RollDice()
    {
        currentState = DiceState.Rolled;
        rb.useGravity = true;
        float dirX = Random.Range(0, 500);
        float dirY = Random.Range(0, 500);
        float dirZ = Random.Range(0, 500);
        rb.AddForce(-transform.right * 3000);
        rb.AddTorque(dirX, dirY, dirZ);
    }
}
