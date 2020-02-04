using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
    //First player is the script player
    private Player player;

    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
    }
    
    void OnTriggerEnter2D(Collider2D col)//Gets Called when Trigger enters something

    {
        if (!col.isTrigger)
        {
            player.grounded = true;
        }

    }

    void OnTriggerStay2D(Collider2D col) //The OnTriggerStay2D function is a function that gets called when the trigger stays in something
    {
        if (!col.isTrigger)
        {
            player.grounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) //This is like a function gets called when Trigger exits something
    {
        if (!col.isTrigger)
        {
            player.grounded = false;
        }
    }
}

    
