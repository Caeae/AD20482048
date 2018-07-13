using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCell : Cell{
    public static bool CanGenerate = false;          
    void Start() {
        IsSpecial = true;
    }
    void Update()    {
        cell.text = value.ToString();
        if (Activation) {
            GameManager.Health = GameManager.Health * 0.8f;
            Enemy.EnemyHealthRe(GameManager.Health / 100);
            Activation = false;
        }
    }
}
