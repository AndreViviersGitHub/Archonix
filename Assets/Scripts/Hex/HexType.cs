using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HexType {

    public string name;

    public GameObject VisualHexPrefab;

    public bool isWalkable = true;

    public float movementCost = 1;
}
