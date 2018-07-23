using UnityEngine;
using System.Collections.Generic;

public class PoolSystem : MonoBehaviour{
    const int Cell_Maximum = 17;
    public Stack<GameObject> CellStack;
    public GameObject ObjCell;
    private void Start()
    {
        CellStack = new Stack<GameObject>();
        ObjCell = GameObject.Find("Cell");
    }
    public GameObject Generate(GameObject ObjCell, Vector3 pos, Quaternion rot) {
			GameObject obj;
			if(CellStack.Count==0) {
				obj = GameObject.Instantiate(ObjCell, pos, rot);				
			}
			else {
				obj = CellStack.Pop();	//스택에서, 가장 최신에 입력한 데이터를 차례로 가져오는 함수 - 반대는 Push
				if(obj == null) {
					return Generate(ObjCell,pos, rot);
				}
			}		
			obj.transform.position = pos;
			obj.transform.rotation = rot;
			obj.SetActive(true);
			return obj;	
		}
		public void DeleteCell(GameObject obj) {
			obj.SetActive(false);
			CellStack.Push(obj);
		}
	}	
