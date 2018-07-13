using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuclearCell : Cell {        
    private void Start()
    {
        IsSpecial = true;
    }
    void Update() {
        cell.text = value.ToString();    
        if(Activation) {
            float BytePlus = GetComponent<Cell>().value;
            GameManager.Byte += 128 + (BytePlus * 1.5f);
            Activation = false;
        }
    }
    
}

