using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossTreeScript : MonoBehaviour
{

    private Vector2 dir = new Vector2(-1, 0); //use one if facing right
    private Animator anim;
    private Rigidbody2D rb2d;
    public int curHealth;
    public int maxHealth;
    private Player player;

    private Transform target;

    public Transform hitpos1;
    public Transform hitpos2;
    public Transform hitpos3;
    public Transform hitpos4;
    public Transform spawnpos;

    public int hitTimerSet;
    private int hitTimer;

    public GameObject spikes;
    public GameObject mush;
    public Transform overPlayer;
    public int timerSet;
    private int playerTimer;
    private int noFirst = 0;
    public int spawnTimerSet;
    private int spawnTimer;

    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {

        // anim.SetBool("moving", true);
        // target = GameObject.FindWithTag("Player").GetComponent<Transform>(); // Has to be FindWithTag rather than FindObject with tag because it's a singular instance
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        curHealth = maxHealth;
        playerTimer = timerSet;
        hitTimer = -1;
    }

    // Update is called once per frame
    void Update()
    {
        hitTimer = hitTimer - 1;
        if (hitTimer == 0)
        {
            Instantiate(spikes, hitpos1.position, hitpos1.rotation);
            Instantiate(spikes, hitpos2.position, hitpos2.rotation);
            Instantiate(spikes, hitpos3.position, hitpos3.rotation);
            Instantiate(spikes, hitpos4.position, hitpos4.rotation);
            spawnTimer = spawnTimerSet;
            hitTimer = -1;
        }
        spawnTimer = spawnTimer - 1;
        if (spawnTimer == 0)
        {
            Instantiate(mush, spawnpos.position, spawnpos.rotation);
        }
        playerTimer = playerTimer - 1;
        if (playerTimer <= 0)
        {
            if (noFirst == 0)
            {
                noFirst = noFirst + 1;
            }
            else
            {
                Instantiate(spikes, overPlayer.position, overPlayer.rotation);
            }
            playerTimer = timerSet;
        }



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
        if (hitTimer < 0)
        {
            hitTimer = hitTimerSet;
        }

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
        /*if (col.CompareTag("Block"))
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

        }*/
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
