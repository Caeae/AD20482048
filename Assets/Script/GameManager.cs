using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    const float CellX = 0.8f,
                CellY = 0.8f,
                BoardX = 0.2f,
                BoardY = 0.2f,
                WallX = 0.2f,
                WallY = 0.2f;

    public GameObject ObjCell;    
    public GameObject EmptyCell;
    public static int RestCellleft = 16;
    public static int turn = 0;
    public static float Health = 100f;
    int DamageCool = 180;
    int displayCooldown = 0;
    public GameObject Restart;
    public GameObject ReGreem;
    public GameObject CellFull;
    public static bool gameover = false;

    int Garo = 4;
    int Sero = 4;
    public static float Byte = 0;
    public static List<GameObject> Cellsis;
    int i, j;
    float[,,] Cell_save = new float[21, 4, 4];
    float[] Byte_Save = new float[21];
    //[n,3,4]에는 Byte값을 저장시킴
    public enum State
    {
        Loaded,
        WaitingForInput,
        CheckingMatches,
        GenerateCell,
        UndoFunction,
        GameOver,
        Win
    }
    public static State sangtae;
    
    void Start()
    {
        Cellsis = new List<GameObject>();
        turn = 0;
        Byte = 0;        
        for (int l = 0; l < 21; l++) for (i = 0; i < 4; i++) for (j = 0; j < 4; j++)
                {
                    Cell_save[l, i, j] = 0;
                    Byte_Save[l] = 0;
                }
        Restart.SetActive(false);
        CellFull.SetActive(false);
        ReGreem.SetActive(false);
        sangtae = State.Loaded;
        
    }

    // Update is called once per frame
    void Update()
    {
        while (displayCooldown >= 0) displayCooldown--;
        if (displayCooldown <= 0) { CellFull.SetActive(false); }
        while (DamageCool >= 180)
        {
            Health--;
            DamageCool = 0;
        }
        DamageCool++;
        RestCellleft = 0;
        for (i = 0; i < 4; i++) for (j = 0; j < 4; j++)  {
                if (GetObjectAtGridPosition(i, j) == EmptyCell)
                    {
                        RestCellleft++;
                    }
                }
        }
    



    public void ReadyCellsForUpgrading()
    {
        foreach (var obj in Cellsis)
        {
            if (obj != null)
            {
                Cell cell = obj.GetComponent<Cell>();
                cell.upgradedThisTurn = false;
            }
        }
    }

    public bool CheckForMovesLeft()
    {
        if (Cellsis.Count < Garo * Sero)
        {
            return true;
        }
        for (int x = 0; x < Garo; x++)
        {
            for (int y = 0; y < Sero; y++)
            {
                Cell currentCell = GetObjectAtGridPosition(x, y).GetComponent<Cell>();
                Cell rightCell = GetObjectAtGridPosition(x + 1, y).GetComponent<Cell>();
                Cell downCell = GetObjectAtGridPosition(x, y + 1).GetComponent<Cell>();
                if (x != Garo - 1 && currentCell.value == rightCell.value)
                {
                    return true;
                }
                else if (y != Sero - 1 && currentCell.value == downCell.value)
                {
                    return true;
                }
            }
        }
        return false;
    }
   
    public GameObject GetObjectAtGridPosition(int x, int y)
    {
        RaycastHit2D hit = Physics2D.Raycast(CellToFloat(x, y), Vector2.right, BoardX);

        if (hit && hit.collider.gameObject.GetComponent<Cell>() != null)  {
            return hit.collider.gameObject;
        }
        else {
            return EmptyCell;
        }
    }


    private bool CanUpgrade(Cell thisCell, Cell thatCell)
    { // 값, 숫자가 같을때 합치게 함 && 
        return (thisCell.value == thatCell.value && !thisCell.upgradedThisTurn && !thatCell.upgradedThisTurn);
    }

    public static Vector2 CellToFloat(int x, int y) {
        return new Vector2(x - 1.5f, 1.5f - y);
    }

    public void RemoveFullCell()
    {
        float CellNumhapsan = 0;
        CellFull.SetActive(true);
        displayCooldown = 180;
        
        for (i = 0; i < 4; i++) for (j = 0; j < 4; j++)
            {
                GameObject Obj = GetObjectAtGridPosition(i, j);
                //X자 총 12칸을 없앰 , 일단은 전체를 없애는거로 진행
                if (Obj != EmptyCell)
                {
                    Cell ObjCelll = Obj.GetComponent<Cell>();
                    CellNumhapsan += ObjCelll.value;
                    ObjCelll.value = 0;
                    Cellsis.Remove(Obj);
                    Destroy(Obj);
                }
            }
        Byte += CellNumhapsan;
        GenerateRandomCell();
        GenerateRandomCell();
    }
    private void UpgradeCell(GameObject toDestroy, Cell destroyCell, GameObject toUpgrade, Cell upgradeCell)
    {
        Vector3 toUpgradePosition = toUpgrade.transform.position;
        Destroy(toDestroy);
        Destroy(toUpgrade);
        Cellsis.Remove(toUpgrade);
        Cellsis.Remove(toDestroy);
        float Count = toUpgrade.GetComponent<Cell>().value;
        GameObject newCell = Instantiate(ObjCell, toUpgradePosition, transform.rotation);
        Cellsis.Add(newCell);
        Cell celll = newCell.GetComponent<Cell>();
        celll.upgradedThisTurn = true;
        celll.GetComponent<Cell>().value = Count * 2;
        Byte += celll.GetComponent<Cell>().value;
    }

    //초기에 만들 랜덤타일 1개
    public void GenerateRandomCell()
    {
        if (Cellsis.Count >= Garo * Sero)
        {
            throw new UnityException("Unable to create new tile - grid is already full");
        }
        int value;
        // 10%확률로 4가나옴
        float highOrLowChance = Random.Range(0f, 0.99f);
        if (highOrLowChance >= 0.9f)
        {
            value = 4;
        }
        else
        {
            value = 2;
        }

        // 랜덤한 위치에 생성
        int i = Random.Range(0, Garo);
        int j = Random.Range(0, Sero);


        bool found = false;
        while (!found)
        {
            if (GetObjectAtGridPosition(i, j) == EmptyCell)
            {
                found = true;
                Vector2 worldPosition = CellToFloat(i, j);
                GameObject obj;
                if (value == 4) {
                    //obj = GameObject.Instantiate(ObjCell, worldPosition, transform.rotation);
                    obj = PoolSystem.Generate(ObjCell, worldPosition, transform.rotation);
                    obj.GetComponent<Cell>().value = 4;
                }
                else {
                    //obj = GameObject.Instantiate(ObjCell, worldPosition, transform.rotation);
                    obj = PoolSystem.Generate(ObjCell, worldPosition, transform.rotation);
                    obj.GetComponent<Cell>().value = 2;
                }
                Cellsis.Add(obj);
            }
            i++;
            if (i >= Garo)
            {
                j++;
                i = 0;
            }

            if (j >= Sero)
            {
                j = 0;
            }
        }
    }

    public void SpecificGen()  {
        int SGCheck = turn % Cellsis.Count + 1;        
        int SGHwakin = 0;
        float oneortwo = Random.Range(0, 1);
        bool Creating = false;
        while (!Creating)
            for (int j = 0; j < Sero; j++)  {
                for (int i = 0; i < Garo; i++)  {
                    if (GetObjectAtGridPosition(i, j) == EmptyCell)  {
                        SGHwakin++;
                        if (SGHwakin == SGCheck) {
                            Vector2 worldPosition = CellToFloat(i, j);
                            GameObject obj;
                            if (oneortwo <= 0.9f)
                            {//1,2가 나오는 랜덤함수에서 1이 나오면 2, 2가 나오면 4가 생성시킨다                    
                                obj = GameObject.Instantiate(ObjCell, worldPosition, transform.rotation);
                                obj.GetComponent<Cell>().value = 2;
                            }
                            else  {
                                obj = GameObject.Instantiate(ObjCell, worldPosition, transform.rotation);
                                obj.GetComponent<Cell>().value = 4;
                            }
                            Creating = true;
                            Cellsis.Add(obj);
                        }
                    }
                }
            }
    }

    public void MoveCellSaveToForward()
    {
        //데이터 구조를 큐 형식으로 사용해야하기 때문에, for문의 n이 역주행중.
        for (int n = 19; n >= 0; n--) for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++)
                {
                    Cell_save[n + 1, i, j] = Cell_save[n, i, j];
                    Byte_Save[n + 1] = Byte_Save[n];
                }
        for (i = 0; i < 4; i++) for (j = 0; j < 4; j++)
            {
                GameObject Obj = GetObjectAtGridPosition(i, j);
                Cell ObjCelll = Obj.GetComponent<Cell>();
                if (Obj != EmptyCell) Cell_save[0, i, j] = ObjCelll.value;
                else Cell_save[0, i, j] = 0;
                Byte_Save[0] = Byte;
            }
        
    }

    public void MoveCellLoadToBack()
    {
        //바뀌는 부분은 n이 정주행한다는 것, cellSave[20, i, j]가 0으로 다 바뀐다는것.
        for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++)     {
                for (int n = 0; n < 20; n++)    {
                    Cell_save[n, i, j] = Cell_save[n + 1, i, j];
                    Byte_Save[n] = Byte_Save[n + 1];
                }                
                Cell_save[20, i, j] = 0;
                Byte_Save[20] = 0;
            }
        
        for (i = 0; i < 4; i++) for (j = 0; j < 4; j++)   {
                GameObject Obj = GetObjectAtGridPosition(i, j);
                if (Cell_save[0, i, j] != 0 && Obj == EmptyCell)
                {
                    GameObject NewCell = Instantiate(ObjCell, CellToFloat(i, j), transform.rotation);
                    Cellsis.Add(NewCell);
                    NewCell.GetComponent<Cell>().value = Cell_save[0, i, j];                    
                }
                else if (Cell_save[0, i, j] == 0 && Obj != EmptyCell)
                {
                    GameObject DeleteCell = GetObjectAtGridPosition(i, j);
                    Cellsis.Remove(DeleteCell);
                    Destroy(DeleteCell);
                }
                else if (Cell_save[0, i, j] != 0 && Obj != EmptyCell)
                {                    
                    Obj.GetComponent<Cell>().value = Cell_save[0, i, j];
                }
                Byte = Byte_Save[0];
            }
        turn = turn - 1;
        //이후 cellSave[0]에 맞춰서 현재 보드의 상황을 변경하면 undo가 돌아감.
    }
}

