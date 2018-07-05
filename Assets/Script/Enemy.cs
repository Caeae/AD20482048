using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    float EnemyHealth = 2496f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //EnemyDeath();
        EnemyHealth = 2496 - GameManager.Byte;
        
	}

    public bool EnemyDeath() {
        if (EnemyHealth <= 0) return true;
        else return false;       
    }
}
