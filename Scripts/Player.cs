using UnityEngine;

public enum Type { Player, Enemy }

public class Player : MonoBehaviour
{
    public int[] RollValues;
    public string name;
    private int initialRoll;
    private Team[] teams;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void SetInitialRoll(int rollValue)
    {
        initialRoll = rollValue;
    }

    internal int GetInitialRollValue()
    {
        return initialRoll;
    }
}
