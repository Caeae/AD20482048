using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class DialogManager : MonoBehaviour {
    string SampleDialog = @"Assets\Dialog\Sample.txt";
    //string decodeString;
    string[] textValue;
    Text text;
    int i;

    // Use this for initialization
    void Start () {
        i = 0;
        text = GetComponent<Text>();
        //string convertTxt = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("contents"));
        textValue = System.IO.File.ReadAllLines(SampleDialog);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (i < textValue.Length) { 
            text.text = (i+1).ToString() + ". " +textValue[i];
            }
            else
            {
                text.text = "It`s over!";
            }
            i++;
        }
        
	}
}
