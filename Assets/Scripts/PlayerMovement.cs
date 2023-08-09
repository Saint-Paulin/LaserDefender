using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Shooter shooter;

    [Header("Player movement")]
    [SerializeField] float moveSpeed = 5f;
    // Vector2 playerVelocity;
    // PlayerInput playerInput;
    // Rigidbody2D rb;

    [Header("Player camera padding")]
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBot;
    Vector2 minBounds;
    Vector2 maxBounds;

    private void Awake() {
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        // rb = GetComponent<Rigidbody2D>();
        InitsBounds();
    }

    void Update()
    {
        Movement();
    }

    void InitsBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2 (0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2 (1,1));
    }

    void OnMove(InputValue value)
    {
        // if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }

    void Movement()
    {
        // playerVelocity = new Vector2 (moveInput.x * moveSpeed, rb.velocity.y);
        // rb.velocity = playerVelocity;
        Vector2 delta = moveInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBot, maxBounds.y - paddingTop);
        transform.position = newPos;
    }
}
