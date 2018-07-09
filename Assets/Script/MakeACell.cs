using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeACell : MonoBehaviour {
    int a = 1;
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
        GameObject obj;
        switch (a) {
            case 1:
                obj = Instantiate(NormalCell, Where, transform.rotation);
                obj.GetComponent<Cell>().value = c;
                break;
            case 2:
                obj = Instantiate(NuclearCell, Where, transform.rotation);
                obj.GetComponent<Cell>().value = c;
                break;
            case 3:
                obj = Instantiate(DeathCell, Where, transform.rotation);
                obj.GetComponent<Cell>().value = c;
                break;
            case 4:
                obj = Instantiate(DoubleCell, Where, transform.rotation);
                obj.GetComponent<Cell>().value = c;
                break;
            case 5:
                obj = Instantiate(LockCell, Where, transform.rotation);
                obj.GetComponent<Cell>().value = c;
                break;
            case 6:
                obj = Instantiate(HealCell, Where, transform.rotation);
                obj.GetComponent<Cell>().value = c;
                break;
            case 7:
                obj = Instantiate(FeverCell, Where, transform.rotation);
                obj.GetComponent<Cell>().value = c;
                break;
            case 8:
                obj = Instantiate(WarpCell, Where, transform.rotation);
                obj.GetComponent<Cell>().value = c;
                break;
            case 9:
                obj = Instantiate(LineGaroCell, Where, transform.rotation);
                obj.GetComponent<Cell>().value = c;
                break;
            default:
                break;
        }
        
        
    }
}
