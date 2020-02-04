using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour {

    public int dmg = 100;

    void OnTriggerEnter2D(Collider2D col)
    { 

        if (col.isTrigger != true && !col.CompareTag("Player"))
        {
            if (col.CompareTag("Enemy")){
                col.SendMessageUpwards("Damage", dmg); //Trying to call a function with whatever we are colliding with 

            }
            Destroy(gameObject);

        }

    }
}
