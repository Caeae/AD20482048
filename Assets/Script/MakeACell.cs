using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeACell : MonoBehaviour {
    int a = 1;
    const float BoardX = 0.2f;
    bool WasGenerate = false;
    public GameObject EmptyCell;
    public GameObject NormalCell;
    public GameObject DeathCell;
    public GameObject NuclearCell;
    public GameObject DoubleCell;
    public GameObject LockCell;
    public GameObject UnLockCell;
    public GameObject HealCell;
    public GameObject FeverCell;    
    public GameObject LineGaroCell;
    public GameObject LineSeroCell;
    public GameObject BuffCell;
    PoolSystem PoolManageMent;
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
        PoolManageMent = GameObject.Find("CellManageMent").GetComponent<PoolSystem>();
    }
	
	// Update is called once per frame
	void Update () {     
        float HealthPerCent = 100 - (30 - GameManager.Health) / 3;
        float HealCellGenerateP = 99 - (100 - GameManager.Health) / 25; // 99 - 4 = 95, 5%임
        if (GameManager.sangtae == GameManager.State.GenerateCell) {

            Debug.Log("GenerateCell 진입");
            for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++)
                {
                    GameObject obj = GetObjectAtGridPosition(i, j);
                    Vector3 CellWorld = GameManager.CellToFloat(i, j);
                    GameObject GeneCell;
                    if (obj != EmptyCell)
                    {
                        float CellValueCheck = obj.GetComponent<Cell>().value;
                        while (!WasGenerate)
                        {                        
                            if (Random.Range(0,100) > 50) { //HealthPerCent
                                
                                PoolManageMent.DeleteCell(obj);
                                if (Cell.DeathGene) GeneCell = PoolManageMent.Generate(DeathCell, CellWorld, Quaternion.identity);
                                else GeneCell = PoolManageMent.Generate(NormalCell, CellWorld, Quaternion.identity);
                                GeneCell.GetComponent<Cell>().value = CellValueCheck;
                                Debug.Log("DeathCell 생성");

                                WasGenerate = true;
                            }                        
                            else if (Random.Range(0,100) > 50) { //98

                                PoolManageMent.DeleteCell(obj);
                                if (Cell.NukeGene) GeneCell = PoolManageMent.Generate(NuclearCell, CellWorld, Quaternion.identity);
                                else GeneCell = PoolManageMent.Generate(NormalCell, CellWorld, Quaternion.identity);
                                GeneCell.GetComponent<Cell>().value = CellValueCheck;
                                Debug.Log("NukeCell 생성");

                                WasGenerate = true;
                            }
                            else if(Random.Range(0,100) >50) {//HealCellGenerateP

                                PoolManageMent.DeleteCell(obj);
                                if (Cell.HealGene) GeneCell = PoolManageMent.Generate(HealCell, CellWorld, Quaternion.identity);
                                else GeneCell = PoolManageMent.Generate(NormalCell, CellWorld, Quaternion.identity);
                                Debug.Log("HealCell 생성");
                                GeneCell.GetComponent<Cell>().value = CellValueCheck;
                                WasGenerate = true;
                            }
                        }
                        
                    }
                }

            GameManager.sangtae = GameManager.State.WaitingForInput;
            WasGenerate = false;
        }
    }

    public GameObject GenerateCell(Vector3 Where, int c)
    {        
        int Count = Random.Range(0,100);
        GameObject obj;
        GameObject PreObj = GetObjectAtVector(Where);
        if(PreObj != EmptyCell) {
            
        }
        
        switch (c) {            
            case 2:
                if (Cell.NukeGene) obj = PoolManageMent.Generate(NuclearCell, Where, Quaternion.identity);
                else obj = PoolManageMent.Generate(NormalCell, Where, Quaternion.identity);
                Debug.Log("NukeCell 생성");
                return obj;
            case 3:
                if(Cell.DeathGene) obj = PoolManageMent.Generate(DeathCell, Where, Quaternion.identity);
                else obj = PoolManageMent.Generate(NormalCell, Where, Quaternion.identity);
                Debug.Log("DeathCell 생성");
                return obj;
            case 4:
                obj = PoolManageMent.Generate(DoubleCell, Where, Quaternion.identity);
                
                return obj;
            case 5:                
                obj = PoolManageMent.Generate(LockCell, Where, Quaternion.identity);
                
                return obj;
            case 6:
                if(Cell.HealGene) obj = PoolManageMent.Generate(HealCell, Where, Quaternion.identity);
                else obj = PoolManageMent.Generate(NormalCell, Where, Quaternion.identity);
                Debug.Log("HealCell 생성");
                return obj;
            case 7:
                obj = PoolManageMent.Generate(FeverCell, Where, Quaternion.identity);
                
                return obj;
            case 8:
                obj = PoolManageMent.Generate(LineGaroCell, Where, Quaternion.identity);
                
                return obj;
            case 9:
                if (Cell.BuffGene) obj = PoolManageMent.Generate(BuffCell, Where, Quaternion.identity);
                else obj = PoolManageMent.Generate(NormalCell, Where, Quaternion.identity);                
                
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
