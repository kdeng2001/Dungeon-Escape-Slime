using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public PlayerBehavior pb;

    private float maxVelocity;

    void Start()
    {
        pb = GetComponent<PlayerBehavior>();
        maxVelocity = 10f;
    }

    void Update()
    {
        HandleMaxVelocity();
    }

    private void HandleMaxVelocity() 
    {
        pb.rb.velocity = new Vector2(Mathf.Round(pb.newDirection) * maxVelocity, pb.rb.velocity.y);
    }
}
