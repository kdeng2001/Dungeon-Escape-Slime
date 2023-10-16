using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeShot : MonoBehaviour
{
    [SerializeField] private float launchForce;
    private Rigidbody2D rb;
    public float xDirection;
    public float yDirection;
    public bool moving;

    private float spawnTime;
    [SerializeField] private float aliveTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        launchForce = 800f;
        aliveTime = 4f;
    }
    private void Update()
    {
        if(Time.time - spawnTime > aliveTime) { gameObject.SetActive(false); }
    }

    private void OnEnable()
    {
        spawnTime = Time.time;
        Debug.Log("yDirection: " + yDirection + " xDirection: " + xDirection);
        if(yDirection != 0 && !moving) 
        {
            rb.AddForce(new Vector2(0, launchForce * yDirection));
        }
        else if(yDirection != 0 && moving) 
        {
            rb.AddForce(new Vector2(0.75f * xDirection, 0.75f * yDirection) * launchForce);
        }
        else
        {
             rb.AddForce(new Vector2(launchForce * xDirection, 0f));
        }
       
        Debug.Log("adding force to slime shot");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) { return; }
        if(collision.gameObject.CompareTag("PlayerProjectile")) { return; }
        gameObject.SetActive(false);
    }
}
