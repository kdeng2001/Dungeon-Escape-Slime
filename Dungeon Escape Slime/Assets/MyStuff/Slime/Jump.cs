using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerInput playerInput;
    InputActionMap currActionMap;

    InputAction jump;
    public Vector2 jumpForce;
    public bool canJump { get; private set; }
    public bool jumping { get; private set; }
    private Vector3 jumpPos;
    private float jumpHeight;
    private bool jumpBoost = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        currActionMap = playerInput.currentActionMap;
        jump = currActionMap.FindAction("Jump");

        jumpForce = new Vector2(0f, 250f);
        jumpHeight = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(jump.WasPerformedThisFrame() && canJump) 
        {
            //Debug.Log("Jump!");
            jumpPos = transform.position;
            rb.AddForce(jumpForce);
            canJump = false;
            jumping = true;
            jumpBoost = true;

        }
        if(jump.WasReleasedThisFrame() && jumping) { jumpBoost = false; }
        
    }
    private void FixedUpdate()
    {
        if (jump.IsPressed() && jumping)
        {
            if (jumpBoost && transform.position.y - jumpPos.y < jumpHeight) 
            { rb.AddForce(jumpForce); }
            else 
            { jumpBoost = false; }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground")) 
        { 
            canJump = true; jumping = false; 
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground")) 
        { 
            canJump = false; 
            //Debug.Log("jump value: " + jump.ReadValue<float>()); 
        }
    }
}
