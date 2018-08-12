using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Cell : MonoBehaviour {
    public bool upgradedThisTurn;
    public float value=2;
    public TextMesh cell;
    public int HowTurnAfter = 0;
    public bool Activation = false;
    public bool IsSpecial = false;
    public static bool DeathGene = false;
    public static bool LockGene = true;
    public static bool NukeGene = false;
    public static bool HealGene = false;
    public static bool BuffGene = false;
    GameObject EmptyCell;    
    float BiggestNumber=0;

    private void Start()
    {
        EmptyCell = GameObject.Find("NoCell");        
        DeathGene = true;
        NukeGene = true;
        HealGene = true;
        BuffGene = true;
    }
    void Update()    {
        Searching();
        /*if (BiggestNumber >= 64) HealGene = true;
        else HealGene = false;
        if (BiggestNumber >= 128) NukeGene = true;
        else NukeGene = false;
        if (BiggestNumber >= 256) BuffGene = true;
        else BuffGene = false;
        if (GameManager.Health <= 30) DeathGene = true;        
        else DeathGene = false;*/
        for(int i=0; i<4;i++) for(int j = 0; j < 4; j++)
            {
                GameObject obj = GetObjectAtGridPosition(i, j);
                if (obj.tag == "LockCell") LockGene = false;
                else LockGene = true;
            }
    }


    void Searching()
    {
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