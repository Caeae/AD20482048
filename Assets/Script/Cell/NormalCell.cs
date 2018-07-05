using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCell : Cell {
	void Update () {
        cell.text = value.ToString();
        if (value == 2) GetComponent<SpriteRenderer>().material.color = new Color(0, 150, 150);
        else if (value == 4) GetComponent<SpriteRenderer>().material.color = Color.white;
        else if (value == 8) GetComponent<SpriteRenderer>().material.color = Color.gray;
        else if (value == 16) GetComponent<SpriteRenderer>().material.color = Color.green;
        else if (value == 32) GetComponent<SpriteRenderer>().material.color = Color.yellow;
        else if (value == 64) GetComponent<SpriteRenderer>().material.color = Color.cyan;
        else if (value == 128) GetComponent<SpriteRenderer>().material.color = Color.magenta;
        else if (value == 256) GetComponent<SpriteRenderer>().material.color = Color.blue;
        else if (value == 512) GetComponent<SpriteRenderer>().material.color = Color.red;
        else if (value == 1024) GetComponent<SpriteRenderer>().material.color = Color.gray;
        else if (value == 2048) GetComponent<SpriteRenderer>().material.color = Color.black;
    }
}
