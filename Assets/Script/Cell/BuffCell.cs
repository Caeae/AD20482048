using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCell : Cell {

	// Use this for initialization
	void Start () {
        IsSpecial = true;
	}
	
	// Update is called once per frame
	void Update () {
        cell.text = value.ToString();
        if (Activation)
        {

        }
    }
}
