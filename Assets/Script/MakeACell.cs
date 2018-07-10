using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeACell : MonoBehaviour {
    int a = 1;
    float BoardX = 0.2f;
    public GameObject EmptyCell;
    public GameObject NormalCell;
    public GameObject DeathCell;
    public GameObject NuclearCell;
    public GameObject DoubleCell;
    public GameObject LockCell;
    public GameObject UnLockCell;
    public GameObject HealCell;
    public GameObject FeverCell;
    public GameObject WarpCell;
    public GameObject LineGaroCell;
    public GameObject LineSeroCell;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GenerateCell(Vector3 Where, float c)
    {
        float CellValueCheck = 0;
        GameObject obj;
        GameObject PreObj = GetObjectAtVector(Where);
        if(PreObj != EmptyCell) {
            CellValueCheck = PreObj.GetComponent<Cell>().value;
        }

        switch (a) {
            case 1:
                obj = Instantiate(NormalCell, Where, transform.rotation);
                obj.GetComponent<Cell>().value = CellValueCheck;
                break;
            case 2:
                obj = Instantiate(NuclearCell, Where, transform.rotation);
                obj.GetComponent<Cell>().value = CellValueCheck;
                break;
            case 3:
                obj = Instantiate(DeathCell, Where, transform.rotation);
                obj.GetComponent<Cell>().value = CellValueCheck;
                break;
            case 4:
                obj = Instantiate(DoubleCell, Where, transform.rotation);
                obj.GetComponent<Cell>().value = CellValueCheck;
                break;
            case 5:
                obj = Instantiate(LockCell, Where, transform.rotation);
                obj.GetComponent<Cell>().value = CellValueCheck;
                break;
            case 6:
                obj = Instantiate(HealCell, Where, transform.rotation);
                obj.GetComponent<Cell>().value = CellValueCheck;
                break;
            case 7:
                obj = Instantiate(FeverCell, Where, transform.rotation);
                obj.GetComponent<Cell>().value = CellValueCheck;
                break;
            case 8:
                obj = Instantiate(WarpCell, Where, transform.rotation);
                obj.GetComponent<Cell>().value = CellValueCheck;
                break;
            case 9:
                obj = Instantiate(LineGaroCell, Where, transform.rotation);
                obj.GetComponent<Cell>().value = CellValueCheck;
                break;
            default:
                break;
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
    public GameObject GetObjectAtVector(Vector3 A)
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
