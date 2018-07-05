using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class TurnManager : MonoBehaviour {
    GameObject Restart;
    GameObject RestartButton;
    GameObject CellFull;
    GameManager GameSystem;
    int Undostack = 20;
    // Use this for initialization
    void Start () {
        Restart = GameObject.Find("Restart");
        RestartButton = GameObject.Find("RestartMain");
        CellFull = GameObject.Find("Full Cell");
        Restart.SetActive(false);
        RestartButton.SetActive(false);
        CellFull.SetActive(false);
        GameSystem = GameObject.Find("WholeSystem").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.sangtae == GameManager.State.GameOver)
        {
            Time.timeScale = 0;
            Restart.SetActive(true);
            RestartButton.SetActive(true);
        }
        else if (GameManager.sangtae == GameManager.State.Loaded)
        {
            //처음 시작할때 블록 1~2개 젠됨
            GameSystem.GenerateRandomCell();
            GameSystem.GenerateRandomCell();
            GameManager.sangtae = GameManager.State.WaitingForInput;
        }
        else if (GameManager.sangtae == GameManager.State.WaitingForInput)
        {
            #region 키입력 확인   
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("game");
                GameManager.sangtae = GameManager.State.Loaded;
                GameManager.Cellsis.Clear();
                GameManager.turn = 0;
                GameManager.Byte = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                GameManager.sangtae = GameManager.State.UndoFunction;
            }

        }
        #endregion
        else if (GameManager.sangtae == GameManager.State.CheckingMatches)
        {
            GameSystem.SpecificGen();
            if (GameSystem.CheckForMovesLeft())
            {
                GameSystem.ReadyCellsForUpgrading();
                GameManager.turn++;
                Undostack = 20;
                GameSystem.MoveCellSaveToForward();
                GameManager.sangtae = GameManager.State.WaitingForInput;

            }
            else
            {
                GameSystem.RemoveFullCell();
                GameManager.sangtae = GameManager.State.WaitingForInput;
            }
        }
        else if (GameManager.sangtae == GameManager.State.UndoFunction)
        {
            if (GameManager.turn > 1)
                if (Undostack > 0)
                {
                    Undostack--;
                    GameSystem.MoveCellLoadToBack();
                    GameManager.sangtae = GameManager.State.WaitingForInput;
                }
                else
                {
                    Debug.Log("20번 이상은 할 수 없습니다");
                    GameManager.sangtae = GameManager.State.WaitingForInput;
                }
        }        
    }
}
