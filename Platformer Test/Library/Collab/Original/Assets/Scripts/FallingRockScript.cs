using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockScript : MonoBehaviour {

    private Vector2 dir = new Vector2(-1, 0);
    private Player player;
    //private Rigidbody2D rb2d;

    // Use this for initialization
    void Start () {
        Vector2 jump;
        jump.x = 0;
        jump.y = -500;
        //rb2d.AddForce(jump);
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 jump;
        jump.x = 0;
        jump.y = 500;
        //rb2d.AddForce(jump);
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
