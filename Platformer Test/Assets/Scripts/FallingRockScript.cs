using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockScript : MonoBehaviour {

    private Player player;


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Block"))
        {
            Destroy(gameObject);
        }
        if (col.CompareTag("Player"))
        {
            col.SendMessageUpwards("Damage", 2); //Trying to call a function with whatever we are colliding with 

            Destroy(gameObject);
        }
    }
}
