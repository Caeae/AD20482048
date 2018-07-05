﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCell : Cell {     
    public static bool CanGenerate = false;    
    public GameObject EmptyCell;
    int BiggestNumber = 0;

    void Update(){        
        Searching();
        if (BiggestNumber >= 64) {
            CanGenerate = true;
        }
        else CanGenerate = false;
        if (Activation) GameManager.Health += 15;
    }

    void Searching()    {
        int i = 0, j = 0, counting =0;
        for (i = 0; i < 4; i++) for (j = 0; j < 4; j++) {
                GameObject obj = GetObjectAtGridPosition(i, j);
                if(obj != EmptyCell) {
                    if(counting < obj.GetComponent<Cell>().value)   counting = obj.GetComponent<Cell>().value;
                }
            }
        BiggestNumber=counting;
    }
    public GameObject GetObjectAtGridPosition(int x, int y)
    {
        RaycastHit2D hit = Physics2D.Raycast(CellToFloat(x, y), Vector2.right, 0.2f);

        if (hit && hit.collider.gameObject.GetComponent<Cell>() != null) {
            return hit.collider.gameObject;
        }
        else  {
            return EmptyCell;
        }
    }
    public static Vector2 CellToFloat(int x, int y)
    {
        return new Vector2(x - 1.5f, 1.5f - y);
    }
}

