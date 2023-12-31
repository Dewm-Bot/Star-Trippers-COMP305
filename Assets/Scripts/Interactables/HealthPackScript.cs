using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackScript : MonoBehaviour
{
    private PlayerHealthTextScript PHTS;

    private void Awake()
    {
        PHTS = GameObject.Find("Canvas").GetComponent<PlayerHealthTextScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerBasicMovement>().playerHealth += 10;
            PHTS.UpdatePlayerHealthText();
            Destroy(this.gameObject);
        }
    }
}
