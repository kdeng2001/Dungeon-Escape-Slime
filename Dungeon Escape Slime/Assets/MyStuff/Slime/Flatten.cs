using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flatten : MonoBehaviour
{
    PlayerBehavior pb;
    public bool crouching { get; private set; }
    private bool isGrounded;
    [SerializeField] private float xChange;
    [SerializeField] private float yChange;
    // Start is called before the first frame update
    void Start()
    {
        pb = GetComponent<PlayerBehavior>();
        
        crouching = false;
        xChange = 4f;
        yChange = 0.25f;
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!crouching && !isGrounded && pb.crouch.WasPerformedThisFrame()) 
        {
            Debug.Log("CALLED");
            crouching = true;
            pb.rb.velocity = new Vector2(pb.rb.velocity.x, -11f);
            pb.rb.gravityScale = 0.0001f;
            transform.localScale =
               new Vector3(transform.localScale.x * xChange, transform.localScale.y * yChange, transform.localScale.z);
        }
        //if(!isGrounded) { return; } 
        else if(!crouching && /*isGrounded && */pb.crouch.WasPerformedThisFrame()) 
        {
            Debug.Log("Crouching");
            //pb.rb.gravityScale = 5f;
            crouching = true;
            transform.localScale =
                new Vector3(transform.localScale.x * xChange, transform.localScale.y * yChange, transform.localScale.z); 
        }        
        
        else if (crouching && pb.crouch.WasReleasedThisFrame())
        {
            pb.rb.gravityScale = 10f;
            crouching = false;
            Debug.Log("Uncrouch");
            transform.localScale =
                new Vector3(transform.localScale.x / xChange, transform.localScale.y / yChange, transform.localScale.z);
        }

        else if (crouching && pb.rb.velocity.y < -10f) 
        {
            //Debug.Log("Y velocity: " + pb.rb.velocity.y);
            pb.rb.gravityScale = 0.00001f;
            
        }
        else if (crouching && isGrounded) 
        {
            pb.rb.gravityScale = 20f;
        }
        else if (!crouching) { pb.rb.gravityScale = 10f; }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {   
            isGrounded = true; 
            //Debug.Log("grounded: " + isGrounded); 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            isGrounded = false;
            //isGrounded = true;
            //Debug.Log("Not grounded: " + isGrounded);
        }
    }
}
