using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCell : Cell {

    private void Start()
    {
        IsSpecial = true;
    }
    void Update()
    {
        cell.text = value.ToString();
        if (Activation) {

        }
    }


}
