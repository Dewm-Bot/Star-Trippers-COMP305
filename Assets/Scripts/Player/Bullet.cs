using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int damage = 50;
    [SerializeField]
    private float lifetime = 3000.0f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject objecthit = collision.gameObject; //Checks if we hit something
        EnemyAI enemy = objecthit.GetComponent<EnemyAI>(); //Does the thing we hit have an enemy script?


        if (enemy != null) //if it does, apply that damage!
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
