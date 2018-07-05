using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Remain : MonoBehaviour {
    public Text Cex;
    public Text Turn;
    public Text Score;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Cex.text = GameManager.RestCellleft.ToString();
        Turn.text = GameManager.turn.ToString();
        Score.text = GameManager.Byte.ToString();
    }

    
}
