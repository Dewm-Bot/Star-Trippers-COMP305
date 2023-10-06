using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBasicMovement : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float playerPushForce = 450.0f;
    public float playerJumpForce = 200f;
    public float worldGravity = 5.0f;
    public bool playerCanJump = true;
    public float boostDelay = 0.5f; //Delay until boost can be applied again
    public float boostSpeed = 3.0f; //Boost speed that will be set to BoostSpeedSet (cannot be applied to boostapply directly for some reason)

    private float boostApply = 1.5f; //Speed that will be applied to the player. Starts at base speed, then is set to boostSpeed
    private float boostTimer = 0.0f;
    private float horizontalMovement = 0f;
    private float lastDirection = 0f;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        boostTimer += Time.deltaTime;
        //Debug.Log(boostApply);
        if (Input.GetButtonDown("Horizontal"))
        {
            //playerRb.AddForce(new Vector2(playerPushForce * horizontalMovement, 0));
            lastDirection = horizontalMovement;
        }
        if (Input.GetButtonDown("Jump") && playerCanJump)
        {
            playerRb.AddForce(new Vector2(0, playerJumpForce));
            playerCanJump = false;
        }
        if (boostApply > 1.5f) //make sure the 1.5 is the base speed you want, also change the boostApply private var to preffered value
        {
            boostApply -= 0.001f; //decays boostApply slowly if it is above base speed value
        }

        playerRb.velocity = new Vector2(lastDirection * boostApply, playerRb.velocity.y);

        Flip();
    }

    private void Flip()
    {
        if (isFacingRight && horizontalMovement < 0f || !isFacingRight && horizontalMovement > 0f)
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
}
