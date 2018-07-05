using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCell : Cell{
    bool CanGenerate = false;
    int PerCent = 0;
    float HealthPerCent = 100f;
    void Update()
    {
        if (GameManager.Health <= 30)
        {
            CanGenerate = true;
            HealthPerCent = HealthPerCent - (33 - GameManager.Health) / 3;
            if (HealthPerCent <= 90) HealthPerCent = 90;
        }
        else CanGenerate = false;
        if (Activation) GameManager.Health = GameManager.Health * 0.8f; Activation = false;
    }

    public void DeathCellGenerate()
    {
        PerCent = Random.Range(0, 99);
        if (CanGenerate && PerCent >= HealthPerCent)
        {
            //Instantiate(Cell);
        }
        
        
        //상대의 체력 = 상대체력 * GameManager.Health / 100;
    }
}