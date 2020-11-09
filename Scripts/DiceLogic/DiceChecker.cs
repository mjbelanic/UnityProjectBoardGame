using UnityEngine;

public class DiceChecker : MonoBehaviour
{
    Vector3[] diceVelocity;
    Dice[] dice;

    private void Start()
    {
        dice = new Dice[GameObject.FindGameObjectsWithTag("Dice").Length];
        diceVelocity = new Vector3[GameObject.FindGameObjectsWithTag("Dice").Length];
        int count = 0;
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Dice"))
        {
            dice[count] = gameObject.GetComponent<Dice>();
            count++;
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < dice.Length; i++)
        {
            diceVelocity[i] = dice[i].diceVelocity;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        for (int i = 0; i < dice.Length; i++)
        {
            if (diceVelocity[i].x == 0f && diceVelocity[i].y == 0f && diceVelocity[i].z == 0f)
            {
                switch (other.gameObject.name)
                {
                    case "1":
                        dice[i].rollValue = 6;
                        break;
                    case "2":
                        dice[i].rollValue = 5;
                        break;
                    case "3":
                        dice[i].rollValue = 4;
                        break;
                    case "4":
                        dice[i].rollValue = 3;
                        break;
                    case "5":
                        dice[i].rollValue = 2;
                        break;
                    default:
                        dice[i].rollValue = 1;
                        break;
                }
            }
        }
    }
}
