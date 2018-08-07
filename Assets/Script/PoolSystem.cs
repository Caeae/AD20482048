using UnityEngine;
using System.Collections.Generic;

public class PoolSystem : MonoBehaviour{
    int Cell_Maximum = 17;
    public static GameObject EmptyCell;
    public static GameObject NormalCell;
    public static GameObject DeathCell;
    public static GameObject NuclearCell;
    public static GameObject DoubleCell;
    public static GameObject LockCell;    
    public static GameObject HealCell;
    public static GameObject BuffCell;
    public GameObject LineGaroCell;
    
    
    public static GameObject FeverCell;
    public static GameObject LineSeroCell;
    public static GameObject UnLockCell;
    public Stack<GameObject> CellStack;    
    private void Start() {
        CellStack = new Stack<GameObject>();
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

    public GameObject Generate(GameObject Object, Vector3 pos, Quaternion rot)   {
        GameObject obj;
        if (CellStack.Count == 0) {
            obj = GameObject.Instantiate(Object, pos, rot);
        }
        else  {
            obj = CellStack.Pop();  //스택에서, 가장 최신에 입력한 데이터를 차례로 가져오는 함수 - 반대는 Push
            if (obj == null) return Generate(Object, pos, rot);
        }                
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(true);
        return obj;
    }

    public void DeleteCell(GameObject obj) {
        CellStack.Push(obj);
        obj.SetActive(false);
    }
}	
