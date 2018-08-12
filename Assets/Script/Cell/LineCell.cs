using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCell : Cell {
    private void Start()
    {
        IsSpecial = true;
    }
    void Update()
    {
        cell.text = value.ToString();
    }
}
