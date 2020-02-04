using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{

    public float speed;
    public float stoppingDistance;
    //References 
    private Rigidbody2D rb2d;
    private Animator anim;
    public int curHealth;
    public int maxHealth;
    private Player player;

    private Transform target;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>(); // Has to be FindWithTag rather than FindObject with tag because it's a singular instance
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        curHealth = maxHealth;


    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));

        if (Vector2.Distance(transform.position, target.position) < stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }


    }

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
                col.GetComponent<Player>().Damage(1);

            }
        }
    }
}
 
