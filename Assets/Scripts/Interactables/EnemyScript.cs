using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private PlayerHealthTextScript PHTS;
    
    private float hurtdelay = 0f;
    [SerializeField]
    private float delayBetweenHits = 1f;
    [SerializeField]
    private double damage = 10;

    private void Awake()
    {
        PHTS = GameObject.Find("Canvas").GetComponent<PlayerHealthTextScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (hurtdelay > delayBetweenHits)
            {
                GameObject.Find("Player").GetComponent<PlayerHealthScript>().playerHealth -= damage;
                PHTS.UpdatePlayerHealthText();
                hurtdelay = 0;
            }
            
            
        }
    }

    private void FixedUpdate()
    {
        hurtdelay += Time.deltaTime;
    }
}
