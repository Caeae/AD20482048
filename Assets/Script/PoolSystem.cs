using UnityEngine;
using System.Collections.Generic;

public class PoolSystem : MonoBehaviour{
    GameObject DeathCell;
    GameObject NuclearCell;
    GameObject HealCell;
    GameObject LockCell;
    GameObject FeverCell;
    GameObject BuffCell;
    GameObject DoubleCell;
    GameObject ObjCell;
    public Stack<GameObject> CellStack;
    Stack<GameObject> DeathStack;
    Stack<GameObject> NukeStack;
    Stack<GameObject> HealStack;
    Stack<GameObject> LockStack;
    Stack<GameObject> DoubleStack;
    Stack<GameObject> FeverStack;
    Stack<GameObject> BuffStack;
    
    private void Start() {
        CellStack = new Stack<GameObject>();
        DeathStack = new Stack<GameObject>();
        NukeStack = new Stack<GameObject>();
        HealStack = new Stack<GameObject>();
        LockStack = new Stack<GameObject>();
        DoubleStack = new Stack<GameObject>();
        FeverStack = new Stack<GameObject>();
        BuffStack = new Stack<GameObject>();
        ObjCell = GameObject.Find("Cell");
        DeathCell = GameObject.Find("DeathCell");
        NuclearCell = GameObject.Find("NuclearCell");
        HealCell = GameObject.Find("HealCell");
        LockCell = GameObject.Find("LockCell");
        DoubleCell = GameObject.Find("DoubleCell");
        BuffCell = GameObject.Find("BuffCell");
        FeverCell = GameObject.Find("FeverCell");
    }    
    public GameObject Generate(Vector3 pos, Quaternion rot)
    {
        GameObject obj;
        if (CellStack.Count == 0) {
            obj = GameObject.Instantiate(ObjCell, pos, rot);
        }
        else  {
            obj = CellStack.Pop();  //스택에서, 가장 최신에 입력한 데이터를 차례로 가져오는 함수 - 반대는 Push
            if (obj == null) return Generate(pos, rot);
        }
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(true);
        GameManager.Cellsis.Add(obj);
        return obj;
    }

    public GameObject DeathGenerate(Vector3 pos, Quaternion rot)
    {
        GameObject obj;
        if (DeathStack.Count == 0) {
            obj = GameObject.Instantiate(DeathCell, pos, rot);
        }        
        else
        {
            obj = DeathStack.Pop();  //스택에서, 가장 최신에 입력한 데이터를 차례로 가져오는 함수 - 반대는 Push
            if (obj == null) return DeathGenerate(pos, rot);
        }
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(true);
        GameManager.Cellsis.Add(obj);
        return obj;
    }

    public GameObject NuclearGenerate(Vector3 pos, Quaternion rot)
    {
        GameObject obj;
        
        if (NukeStack.Count == 0)
        {
            obj = GameObject.Instantiate(NuclearCell, pos, rot);
        }        
        else        {
            obj = NukeStack.Pop();  //스택에서, 가장 최신에 입력한 데이터를 차례로 가져오는 함수 - 반대는 Push
            if (obj == null) return NuclearGenerate(pos, rot);
        }
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(true);
        GameManager.Cellsis.Add(obj);
        return obj;
    }

    public GameObject HealGenerate(Vector3 pos, Quaternion rot)
    {
        GameObject obj;
        if (HealStack.Count == 0)
        {
            obj = GameObject.Instantiate(HealCell, pos, rot);
        }
        else
        {
            obj = HealStack.Pop();  //스택에서, 가장 최신에 입력한 데이터를 차례로 가져오는 함수 - 반대는 Push
            if (obj == null) return HealGenerate(pos, rot);
        }
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(true);
        GameManager.Cellsis.Add(obj);
        return obj;
    }

    public GameObject LockGenerate(Vector3 pos, Quaternion rot)
    {
        GameObject obj;
        if (LockStack.Count == 0)
        {
            obj = GameObject.Instantiate(LockCell, pos, rot);
        }
        else
        {
            obj = LockStack.Pop();  //스택에서, 가장 최신에 입력한 데이터를 차례로 가져오는 함수 - 반대는 Push
            if (obj == null) return LockGenerate(pos, rot);
        }
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(true);
        GameManager.Cellsis.Add(obj);
        return obj;
    }

    public GameObject BuffGenerate(Vector3 pos, Quaternion rot)
    {
        GameObject obj;
        if (BuffStack.Count == 0)
        {
            obj = GameObject.Instantiate(BuffCell, pos, rot);
        }
        else
        {
            obj = BuffStack.Pop();  //스택에서, 가장 최신에 입력한 데이터를 차례로 가져오는 함수 - 반대는 Push
            if (obj == null) return BuffGenerate(pos, rot);
        }
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(true);
        GameManager.Cellsis.Add(obj);
        return obj;
    }

    public GameObject FeverGenerate(Vector3 pos, Quaternion rot)
    {
        GameObject obj;
        if (FeverStack.Count == 0)
        {
            obj = GameObject.Instantiate(FeverCell, pos, rot);
        }
        else
        {
            obj = FeverStack.Pop();
            if (obj == null) return FeverGenerate(pos, rot);
        }
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(true);
        GameManager.Cellsis.Add(obj);
        return obj;
    }

    public GameObject DoubleGenerate(Vector3 pos, Quaternion rot)
    {
        GameObject obj;
        if (DoubleStack.Count == 0) {
            obj = GameObject.Instantiate(DoubleCell, pos, rot);
        }
        else {
            obj = DoubleStack.Pop();
            if (obj == null) return DoubleGenerate(pos, rot);
        }
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(true);
        GameManager.Cellsis.Add(obj);
        return obj;
    }


    public void DeleteCell(GameObject obj) {
        if (obj.tag == "LockCell") { Debug.Log("LockPush"); LockStack.Push(obj); }
        else if (obj == NuclearCell)
        { Debug.Log("NukePush"); NukeStack.Push(obj); }
        else if (obj == DeathCell)
        { Debug.Log("DeathPush"); DeathStack.Push(obj); }
        else if (obj == HealCell)
        { Debug.Log("HealPush"); HealStack.Push(obj); }
        else if (obj == DoubleCell)
        { Debug.Log("DoublePush"); DoubleStack.Push(obj); }
        else if (obj == BuffCell)
        { Debug.Log("BuffPush"); BuffStack.Push(obj); }
        else if (obj == FeverCell)
        { Debug.Log("FeverPush"); FeverStack.Push(obj); }
        else CellStack.Push(obj);
        GameManager.Cellsis.Remove(obj);
        obj.SetActive(false);
    }
}	
