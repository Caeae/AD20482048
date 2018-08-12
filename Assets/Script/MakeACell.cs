using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeACell : MonoBehaviour {    
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
        float DCP = (30 - GameManager.Health) / 3; //통상 생길수 없음, 30밑으로 내려가면 0~10%
        if (DCP < 0) DCP = 0;
        float HCP = 1 - (GameManager.Health - 100) / 25; //1 +~4%, 최대 5%임
        float Chance = Random.Range(0, 100);
        int Randum = Random.Range(0, 100);
        if (GameManager.sangtae == GameManager.State.GenerateCell) {      
            for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++)
                {
                    GameObject obj = GetObjectAtGridPosition(i, j);
                    Vector3 CellWorld = GameManager.CellToFloat(i, j);
                    GameObject GeneCell;
                    
                    if (obj != EmptyCell)// && !obj.GetComponent<Cell>().IsSpecial) 
                    {
                        float CellValueCheck = obj.GetComponent<Cell>().value;
                        while (!WasGenerate) {
                            if (Chance < DCP)
                            {
                                PoolManageMent.DeleteCell(obj);
                                if (Cell.DeathGene) GeneCell = PoolManageMent.DeathGenerate(CellWorld, Quaternion.identity);
                                else GeneCell = PoolManageMent.Generate(NormalCell, CellWorld, Quaternion.identity);
                                GeneCell.GetComponent<Cell>().value = CellValueCheck;
                                Debug.Log("DeathCell 생성");
                                WasGenerate = true;

                            }
                            else if (Chance < DCP + 2)
                            {            
                                Debug.Log(Randum);
                                PoolManageMent.DeleteCell(obj);
                                if (Cell.NukeGene) GeneCell = PoolManageMent.NuclearGenerate(CellWorld, Quaternion.identity);
                                else GeneCell = PoolManageMent.Generate(NormalCell, CellWorld, Quaternion.identity);
                                GeneCell.GetComponent<Cell>().value = CellValueCheck;
                                WasGenerate = true;

                            }
                            else if (Chance < DCP + HCP + 2)
                            {//DCP + HCP + 2

                                PoolManageMent.DeleteCell(obj);
                                if (Cell.HealGene) GeneCell = PoolManageMent.HealGenerate(CellWorld, Quaternion.identity);
                                else GeneCell = PoolManageMent.Generate(NormalCell, CellWorld, Quaternion.identity);
                                Debug.Log("HealCell 생성");
                                GeneCell.GetComponent<Cell>().value = CellValueCheck;
                                WasGenerate = true;

                            }

                            else WasGenerate = true;
                        }
                        
                    }
                }
                
            GameManager.sangtae = GameManager.State.WaitingForInput;
            WasGenerate = false;
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
