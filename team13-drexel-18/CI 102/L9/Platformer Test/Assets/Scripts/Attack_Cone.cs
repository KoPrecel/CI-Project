using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Cone : MonoBehaviour {

    public TurretAI turretAI;

    public bool isLeft = false; 


    void Awake()
    {
        turretAI = gameObject.GetComponentInParent<TurretAI>(); 
    }

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.CompareTag("Player"))
        {
            if (isLeft)
            {
                turretAI.Attack(false);  //look at the bool inside partenthese it is saing attacking right 
            }
            else
            {
                turretAI.Attack(true);
            }
        }

    }
}
