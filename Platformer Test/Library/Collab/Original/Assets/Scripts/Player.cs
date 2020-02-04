using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {


    // Floats
    public float maxSpeedX = 3;
    public float speed = 50f;
    public float jumpPower = 150f;
    private float invincibleTimer;
    public float ballForce;
    public float h;
    public float maxSpeedY = 15;

    public float knockback;
    public float knockbackCount;
    public float knockbackLength;
    public float manaTimer;
    public float dashRightInputTimer;
    public float dashLeftInputTimer;
    public float dashCooldown;
    public float dashSpeedModifier = 1;
    public float dashDuration;

    // Integers
    public int jumpCount;
    public int downCount;

    // Booleans
    public bool grounded;
    public bool canDoubleJump;
    public bool fallDamageBool;
    public bool playerInvincible = false;
    public bool faceRight;
    public bool magicUnlock = false;
    public bool inputDisabled = false;
    public bool knockFromRight;
    public bool rustSpearUnlock = false;
    public bool dashBoolRight = false;
    public bool dashBoolLeft = false;
    public bool dashing = false;


    // Stats
    public int curHealth;
    public int maxHealth = 20;
    public float curMana;
    public float maxMana = 25f;
    public float manaCost = .25f;


    // References
    private Rigidbody2D rb2d;
    private Animator anim;
    public GameObject energyball;
    public Slider manaSlider; 


	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        curMana = maxMana;
    }


    // Update is called once per frame
    void Update () {
        // Invincible frame function
        invincibleTimer = invincibleTimer - 1;

        if (invincibleTimer > 0)
        {
            playerInvincible = true;
        }

        else
        {
            playerInvincible = false;
        }


        // If player is flying/falling above/below certain y coordinate, he dies.
        if (!grounded && (rb2d.position.y < -50 || rb2d.position.y > 50))
        {
            Die();
        }


        // Air resistence
        if (grounded)
        {
            speed = 250f; 
        }

        else
        {
            speed = 100f;
        }


        // Now speed variable is set to the actual speed of the player rb2d.velocity.x
        anim.SetBool("Grounded",grounded);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));


        // Makes the character face left or right depending on the input
        if (!inputDisabled)
        {
            if (InputManager.Left() && InputManager.Right())
            {
                //do nothing
            }
            else if (InputManager.Left())
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceRight = false;
            }

            else if (InputManager.Right())
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceRight = true;
            }


            // Down function
            if (InputManager.Down())
            {
                if (!grounded && (downCount == 0))
                {
                    rb2d.AddForce(Vector2.down * jumpPower);
                    downCount += 1;
                }
            }


            // Jump function
            if (InputManager.Jump())
            {
                if (grounded)
                {
                    rb2d.AddForce(Vector2.up * jumpPower);
                    jumpCount += 1;
                }

                else if (jumpCount < 1)
                {
                    jumpCount += 1;
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0); //Not moving on the y axis in the air so can add extra force and double jump. Think back to our glitch where we could fly it stops that type of momentum 
                    rb2d.AddForce(Vector2.up * jumpPower); // Actual Jumping * Divided by 1.75 to reduce jumping power
                }
            }



            // Dash function
            if (dashRightInputTimer <= 0)
            {
                dashBoolRight = false;
            }
            if (dashLeftInputTimer <= 0)
            {
                dashBoolLeft = false;
            }

            if (dashBoolRight && InputManager.RightKeyDown() && dashCooldown <= 0)
            {
                dashDuration = .33f;
                dashing = true;
            }
            if (dashBoolLeft && InputManager.LeftKeyDown() && dashCooldown <= 0)
            {
                dashDuration = .33f;
                dashing = true;
            }

            if (dashing)
            {
                dashSpeedModifier = 2.5f;
                maxSpeedX = 7.5f;
                dashCooldown = 4;
                dashBoolRight = false;
                dashBoolLeft = false;
            }
            else if (InputManager.RightKeyDown())
            {
                dashBoolRight = true;
                dashRightInputTimer = 0.5f;
                dashLeftInputTimer = 0;
            }
            else if (InputManager.LeftKeyDown())
            {
                dashBoolLeft = true;
                dashLeftInputTimer = 0.5f;
                dashRightInputTimer = 0;
            }

            if (dashDuration > 0)
            {
                dashDuration -= Time.deltaTime;
            }
            else
            {
                dashing = false;
                maxSpeedX = 3;
                dashSpeedModifier = 1;
            }

            if (dashRightInputTimer > 0)
            {
                dashRightInputTimer -= Time.deltaTime;
            }
            if (dashLeftInputTimer > 0)
            {
                dashLeftInputTimer -= Time.deltaTime;
            }

            if (dashCooldown > 0)
            {
                dashCooldown -= Time.deltaTime;
            }
            // Dash function
            //public float dashInputTimer;
            //public float dashCooldown;
            //public float dashSpeedModifier = 1;
            //public float dashDuration;
            //public bool dashBoolRight = false;
            //public bool dashBoolLeft = false;
            //public float dashDuration;
            /* Psuedo code
             * dashbool = true when left/right is clicked, run timer
             * dash function when dashbool is true and left/right is clicked within timer
             *  in the function, set speed modifier to 2
             *  max speed = 6
             *  dashbool = false
             *  dashcooldown
             * reset dashbool and maxspeed*/
        }
    


        if (grounded)
        {
            jumpCount = 0;
            downCount = 0;
        }


        // Health function
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth; 
        }
        if (curHealth <= 0)
        {
            Die();
        }


        // Magic function
        if(magicUnlock)
        {
            if (InputManager.Magic() && manaSlider.value >= manaCost)
            {
                GameObject newEnergyBall = Instantiate(energyball, transform.position, transform.rotation);

                if (faceRight)
                {
                    newEnergyBall.GetComponent<Rigidbody2D>().AddRelativeForce(newEnergyBall.transform.right * ballForce);
                    manaSlider.value -= manaCost;
                    curMana = manaSlider.value;

                }

                else
                {
                    newEnergyBall.GetComponent<Rigidbody2D>().AddRelativeForce(newEnergyBall.transform.right * -ballForce);
                    manaSlider.value -= manaCost;
                    curMana = manaSlider.value;
                }
            }

            if (manaSlider.value < maxMana)
            {
                manaTimer += Time.deltaTime;
            }
            if (manaTimer >= 3)
            {
                manaSlider.value += .25f;
                manaTimer = 0;
            }
        }
    }


    // TODO: ADD COMMENT
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("UnlockMagic"))
        {
            magicUnlock = true;
            Destroy(col);
        }

        if (col.CompareTag("RustSpear"))
        {
            rustSpearUnlock = true;
            Destroy(col);
        }
    }

    
    void FixedUpdate()
    {
        // Pseudo Friction
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y; //Doesn't affect the y velocity of the player
        easeVelocity.z = 0.0f; //z axis = 0
        easeVelocity.x *= 0.5f; //easeVelocity.x * .5 which reduces the x velocity to half of original value

        // Apply the new velocity 
        if (grounded)
        {
            rb2d.velocity = easeVelocity;
        }

        // if input is not disabled, player is able to change direction with left/right key
        if (!inputDisabled)
        {
            if (InputManager.Left() && InputManager.Right())
            {
                h = 0;
            }
            else if (InputManager.Left())
            {
                h = -1;
            }
            else if (InputManager.Right())
            {
                h = 1;
            }
            else
            {
                h = 0;
            }
        }


        rb2d.AddForce((Vector2.right * speed) * h);


        // X direction max speed function
        if (rb2d.velocity.x > maxSpeedX)
        {
            rb2d.velocity = new Vector2(maxSpeedX, rb2d.velocity.y);
        }
        if (rb2d.velocity.x < -maxSpeedX)
        {
            rb2d.velocity = new Vector2(-maxSpeedX, rb2d.velocity.y);
        }


        // Y direction max speed function
        if (rb2d.velocity.y > maxSpeedY)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, maxSpeedY);
        }
        if (rb2d.velocity.y < -maxSpeedY)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, -maxSpeedY);
        }
    }


    // Death function
    void Die()
    {
        // Restart the active scene and reset the health
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        curHealth = maxHealth;
    }


    // Enables input
    public void EnableInput()
    {
        inputDisabled = false;
    }


    // Disables input for the specified duration.
    // TODO: Is there a way to enable input when (disableDuration == 0 and grounded)?
    public void DisableInput(float disableDuration)
    {
        inputDisabled = true;
        h = 0;
        Invoke("EnableInput", disableDuration);
    }


    // Damage function for player
    public void Damage(int dmg)
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (invincibleTimer <= 0)
        {
            curHealth -= dmg;
            gameObject.GetComponent<Animation>().Play("Player_RedFlash");
            invincibleTimer = 50;
            audio.Play();
        }
    }


    // Knockback function
    // Disables input first, then calls for the knockback push
    public void Knockback(float knockDur, float knockbackPwr, float inputDisabledDuration)
    {
        DisableInput(inputDisabledDuration);
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
