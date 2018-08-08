using UnityEngine;
using System.Collections.Generic;

public class PoolSystem : MonoBehaviour{
    int Cell_Maximum = 17;
    public Stack<GameObject> CellStack;    
    private void Start() {
        CellStack = new Stack<GameObject>();
    }

    public GameObject Generate(GameObject Object, Vector3 pos, Quaternion rot)   {
        GameObject obj;
        if (CellStack.Count == 0) {
            obj = GameObject.Instantiate(Object, pos, rot);
        }
        else if (!CellStack.Contains(Object)) {
            obj = GameObject.Instantiate(Object, pos, rot);            
        }
        else {
            obj = CellStack.Pop();  //스택에서, 가장 최신에 입력한 데이터를 차례로 가져오는 함수 - 반대는 Push
            if (obj == null) return Generate(Object, pos, rot);
        }                
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(true);
        //GameManager.Cellsis.Add(obj);
        return obj;
    }

    public void DeleteCell(GameObject obj) {
        CellStack.Push(obj);
        GameManager.Cellsis.Remove(obj);
        obj.SetActive(false);
    }
}	
