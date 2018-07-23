using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeACell : MonoBehaviour {
    int a = 1;
    const float BoardX = 0.2f;
    public static GameObject EmptyCell;
    public static GameObject NormalCell;
    public static GameObject DeathCell;
    public static GameObject NuclearCell;
    public static GameObject DoubleCell;
    public static GameObject LockCell;
    public static GameObject UnLockCell;
    public static GameObject HealCell;
    public static GameObject FeverCell;    
    public static GameObject LineGaroCell;
    public static GameObject LineSeroCell;
    public static GameObject BuffCell;
    // Use this for initialization
    void Start () {
        EmptyCell = GameObject.Find("NoCell");
        NormalCell = GameObject.Find("Cell");
        DeathCell = GameObject.Find("DeathCell");
        NuclearCell = GameObject.Find("NuclearCell");
        DoubleCell = GameObject.Find("Double");
        LockCell = GameObject.Find("LockCell");
        HealCell = GameObject.Find("HealCell");
        LineGaroCell = GameObject.Find("LineCellGaro");
        BuffCell = GameObject.Find("BuffCell");
    }
	
	// Update is called once per frame
	void Update () {
        if(GameManager.sangtae == GameManager.State.GenerateCell)
        {
            
            GameManager.sangtae = GameManager.State.WaitingForInput;
        }
    }

    public static GameObject GenerateCell(Vector3 Where, int c)
    {
        float CellValueCheck = 0;
        int Count = Random.Range(0,100);
        GameObject obj;
        GameObject PreObj = GetObjectAtVector(Where);
        if(PreObj != EmptyCell) {
            CellValueCheck = PreObj.GetComponent<Cell>().value;
        }
        
        switch (c) {
            case 1:
                obj = Instantiate(NormalCell, Where, Quaternion.identity);
                obj.GetComponent<Cell>().value = CellValueCheck;
                return obj;
            case 2:
                if(Cell.NukeGene) obj = Instantiate(NuclearCell, Where, Quaternion.identity);
                else obj = Instantiate(NormalCell, Where, Quaternion.identity);
                obj.GetComponent<Cell>().value = CellValueCheck;
                return obj;
            case 3:
                if(Cell.DeathGene) obj = Instantiate(DeathCell, Where, Quaternion.identity);
                else obj = Instantiate(NormalCell, Where, Quaternion.identity);
                obj.GetComponent<Cell>().value = CellValueCheck;
                return obj;
            case 4:
                obj = Instantiate(DoubleCell, Where, Quaternion.identity);
                obj.GetComponent<Cell>().value = CellValueCheck;
                return obj;
            case 5:                
                obj = Instantiate(LockCell, Where, Quaternion.identity);
                obj.GetComponent<Cell>().value = CellValueCheck;
                return obj;
            case 6:
                if(Cell.HealGene) obj = Instantiate(HealCell, Where, Quaternion.identity);
                else obj = Instantiate(NormalCell, Where, Quaternion.identity);
                obj.GetComponent<Cell>().value = CellValueCheck;
                return obj;
            case 7:
                obj = Instantiate(FeverCell, Where, Quaternion.identity);
                obj.GetComponent<Cell>().value = CellValueCheck;
                return obj;
            case 8:
                obj = Instantiate(LineGaroCell, Where, Quaternion.identity);
                obj.GetComponent<Cell>().value = CellValueCheck;
                return obj;
            case 9:
                if (Cell.BuffGene) obj = Instantiate(BuffCell, Where, Quaternion.identity);
                else obj = Instantiate(NormalCell, Where, Quaternion.identity);                
                obj.GetComponent<Cell>().value = CellValueCheck;
                return obj;
            default:
                return EmptyCell;
        }
    }
    void WorldSearch()
    {
        int i = 0, j = 0;
        for (i = 0; i < 4; i++) for (j = 0; j < 4; j++) {
                GameObject obj = GetObjectAtGridPosition(i, j);
                Vector3 World = obj.transform.position;
            }
    }

    public GameObject GetObjectAtGridPosition(int x, int y)
    {
        RaycastHit2D hit = Physics2D.Raycast(GameManager.CellToFloat(x, y), Vector2.right, BoardX);

        if (hit && hit.collider.gameObject.GetComponent<Cell>() != null)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return EmptyCell;
        }
    }
    public static GameObject GetObjectAtVector(Vector3 A)
    {
        RaycastHit2D hit = Physics2D.Raycast(A, Vector2.right, BoardX);

        if (hit && hit.collider.gameObject.GetComponent<Cell>() != null)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return EmptyCell;
        }
    }

}
