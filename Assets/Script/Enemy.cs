using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    public static float EnemyHealth = 4352f;
    public static float ConstHealth = 4352;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        EnemyHealth = ConstHealth - GameManager.Byte;        
	}
    public static void EnemyHealthRe(float a)
    {
        EnemyHealth=a;
    }
    public bool EnemyDeath() {
        if (EnemyHealth <= 0) return true;
        else return false;       
    }
}
