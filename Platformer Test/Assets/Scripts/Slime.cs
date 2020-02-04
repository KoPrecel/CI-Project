using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public int curHealth;
    public int maxHealth;
    public bool moving;

    private Player player;
    private Rigidbody2D rb2d;
    private Animator anim;
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
            anim.SetBool("moving", true);
        }

        if ((rb2d.velocity.x != 0))
        {
            moving = true;
            anim.SetBool("moving", true);
        }

        else
        {
            moving = false;
        }


        if ((target.position.x < transform.position.x && transform.localScale.x == 1) || (target.position.x > transform.position.x && transform.localScale.x == -1))
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }


        // Slime dies if its health is less than 0
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
                col.GetComponent<Player>().Damage(2);
                //col.GetComponent<Player>().Knockback(0.02f, 500, 0);
            }
        }
    }


    void OnTriggerStay2D(Collider2D col)
    {
        if (col.isTrigger != true)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<Player>().Damage(1);
            }
        }
    }



    // Disables input for the specified duration.
    // TODO: Is there a way to enable input when (disableDuration == 0 and grounded)?
    /*public void DisableInput(float disableDuration)
    {
        inputDisabled = true;
        h = 0;
        Invoke("EnableInput", disableDuration);
    }*/
    // Knockback function
    // Disables input first, then calls for the knockback push
    public void Knockback(Vector2 tempVector) //, float inputDisabledDuration
    {
        //DisableInput(inputDisabledDuration);
        float knockDur = tempVector.x;
        float knockbackPwr = tempVector.y;
        StartCoroutine(KnockbackPush(knockDur, knockbackPwr, transform.localScale));
    }

    // Knockback push function
    // Pushes player back for time specified by knockDuration, with power specified by knockbackPower
    public IEnumerator KnockbackPush(float knockbackDuration, float knockbackPower, Vector3 knockbackDir)
    {
        float timer = 0;// Timer since the function started

        rb2d.velocity = new Vector2(rb2d.velocity.x, 0); // Set the current y-velocity to 0

        while (knockbackDuration > timer)
        {
            timer += Time.deltaTime; // Add time to timer each frame

            // Knocks the player back opposite direction to where he's facing with constant power (knockbackDir is always 1 or -1; 1*knockbackPwr = constant knockbackPwr)
            // Knocks the player up with a constant value
            // z position stays the same
            rb2d.AddForce(new Vector3(-knockbackDir.x * knockbackPower, knockbackDir.y + knockbackPower, transform.position.z));
        }
        yield return null; // return nothing
    }
}

 
