using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCell : Cell {
    
    void Update()
    {
        cell.text = value.ToString();
    }


}
