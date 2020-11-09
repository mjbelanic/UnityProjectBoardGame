using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpace : MonoBehaviour
{
    public Transform position { get; set; }
    public List<TileSpace> connectedSpaces = new List<TileSpace>();
}
