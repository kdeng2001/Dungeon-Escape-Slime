using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitSlime : MonoBehaviour
{
    private PlayerBehavior pb;
    private Transform player;
    private Rigidbody2D rb;
    private Vector2 force;
    private float sign;
    private float spawnTime;
    private float duration;
    private void Awake()
    {
        
        player = GameObject.Find("Slime").transform;
        pb = player.GetComponent<PlayerBehavior>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        force = new Vector2(500f, 300f);
        duration = .2f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Time.time - spawnTime < duration) { return; }
        if (collision.gameObject.CompareTag("Player"))
        {
            Grow();
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if(Random.Range(0f,1f) < 0.5f) { sign = -1; }
        else { sign = 1; }
        Debug.Log("sign:" + sign);
        force = new Vector2(Random.Range(100f, 500f) * sign, Random.Range(500f, 800f));
        rb.AddForce(force);
        spawnTime = Time.time;
    }

    private void Grow()
    {
        if(pb.crouch.IsPressed()) 
        {
            player.localScale += new Vector3(0.333f * 4f, 0.333f * 0.25f, 0.333f);
        }
        else 
        {
            player.localScale += new Vector3(0.333f, 0.333f, 0.333f);
        }
        
    }
}

