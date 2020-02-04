using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor.SceneManagement;

public class Player : MonoBehaviour {

    //Floats
    public float maxSpeed = 3;
    public float speed = 50f;
    public float jumpPower = 250f;
    public int Health = 0;
    public float maxFallTime = 3;
    float currentFallTime;

    //Booleans
    public bool grounded;
    public bool canDoubleJump;
    bool falling;

    //Stats
    public int curHealth;
    public int maxHealth = 100; 

    //References
    private Rigidbody2D rb2d;
    private Animator anim;


	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        curHealth = maxHealth;
        
	}


        // Update is called once per frame
        void Update () {

        //Now speed variable is set to the actual speed of the player rb2d.velocity.x
        anim.SetBool("Grounded",grounded);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

        if(Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                rb2d.AddForce(Vector2.up * jumpPower);
                canDoubleJump = true; 

            }
            else
            {

                if(canDoubleJump)
                {
                    canDoubleJump = false;

                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0); //Not moving on the y axis in the air so can add extra force and double jump. Think back to our glitch where we could fly it stops that type of momentum 
                    rb2d.AddForce(Vector2.up * jumpPower / 1.75f); // Actual Jumping * Divided by 1.75 to reduce jumping power
                }

            }

        }

        if (grounded)
        {
            speed = 75f;
        }
        else
        {
            speed = 50f;
        }

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth; 
        }
        if (curHealth <= 0)
        {
            Die ();
        }

        //Min Dye 3/14/18 If you fall for too long, you die
        if (grounded)
        {
            falling = false;
            currentFallTime = 0;
        }
        else falling = true;

        if (falling)
        {
            currentFallTime += Time.deltaTime;
        }
        if (currentFallTime >= maxFallTime)
        {
            Die();
        }

    }

    void FixedUpdate()
    {
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y; //Doesn't affect the y velocity of the player
        easeVelocity.z = 0.0f; //Don't use z axis 2d game duh
        easeVelocity.x *= 0.5f; //Multiplies easeVelocity.x * .75 which reduces the x velocity .75 is original value
     

        float h = Input.GetAxis("Horizontal");

        //Fake friction or rather easing the x speed of the player 
        if (grounded)
        {
            rb2d.velocity = easeVelocity;  //Just setting velocity to ease velocity
        }

        //Moving the Player
        rb2d.AddForce((Vector2.right * speed) * h);

        //If velocity is over max speed sets velocity to max speed, simply limits the speed of the player
        if(rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);

        }

        if(rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
        
    }

    void Die()
    {
        //Right Now when we die, we just restart the scene in which we are currently in asfdsdsasdfasdfa
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().buildIndex);
    }

    public void Damage(int dmg)
    {

        curHealth -= dmg;
        gameObject.GetComponent<Animation>().Play("Player_RedFlash");
    }

    public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir)
    {

        float timer = 0; 

        while( knockDur > timer)
        {
            timer += Time.deltaTime; //Just counts in seconds 

            rb2d.AddForce(new Vector3(knockbackDir.x * -100, knockbackDir.y + knockbackPwr, transform.position.z));  //knockbacked opposite way in the x axis 
                   //Do KNockbackdir.y + pwr for more realistic * for functioning                                                                                                  //the execution of a coroutine can be paused at any point using the yield statement. 
                                                                                                                     //The yield return value specifies when the coroutine is resumed. Coroutines are excellent when modelling behaviour over several frames. 
                                                                                                                     //Coroutines have virtually no performance overhead. StartCoroutine function always returns immediately, 
                                                                                                                    //however you can yield the result. This will wait until the coroutine has finished execution. There is no guarantee that coroutines end in the same order that they were started, even if they finish in the same frame.
        }

        yield return 0;

    }


}
