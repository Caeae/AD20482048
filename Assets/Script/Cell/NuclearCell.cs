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
            Debug.Log("효과 발동하였습니다");
            float BytePlus = this.GetComponent<Cell>().value;
            GameManager.Byte = GameManager.Byte + 128 + (BytePlus * 1.5f);
            Activation = false;
        }
    }
    
}

