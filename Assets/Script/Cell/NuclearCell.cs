using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuclearCell : Cell {
    public GameObject EmptyCell;
    bool CanGenerate = false;
    float BiggestNumber=0;
    private void Start()
    {
        IsSpecial = true;
    }
    void Update() {
        cell.text = value.ToString();
        Searching();
        if (BiggestNumber >= 128) CanGenerate = true;
        else CanGenerate = false;

        if(Activation) {
            float BytePlus = GetComponent<Cell>().value;
            GameManager.Byte += 128 + (BytePlus * 1.5f);
            Activation = false;
        }
    }
    void Searching()    {
        int i = 0, j = 0;
        float counting = 0;
        for (i = 0; i < 4; i++) for (j = 0; j < 4; j++)
            {
                GameObject obj = GetObjectAtGridPosition(i, j);
                if (obj != EmptyCell)
                {
                    if (counting < obj.GetComponent<Cell>().value) counting = obj.GetComponent<Cell>().value;
                }
            }
        BiggestNumber = counting;
    }
    public GameObject GetObjectAtGridPosition(int x, int y)
    {
        RaycastHit2D hit = Physics2D.Raycast(CellToFloat(x, y), Vector2.right, 0.2f);

        if (hit && hit.collider.gameObject.GetComponent<Cell>() != null)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return EmptyCell;
        }
    }
    public static Vector2 CellToFloat(int x, int y)
    {
        return new Vector2(x - 1.5f, 1.5f - y);
    }
}

