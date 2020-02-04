﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackTrigger : MonoBehaviour {

    public int dmg = 20;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.isTrigger != true && col.CompareTag("Enemy"))
        {
            col.SendMessageUpwards("Damage", dmg); //Trying to call a function with whatever we are colliding with 
            col.SendMessageUpwards("Knockback", new Vector2(.02f, 250f), SendMessageOptions.DontRequireReceiver);

        }


    }

}
