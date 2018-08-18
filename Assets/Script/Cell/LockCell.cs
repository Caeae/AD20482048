using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCell : Cell {
    Vector3 Where;
    private void Start()
    {
        IsSpecial = true;
    }
    void Update()
    {
        Where = this.transform.position;
        cell.text = value.ToString();
        if (Activation) {
            
        }
    }


}
