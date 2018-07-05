using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;



public class Button : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ButtonYes()
    {
        SceneManager.LoadScene("game");
    }

    public void ButtonNo()
    {
        Application.Quit();
    }
}
