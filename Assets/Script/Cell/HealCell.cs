using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCell : Cell {     
    
    private void Start()
    {
        IsSpecial = true;
    }
    void Update()
    {
        cell.text = value.ToString();
        
        if (Activation){
            GameManager.Health += 15;
            Activation = false;
        }
    }
}

