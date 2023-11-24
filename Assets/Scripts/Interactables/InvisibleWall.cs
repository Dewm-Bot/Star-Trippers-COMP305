using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    [SerializeField]
    public float offset = 20f;
    Vector2 newpos;

    private void OnCollisionEnter2D (Collision2D coll)
    {
        GameObject player = coll.gameObject;
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        newpos = new Vector2(rb.position.x + offset, rb.position.y);
        rb.transform.position = newpos;
    }
}
