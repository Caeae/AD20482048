using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingScript : MonoBehaviour {
    const float CellX = 0.8f,
            CellY = 0.8f,
            BoardX = 0.2f,
            BoardY = 0.2f,
            WallX = 0.2f,
            WallY = 0.2f;

    public GameObject ObjCell;
    public GameObject EmptyCell;
    public static bool gameover = false;
    int i=0, j=0;
    int Garo = 4;
    int Sero = 4;
    public static float ScoreHap = 0;
    PoolSystem PoolManager;    
    // Use this for initialization
    void Start() {
        PoolManager = GameObject.Find("CellManageMent").GetComponent<PoolSystem>();        
    }

    // Update is called once per frame
    void Update()
    {
        #region 상하좌우입력
        if (GameManager.sangtae == GameManager.State.WaitingForInput)
        {
 
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (MoveCellsLeft())
                {
                    GameManager.sangtae = GameManager.State.CheckingMatches;
                    
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (MoveCellsRight())
                {
                    GameManager.sangtae = GameManager.State.CheckingMatches;
                    
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (MoveCellsUp())
                {
                    GameManager.sangtae = GameManager.State.CheckingMatches;
                    
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (MoveCellsDown())
                {
                    GameManager.sangtae = GameManager.State.CheckingMatches;
                    
                }
            }
        }
        #endregion
    }
    
    #region 상하좌우
    private bool MoveCellsLeft()
    {
        bool hasMoved = false;
        for (int x = 1; x < Garo; x++)
        {
            for (int y = 0; y < Sero; y++)
            {
                GameObject obj = GetObjectAtGridPosition(x, y);
                //if (obj == noTile)
                if (obj == EmptyCell || obj.tag =="LockCell")
                {
                    continue;
                }
                Vector2 raycastOrigin = obj.transform.position;
                raycastOrigin.x -= 0.5f;
                RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.left, Mathf.Infinity);
                if (hit.collider != null)
                {
                    GameObject hitObject = hit.collider.gameObject;
                    if (hitObject != obj)
                    {
                        //부딪힌게 셀이면
                        if (hitObject.tag == "Cell")
                        {
                            Cell thatCell = hitObject.GetComponent<Cell>();
                            Cell thisCell = obj.GetComponent<Cell>();
                            //상승이 가능하면
                            if (CanUpgrade(thisCell, thatCell))
                            {
                                UpgradeCell(obj, thisCell, hitObject, thatCell);
                                hasMoved = true;
                            }
                            //상승할수 없는 상태면
                            Vector3 newPosition = hitObject.transform.position;
                            newPosition.x += 1f;
                            //부딪힌 자리와 자신의 자리가 비슷하다면

                            if (!Mathf.Approximately(obj.transform.position.x, newPosition.x))
                            {
                                //그 부딪힌 위치로 지정함

                                obj.transform.position = newPosition;
                                hasMoved = true;
                            }

                        }
                        //벽에 부딪히면
                        else if (hitObject.tag == "Wall")
                        {
                            Vector3 newPosition = obj.transform.position;
                            newPosition.x = hit.point.x + 0.6f;
                            //벽에서 오른쪽으로 간격 벌려주기
                            //부딪힌 자리와 자신의 자리가 비슷하다면
                            if (!Mathf.Approximately(obj.transform.position.x, newPosition.x))
                            {
                                obj.transform.position = newPosition;
                                hasMoved = true;
                            }
                        }
                        else if (hitObject.tag == "LockCell")
                        {
                            Cell thatCell = hitObject.GetComponent<Cell>();
                            Cell thisCell = obj.GetComponent<Cell>();
                            if (LockCanUpgrade(thisCell, thatCell))
                            {
                                UpgradeCell(obj, thisCell, hitObject, thatCell);
                                hasMoved = true;
                            }
                            Vector3 newPosition = hitObject.transform.position;
                            newPosition.x += 1f;
                            //newPosition.y -= spaceBetweenTiles;
                            //부딪힌 자리와 자신의 자리가 비슷하다면
                            if (!Mathf.Approximately(obj.transform.position.y, newPosition.y))
                            {
                                //그 부딪힌 위치로 지정함

                                obj.transform.position = newPosition;
                                hasMoved = true;
                            }

                        }
                    }
                }
            }
        }

        return hasMoved;

    }

    private bool MoveCellsRight()
    {
        bool hasMoved = false;
        for (int x = Garo - 1; x >= 0; x--)
        {
            for (int y = 0; y < Sero; y++)
            {
                GameObject obj = GetObjectAtGridPosition(x, y);
                //if (obj == noTile)
                if (obj == EmptyCell || obj.tag =="LockCell")
                {
                    continue;
                }
                Vector2 raycastOrigin = obj.transform.position;
                raycastOrigin.x += 0.5f;
                //raycastOrigin.x += halfTileWidth;
                RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.right, Mathf.Infinity);
                //레이캐스트 : 빛처럼 쏘는형식으로 충돌판정확인 , 
                if (hit.collider != null)
                {
                    GameObject hitObject = hit.collider.gameObject;
                    if (hitObject != obj)
                    {
                        //부딪힌게 셀이면
                        if (hitObject.tag == "Cell")
                        {
                            Cell thatCell = hitObject.GetComponent<Cell>();
                            Cell thisCell = obj.GetComponent<Cell>();
                            if (CanUpgrade(thisCell, thatCell))
                            {
                                UpgradeCell(obj, thisCell, hitObject, thatCell);
                                hasMoved = true;
                            }                            //상승할수 없는 상태면                            
                            Vector3 newPosition = hitObject.transform.position;
                            //newPosition.y -= spaceBetweenTiles;
                            newPosition.x -= 1f;
                            //부딪힌 자리와 자신의 자리가 비슷하지 않다면
                            if (!Mathf.Approximately(obj.transform.position.x, newPosition.x))
                            {
                                //그 부딪힌 위치로 지정함

                                obj.transform.position = newPosition;
                                hasMoved = true;
                            }
                        }
                        //벽에 부딪히면
                        else if (hitObject.tag == "Wall")
                        {
                            Vector3 newPosition = obj.transform.position;
                            newPosition.x = hit.point.x - 0.6f;
                            // newPosition.x = hit.point.x - halfTileWidth - borderOffset; 벽에서 왼쪽으로 간격 벌려주기
                            //부딪힌 자리와 자신의 자리가 비슷하지 않다면
                            if (!Mathf.Approximately(obj.transform.position.x, newPosition.x))
                            {
                                obj.transform.position = newPosition;
                                hasMoved = true;
                            }
                        }
                        else if (hitObject.tag == "LockCell")
                        {
                            Cell thatCell = hitObject.GetComponent<Cell>();
                            Cell thisCell = obj.GetComponent<Cell>();
                            if (LockCanUpgrade(thisCell, thatCell))
                            {
                                UpgradeCell(obj, thisCell, hitObject, thatCell);
                                hasMoved = true;
                            }
                            Vector3 newPosition = hitObject.transform.position;
                            newPosition.x -= 1f;
                            //newPosition.y -= spaceBetweenTiles;
                            //부딪힌 자리와 자신의 자리가 비슷하다면
                            if (!Mathf.Approximately(obj.transform.position.y, newPosition.y))
                            {
                                //그 부딪힌 위치로 지정함

                                obj.transform.position = newPosition;
                                hasMoved = true;
                            }

                        }
                    }
                }
            }
        }

        return hasMoved;

    }

    private bool MoveCellsUp()
    {
        bool hasMoved = false;
        for (int y = 1; y < Sero; y++)
        {
            for (int x = 0; x < Garo; x++)
            {
                GameObject obj = GetObjectAtGridPosition(x, y);
                //if (obj == noTile)
                if (obj == EmptyCell || obj.tag =="LockCell")
                {
                    continue;
                }
                Vector2 raycastOrigin = obj.transform.position;
                raycastOrigin.y += 0.5f;
                //raycastOrigin.y += halfTileWidth;
                //합쳐지는걸 확인
                RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.up, Mathf.Infinity);
                if (hit.collider != null)
                {
                    GameObject hitObject = hit.collider.gameObject;
                    if (hitObject != obj)
                    {
                        //부딪힌게 셀이면
                        if (hitObject.tag == "Cell")
                        {
                            Cell thatCell = hitObject.GetComponent<Cell>();
                            Cell thisCell = obj.GetComponent<Cell>();
                            //상승이 가능하면
                            if (CanUpgrade(thisCell, thatCell))
                            {
                                UpgradeCell(obj, thisCell, hitObject, thatCell);
                                hasMoved = true;
                            }
                            //상승할수 없는 상태면 한칸만큼 간격 벌림

                            Vector3 newPosition = hitObject.transform.position;
                            newPosition.y -= 1f;
                            //newPosition.y -= spaceBetweenTiles;
                            //부딪힌 자리와 자신의 자리가 비슷하다면
                            if (!Mathf.Approximately(obj.transform.position.y, newPosition.y))
                            {
                                //그 부딪힌 위치로 지정함

                                obj.transform.position = newPosition;
                                hasMoved = true;
                            }

                        }
                        //벽에 부딪히면
                        else if (hitObject.tag == "Wall")
                        {
                            Vector3 newPosition = obj.transform.position;
                            newPosition.y = hit.point.y - 0.6f;
                            // newPosition.y = hit.point.y - halfTileWidth - borderOffset;
                            //부딪힌 자리와 자신의 자리가 비슷하지 않다면
                            if (!Mathf.Approximately(obj.transform.position.y, newPosition.y))
                            {
                                obj.transform.position = newPosition;
                                hasMoved = true;
                            }
                        }
                        else if (hitObject.tag == "LockCell") {
                            Cell thatCell = hitObject.GetComponent<Cell>();
                            Cell thisCell = obj.GetComponent<Cell>();
                            if (LockCanUpgrade(thisCell, thatCell)) {
                                UpgradeCell(obj, thisCell, hitObject, thatCell);
                                hasMoved = true;
                            }
                            Vector3 newPosition = hitObject.transform.position;
                            newPosition.y -= 1f;
                            //newPosition.y -= spaceBetweenTiles;
                            //부딪힌 자리와 자신의 자리가 비슷하다면
                            if (!Mathf.Approximately(obj.transform.position.y, newPosition.y))
                            {
                                //그 부딪힌 위치로 지정함

                                obj.transform.position = newPosition;
                                hasMoved = true;
                            }

                        }
                    }
                }
            }
        }

        return hasMoved;

    }

    private bool MoveCellsDown()
    {
        bool hasMoved = false;
        for (int y = Sero - 1; y >= 0; y--)
        {
            for (int x = 0; x < Garo; x++)
            {
                GameObject obj = GetObjectAtGridPosition(x, y);
                //if (obj == noTile)
                if (obj == EmptyCell || obj.tag =="LockCell")
                {
                    continue;
                }
                Vector2 raycastOrigin = obj.transform.position;
                raycastOrigin.y -= 0.5f;
                //raycastOrigin.y -= halfTileWidth; 자기 자신과 안부딪히게 빛 발사
                RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, Mathf.Infinity);
                if (hit.collider != null)
                {
                    GameObject hitObject = hit.collider.gameObject;
                    if (hitObject != obj)
                    {
                        //부딪힌게 셀이면
                        if (hitObject.tag == "Cell")
                        {
                            Cell thatCell = hitObject.GetComponent<Cell>();
                            Cell thisCell = obj.GetComponent<Cell>();
                            //상승이 가능하면
                            if (CanUpgrade(thisCell, thatCell))
                            {
                                UpgradeCell(obj, thisCell, hitObject, thatCell);
                                hasMoved = true;
                            }
                            //상승할수 없는 상태면

                            Vector3 newPosition = hitObject.transform.position;
                            newPosition.y += 1f;
                            //newPosition.y += spaceBetweenTiles;
                            //부딪힌 자리와 자신의 자리가 비슷하다면
                            if (!Mathf.Approximately(obj.transform.position.y, newPosition.y))
                            {
                                obj.transform.position = newPosition;
                                hasMoved = true;
                            }
                        }
                        //벽에 부딪히면
                        else if (hitObject.tag == "Wall")
                        {
                            Vector3 newPosition = obj.transform.position;
                            newPosition.y = hit.point.y + 0.6f;
                            // newPosition.y = hit.point.y + halfTileWidth + borderOffset; 부딪힌데에서 위쪽으로 간격 벌림
                            //부딪힌 자리와 자신의 자리가 비슷하다면 (소수점 제거)
                            if (!Mathf.Approximately(obj.transform.position.y, newPosition.y)) {
                                obj.transform.position = newPosition;
                                hasMoved = true;
                            }
                        }
                        else if (hitObject.tag == "LockCell") {
                            Cell thatCell = hitObject.GetComponent<Cell>();
                            Cell thisCell = obj.GetComponent<Cell>();
                            if (LockCanUpgrade(thisCell, thatCell)) {
                                UpgradeCell(obj, thisCell, hitObject, thatCell);
                                hasMoved = true;
                            }
                            Vector3 newPosition = hitObject.transform.position;
                            newPosition.y += 1f;
                            
                            //부딪힌 자리와 자신의 자리가 비슷하다면
                            if (!Mathf.Approximately(obj.transform.position.y, newPosition.y)) {
                                //그 부딪힌 위치로 지정함

                                obj.transform.position = newPosition;
                                hasMoved = true;
                            }
                        }
                    }
                }
            }
        }

        return hasMoved;

    }
    #endregion
    private GameObject GetObjectAtGridPosition(int x, int y) {
        RaycastHit2D hit = Physics2D.Raycast(CellToFloat(x, y), Vector2.right, BoardX);

        if (hit && hit.collider.gameObject.GetComponent<Cell>() != null) {
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

    private bool LockCanUpgrade(Cell thisCell, Cell thatCell)
    { // 숫자 16이상일때 합치게 함
        return (thisCell.value >= 16 && !thisCell.upgradedThisTurn && !thatCell.upgradedThisTurn);
    }

    private void ReadyCellsForUpgrading()
    {
        foreach (var obj in GameManager.Cellsis) {
            Cell block = obj.GetComponent<Cell>();
            block.upgradedThisTurn = false;
        }
    }


    private static Vector2 CellToFloat(int x, int y) {
        return new Vector2(x - 1.5f, 1.5f - y);
    }

    private void UpgradeCell(GameObject toDestroy, Cell destroyCell, GameObject toUpgrade, Cell upgradeCell) {
        Vector3 toUpgradePosition = toUpgrade.transform.position;
        destroyCell.Activation = true;
        upgradeCell.Activation = true;
        GameManager.Cellsis.Remove(toUpgrade);
        GameManager.Cellsis.Remove(toDestroy);
        PoolManager.DeleteCell(toDestroy);
        PoolManager.DeleteCell(toUpgrade);
        float Count = toUpgrade.GetComponent<Cell>().value;
        GameObject newCell;
        if (Random.Range(0, 100) < 2 && Cell.LockGene) { newCell = PoolManager.LockGenerate(toUpgradePosition, Quaternion.identity); Cell.LockGene = false; }
        else newCell = PoolManager.Generate(ObjCell, toUpgradePosition, Quaternion.identity);        
        Cell cells = newCell.GetComponent<Cell>();
        cells.upgradedThisTurn = true;
        cells.Activation = false;
        cells.GetComponent<Cell>().value = Count * 2;        
        ScoreHap += cells.GetComponent<Cell>().value;        
    }

    private void UpgradeLockCell(GameObject toDestroy, Cell destroyCell, GameObject toUpgrade, Cell upgradeCell)
    {
        Vector3 toUpgradePosition = toUpgrade.transform.position;
        destroyCell.Activation = true;
        upgradeCell.Activation = true;
        GameManager.Cellsis.Remove(toUpgrade);
        GameManager.Cellsis.Remove(toDestroy);
        PoolManager.DeleteCell(toDestroy);
        PoolManager.DeleteCell(toUpgrade);
        float Count = toUpgrade.GetComponent<Cell>().value;
        GameObject newCell = PoolManager.Generate(ObjCell, toUpgradePosition, Quaternion.identity);
        Cell.LockGene = true;
        Cell cells = newCell.GetComponent<Cell>();
        cells.upgradedThisTurn = true;
        cells.Activation = false;
        cells.GetComponent<Cell>().value = Count * 2;
        ScoreHap += cells.GetComponent<Cell>().value;
    }

    void SearchHighValue()
    {
        for(i=0; i<4;i++) for (j = 0; j < 4; j++)
            {
                GameObject Obj = GetObjectAtGridPosition(i, j);
                if (Obj != null)
                {

                }
            }
    }
}