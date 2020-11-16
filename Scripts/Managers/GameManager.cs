using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public int NumberOfPlayers = 2;
    public int CurrentPlayerId;
    public int DiceTotal;
    public HUDManager hudManager;
    public AIManager ai;
    public GameStates state;
    internal int turnNumber;
    public Player playerOne;
    public Player enemy;
    public float waitTime = 2.5f;

    private bool DiceBoardBeingRemoved;

    public static GameManager Instance { get { return _instance; } }

    // Singleton logic
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        CurrentPlayerId = 0;
        turnNumber = 0;
        DiceBoardBeingRemoved = false;
    }

    private void Update()
    {
        // Don't allow computer to begin turn until dice from player's turn are removed
        if (DiceBoardBeingRemoved)
        {
            return;
        }
        // Determine which player is going
        if (CurrentPlayerId == 0)
        {
            if (state == GameStates.Start)
            {
                StartTurn(playerOne.name);
            }
            if (state == GameStates.RollPhase)
            {
                if (playerOne.GetInitialRollValue() != 0)
                {
                    StartCoroutine("SetTextAndDestroyPieces");
                }
            }
            if (state == GameStates.EndingTurn)
            {
                EndTurn();
            }
        }
        else
        {
            if (state == GameStates.Start)
            {
                StartTurn(enemy.name);
            }
            if (state == GameStates.RollPhase)
            {
                if (enemy.GetInitialRollValue() != 0)
                {
                    StartCoroutine("SetTextAndDestroyPieces");
                }
            }
            if (state == GameStates.EndingTurn)
            {
                EndTurn();
            }
        }
    }

    // TODO: Make an else if to check that turnNumber == 0 so we can move
    // TODO: Insert game logic for movement game state
    public void StartTurn(string player)
    {
        if (enemy.GetInitialRollValue() == 0 || playerOne.GetInitialRollValue() == 0)
        {
            hudManager.DisplayRollCommand(player);
            state = GameStates.RollPhase;
        }
        else
        {
            DetermineWhoGoesFirst();
        }
    }

    // Send Player information to HUD for display
    internal IEnumerator SetTextAndDestroyPieces()
    {
        DiceBoardBeingRemoved = true;
        if (CurrentPlayerId == 0)
        {
            hudManager.SetPlayerFirstRollText(playerOne);
        }
        else
        {
            hudManager.SetPlayerFirstRollText(enemy);
        }
        yield return new WaitForSeconds(waitTime);
        hudManager.diceAndBox.DestroyDicesAndBox();

        // If still deciding who goes first, jump to end turn state
        // Else allow player to move
        if (turnNumber == 0)
        {
            state = GameStates.EndingTurn;
        }
        else
        {
            state = GameStates.MovePhase;
        }
        DiceBoardBeingRemoved = false;
    }

    internal void EndTurn()
    {
        CurrentPlayerId = (CurrentPlayerId + 1) % NumberOfPlayers;
        state = GameStates.Start;
    }

    private void DetermineWhoGoesFirst()
    {
        if(playerOne.GetInitialRollValue() > enemy.GetInitialRollValue())
        {
            turnNumber++;
            EndTurn();
        }
        else if(enemy.GetInitialRollValue() > playerOne.GetInitialRollValue())
        {
            turnNumber++;
            state = GameStates.Start;
        }
        else
        {
            enemy.SetInitialRoll(0);
            playerOne.SetInitialRoll(0);
            EndTurn();
        }
    }
}
