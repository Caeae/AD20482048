using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCell : Cell{
    bool CanGenerate = false;      
    float HealthPerCent = 100f;
    void Start() {
        IsSpecial = true;
    }
    void Update()    {
        cell.text = value.ToString();
        if (GameManager.Health <= 30) {
            CanGenerate = true;
            HealthPerCent = HealthPerCent - (30 - GameManager.Health) / 3;            
        }
        else CanGenerate = false;
        if (Activation) {
            GameManager.Health = GameManager.Health * 0.8f;
            Enemy.EnemyHealthRe(GameManager.Health / 100);
            Activation = false;
        }
    }
}
