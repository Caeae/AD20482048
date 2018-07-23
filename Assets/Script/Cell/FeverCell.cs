using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverCell : Cell {
    
    void Update()
    {
        cell.text = value.ToString();
        if (Activation){


            Activation = false;
        }
    }
}
