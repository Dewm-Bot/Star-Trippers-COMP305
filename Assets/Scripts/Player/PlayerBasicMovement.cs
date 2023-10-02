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

        if (Input.GetButtonDown("Horizontal"))
        {
            playerRb.AddForce(new Vector2(playerPushForce * horizontalMovement, 0));
            lastDirection = horizontalMovement;
        }
        if (Input.GetButtonDown("Jump") && playerCanJump)
        {
            playerRb.AddForce(new Vector2(0, playerJumpForce));
            playerCanJump = false;
        }

        playerRb.velocity = new Vector2(lastDirection * 1.5f, playerRb.velocity.y);

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
        }
    }
}
