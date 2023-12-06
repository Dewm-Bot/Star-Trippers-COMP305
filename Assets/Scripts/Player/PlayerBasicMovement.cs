using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerBasicMovement : MonoBehaviour
{
    //physics stuff
    private Rigidbody2D playerRb;
    //private float playerPushForce = 450.0f; Currently unused?
    [SerializeField]
    private float playerJumpForce = 200f;
    public float worldGravity = 5.0f;
    public bool playerCanJump = true; //Needs to be public for GroundCheck to work. Should we integrate it into this script?

    //boost stuff
    [SerializeField]
    private float boostDelay = 0.5f; //Delay until boost can be applied again
    [SerializeField]
    private float boostSpeed = 3.0f; //Boost speed that will be set to BoostSpeedSet (cannot be applied to boostapply directly for some reason)
    private float boostApply = 1.5f; //Speed that will be applied to the player. Starts at base speed, then is set to boostSpeed
    private float boostTimer = 0.0f;

    //movement stuff
    private float horizontalMovement = 0f;
    private float lastDirection = 0f;
    private bool isFacingRight = true;


    //health stuff
    [SerializeField]
    private double maxHealth = 100;
    public double playerHealth = 100;

    //gun stuff
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletSpeed = 10f;
    public bool hasGun = false;
    private float spawnDistance = 0.6f; //Spawn buffer distance for our bullet

    //animator stuff
    Animator anim;
    private bool isDead = false; //send if the player is dead to animator

    //Score and Lives Stuff
    [SerializeField]
    private int maxLives = 3;
    private int lives = 3;
    public int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerHealth = maxHealth;
        lives = maxLives;
    }

    //Updates at fixed time intervals, good for consistency
    private void FixedUpdate()
    {
        //animator binds
        anim.SetFloat("speed", Mathf.Abs(playerRb.velocity.x));
        anim.SetFloat("jumping", Mathf.Abs(playerRb.velocity.y));
        anim.SetBool("dead", isDead);
        anim.SetBool("hasGun", hasGun);
    }

    // Update is called once per frame
    void Update()
    {
 
        Flip();
        if (playerHealth <= 0)
        {
            isDead = true;
            playerRb.velocity = Vector2.zero;
            Invoke("Dead", 2.0f);
        }

        horizontalMovement = Input.GetAxisRaw("Horizontal");
        boostTimer += Time.deltaTime;
        //Debug.Log(boostApply);
        if (Input.GetButtonDown("Horizontal") && !isDead)
        {
            //playerRb.AddForce(new Vector2(playerPushForce * horizontalMovement, 0));
            lastDirection = horizontalMovement;
        }
        if (Input.GetButtonDown("Jump") && playerCanJump && !isDead)
        {
            playerRb.AddForce(new Vector2(0, playerJumpForce));
            playerCanJump = false;
        }
        if (boostApply > 1.5f) //make sure the 1.5 is the base speed you want, also change the boostApply private var to preffered value
        {
            boostApply -= 0.001f; //decays boostApply slowly if it is above base speed value
        }
        playerRb.velocity = new Vector2(lastDirection * boostApply, playerRb.velocity.y);

        if (Input.GetButtonDown("Fire1") && hasGun)
        {
            Shoot();
        }
        if (Input.GetKey(KeyCode.Escape)) 
        {
            Application.Quit();
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontalMovement < 0f || !isFacingRight && horizontalMovement > 0f && !isDead) 
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            Boost(); //applies boost on direction switch.
        }

    }
    private void Boost()
    {
        float boostSpeedSet = boostSpeed; //Applies wanted value to boostSpeedSet (I have no idea why this is needed, but it breaks without)
        //Debug.Log(boostSpeedSet);
        if (boostTimer > boostDelay)
        {
            boostApply = boostSpeedSet; 
            boostTimer = 0.0f; //set boost speed and reset timer
        }
        
    }

    private void Dead()
    {
        lives--;
        if (lives < 0)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        else
        {
            Scene scene = SceneManager.GetSceneAt(1);
            SceneManager.LoadScene(scene.name);
        }
        
    }

    void Shoot()
    {
        if (isFacingRight) //fire our projectile right
        {
            Vector3 spawnPosition = transform.position + transform.right * spawnDistance;
            GameObject projectile = Instantiate(bulletPrefab, spawnPosition, transform.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>(); //Grab the physics of our bullet
            rb.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse); //Fire away!
        }
        else //negative values fire it to the left
        {
            Vector3 spawnPosition = transform.position - transform.right * spawnDistance;
            GameObject projectile = Instantiate(bulletPrefab, spawnPosition, transform.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>(); //Grab the physics of our bullet
            rb.AddForce(-transform.right * bulletSpeed, ForceMode2D.Impulse); //Fire away!
        }
        
    }
}
