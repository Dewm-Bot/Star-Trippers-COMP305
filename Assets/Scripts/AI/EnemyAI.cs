using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyAI : MonoBehaviour
{
    
    public List<Transform> points;
    public int nextID;
    int idChangeValue = 1;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private LayerMask playerTag;
    [SerializeField]
    private Transform headPos;
    [SerializeField]
    private bool flipSprite;
    private int flippy1;
    private int flippy2;
    [SerializeField]
    private float hurtSphereSize = 0.2f;
    [SerializeField]
    private double health;
    [SerializeField]
    private double maxHealth = 100;

    public void Reset()
    {
        Init();
    }

    public void Init()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        GameObject root = new GameObject(name + "_root");
        root.transform.position = transform.position;
        transform.SetParent(root.transform);

        GameObject waypoints = new GameObject("Waypoints");
        waypoints.transform.SetParent(root.transform);

        GameObject p1 = new GameObject("Point 1");
        p1.transform.SetParent (waypoints.transform);
        p1.transform.position = root.transform.position;

        GameObject p2 = new GameObject("Point 2");
        p2.transform.SetParent(waypoints.transform);
        p2.transform.position = root.transform.position;


        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add(p2.transform);

    }

    public void Update()
    {
       NextWaypoint();
        if (HeadHit() == true) //Destroy ourselves on head hit!
        {
            die();
        }
    }

    public void Start()
    {
        if (flipSprite == true) //This just checks if the sprite needs to be flipped via script and flips it's values.
        {
            flippy1 = -1;
            flippy2 = 1;
        }
        else
        {
            flippy1 = 1;
            flippy2 = -1;
        }
        health = maxHealth;
    }

    public void TakeDamage(int damage) //This is for projectile damage
    {
        health -= damage;
        if (health <= 0)
        {
            die();
        }
    }

    private bool HeadHit() //Check if our head has been hit!
    {
        return Physics2D.OverlapCircle(headPos.transform.position, hurtSphereSize, playerTag);
    }

    void NextWaypoint()
    {
        Transform goalPoint = points[nextID];
        if (goalPoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(flippy1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(flippy2, 1, 1);
        }
        transform.position = Vector2.MoveTowards(transform.position,goalPoint.position,speed*Time.deltaTime);
        if (Vector2.Distance(transform.position, goalPoint.position) < 1f)
        {
            if (nextID == points.Count -1)
            {
                idChangeValue = -1;
            }
            if (nextID == 0)
            {
                idChangeValue = 1;
            }
            nextID += idChangeValue;
        }
    }
    private void die()
    {
        Destroy(gameObject); //We can change this to do something fancier later.
    }
}
