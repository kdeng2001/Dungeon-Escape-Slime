using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputActionMap currActionMap;

    public InputAction movement;
    public InputAction jump;
    public InputAction crouch;
    public InputAction launch;
    public InputAction consume;
    public InputAction takeDamage; // for testing
    public InputAction recoverDamage; // for testing

    public Rigidbody2D rb;

    public float currentDirection { get; private set; }
    public float newDirection { get; private set; }

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        currActionMap = playerInput.currentActionMap;

        movement = currActionMap.FindAction("Movement");
        jump = currActionMap.FindAction("Jump");
        crouch = currActionMap.FindAction("Crouch");
        launch = currActionMap.FindAction("Launch");
        consume = currActionMap.FindAction("Consume");
        takeDamage = currActionMap.FindAction("TakeDamage"); // for testing
        recoverDamage = currActionMap.FindAction("RecoverDamage"); // for testing

        rb = GetComponent<Rigidbody2D>();
        currentDirection = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        newDirection = movement.ReadValue<Vector2>().x;
        HandleDirectionChange();
    }

    private void HandleDirectionChange()
    {
        if (newDirection < 0 && currentDirection < 0) { return; }
        if (newDirection > 0 && currentDirection > 0) { return; }
        if (newDirection != currentDirection && newDirection != 0)
        {
            Debug.Log("New direction: " + newDirection + " Current direction: " + currentDirection);
            currentDirection = newDirection;
            transform.Rotate(new Vector3(0, 0, 180));

        }
    }
}
