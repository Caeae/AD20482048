using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCell : MonoBehaviour {

    public bool upgradedThisTurn;
    public int value = 2;
    public TextMesh cell;
    public int HowTurnAfter = 0;

    void Update()
    {
        cell.text = value.ToString();
    }
}
