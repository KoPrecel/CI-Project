using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour {

    private bool attacking = false;

    private float attackTimer = 0;
    public float attackCd = 0.40f;
    //private float attackCooldown; // TODO: Attack cooldown

    public Collider2D attackTrigger;
    public int weaponIndex;

    private Animator anim;

    public GameObject weaponStick;
    public GameObject weaponRustSpear;



    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        attackTrigger.enabled = false; //disabling the collider so not hitting everything
    }

    void Start()
    {
        


    }

    void Update()
    {


        GameObject WeaponHolder = GameObject.Find("WeaponHolder");
        WeaponSwitching weaponswitching = WeaponHolder.GetComponent<WeaponSwitching>();
        weaponIndex = weaponswitching.selectedWeapon;

        if (weaponIndex == 0)
        {
            attackTrigger =  weaponStick.GetComponent<Collider2D>();
        }

        else if (weaponIndex == 1)
        {
            attackTrigger = weaponRustSpear.GetComponent<Collider2D>();
        }




        if (InputManager.Attack() && !attacking) // TODO: Attack cooldown; && (attackCooldown <= 0)
        {
            attacking = true;
            attackTimer = attackCd;

            attackTrigger.enabled = true; 
        }

        if (attacking)
        {
            if(attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }

            else
            {
                attacking = false;
                attackTrigger.enabled = false;
                //attackCooldown = 0.5f; // TODO: Attack cooldown
            }
        }

        anim.SetBool("Attacking", attacking);
        /* TODO: Attack cooldown
        if(attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }*/
    }
}
