using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour {

    //Integers
    public int curHealth;
    public int maxHealth;

    //Floats 
    public float distance; 
    public float wakeRange;
    public float shootInterval;
    public float bulletSpeed = 100;
    public float bulletTimer;

    //Booleans 
    public bool awake = false;
    public bool lookingRight = true;

    //References
    public GameObject bullet;
    public Transform target;
    public Animator anim;
    public Transform shootPointLeft, shootPointRight; //comma can create two variables of the same type 

    void Awake()
    {

        anim = gameObject.GetComponent<Animator>(); 

    }

    void Start()
    {
        curHealth = maxHealth;
    }

    void Update()
    {

        anim.SetBool("Awake", awake);
        anim.SetBool("LookingRight", lookingRight);

        RangeCheck();

        if(target.transform.position.x > transform.position.x)
        {
            lookingRight = true;
        }

        if(target.transform.position.x < transform.position.x)
        {
            lookingRight = false;
        }

        if(curHealth <= 0)
        {
            Destroy(gameObject);
        }


    }

    void RangeCheck()
    {
        distance = Vector3.Distance(transform.position, target.transform.position); //Vector3 Distance, transform.position is the position of the turret, target.transfrom is positioin of player

        if(distance < wakeRange)
        {
            awake = true; 
        }

        if(distance > wakeRange)
        {
            awake = false; 
        }
    }

    public void Attack(bool attackingRight)
    {
        bulletTimer += Time.deltaTime;  //increases by one every second

        if(bulletTimer >= shootInterval)
        {
            Vector2 direction = target.transform.position - transform.position; //returns direction
            direction.Normalize(); //idk what this does lol 

            if (!attackingRight)
            {
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointLeft.transform.position, shootPointLeft.transform.rotation) as GameObject; //instatiate is creating the bullet at this position as a gmae object
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed; //set the speed to bulletSPeed 

                bulletTimer = 0; //Resets time 

            }

            if (attackingRight)
            {
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointRight.transform.position, shootPointRight.transform.rotation) as GameObject; //instatiate is creating the bullet at this position as a gmae object
                bulletClone.transform.rotation = Quaternion.FromToRotation(Vector3.left, direction); //Rotates Bullet To face Player
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed; //set the speed to bulletSPeed 

                bulletTimer = 0; //Resets time 
            }
        }
    }

    public void Damage(int damage)
    {
        curHealth -= damage;
        gameObject.GetComponent<Animation>().Play("Player_RedFlash");

    }



}
