using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBossAI : MonoBehaviour {

    public Transform originPoint;
    private Vector2 dir = new Vector2(-1, 0); //use one if facing right
    public float range;
    public float speed;
    private Animator anim;
    public float stoppingDistance;
    //References 
    private Rigidbody2D rb2d;
    public int curHealth;
    public int maxHealth;
    private Player player;
    public bool moving;
    public GameObject exitDoor;

    private Transform target;
    public int jumpTimer;
    private int jumpCount = 0;

    public GameObject spikes;
    public Transform rockpos1;
    public Transform rockpos2;
    public Transform rockpos3;
    public Transform rockpos4;

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        jumpTimer = 200;
        anim = gameObject.GetComponent<Animator>();

        // anim.SetBool("moving", true);
        // target = GameObject.FindWithTag("Player").GetComponent<Transform>(); // Has to be FindWithTag rather than FindObject with tag because it's a singular instance
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        curHealth = maxHealth;
        exitDoor.SetActive(true);
        PlayerPrefs.SetInt("Health", 20);


    }

    // Update is called once per frame
    void Update () {
        jumpTimer = jumpTimer - 1;
        Vector2 jump;
        jump.x = 800;
        jump.y = 1000;
        if (jumpTimer <= 0)
        {
            if (jumpCount == 0)
            {
                jump.x = -800;
                rb2d.AddForce(jump);
                jumpCount = 1;

            }
            else
            {
                rb2d.AddForce(jump);
                jumpCount = 0;
            }
            if(curHealth>= maxHealth / 3)
            {
                jumpTimer = 200;
            }
            else if(curHealth >= maxHealth / 4)
            {
                jumpTimer = 100;
            }
            else
            {
                jumpTimer = 100;
            }
        }
        if (jumpTimer <= 0)
        {
            //Code here for random rock falling
        }
        // anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));


        /*if (Vector2.Distance(transform.position, target.position) < stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            anim.SetBool("moving", true);

        }
        else
        {
            anim.SetBool("moving", false);
        }*/

        if (curHealth <= 0)
        {
            Destroy(gameObject);
            exitDoor.SetActive(false);
        }

        /* Debug.DrawRay(originPoint.position, dir * range);
        RaycastHit2D hit = Physics2D.Raycast(originPoint.position, dir, range);
        if (hit == true)
        {
            if (hit.collider.CompareTag("Block"))
            {
                Flip();
                speed *= -1;
                dir *= 1;
            }
        }
		//if checking for like a platform type 
        //if (hit == false)
        */
	}

    /*void FixedUpdate()
    {
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }*/
    public void Damage(int damage)
    {
        curHealth -= damage;
        gameObject.GetComponent<Animation>().Play("Player_RedFlash");

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<Player>().Damage(3);
                col.GetComponent<Player>().Knockback(0.02f, 500, 0.6f);
            }

        }
        if (col.CompareTag("Block"))
        {
            int pos = Random.Range(1, 4);
            if (pos == 1)
            {
                Instantiate(spikes, rockpos1.position, rockpos1.rotation);
                Instantiate(spikes, rockpos4.position, rockpos4.rotation);
            }
            else if (pos == 2)
            {
                Instantiate(spikes, rockpos2.position, rockpos2.rotation);
                Instantiate(spikes, rockpos3.position, rockpos3.rotation);
            }
            else
            {
                Instantiate(spikes, rockpos1.position, rockpos1.rotation);
                Instantiate(spikes, rockpos2.position, rockpos2.rotation);
                Instantiate(spikes, rockpos3.position, rockpos3.rotation);
                Instantiate(spikes, rockpos4.position, rockpos4.rotation);
            }

        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.isTrigger != true)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<Player>().Damage(3);
                //col.GetComponent<Player>().Knockback(0.02f, 500, 0.6f);
            }
        }
    }
}
