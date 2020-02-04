using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealth : MonoBehaviour {

    public Player player;

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")){
            player.curHealth = 20;
        }
        
    }
}
