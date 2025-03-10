using UnityEngine;

public class PlayerControllerFalling : MonoBehaviour {

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Collider2D collider;

    [SerializeField] float speed;
    [SerializeField] float gravity;

    Vector2 lastMoveDirection = Vector2.right;
    Vector2 moveDir;
    Vector2 playerVelocity;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        collider = GetComponent<Collider2D>();
    }
    private void Update()
    {
        movement();
    }

    void movement() 
    { 
        float moveX = Input.GetAxis("Horizontal");

        // Set movement vector
        moveDir = new Vector2(moveX, 0).normalized;
        if (moveDir != Vector2.zero)
        {
            lastMoveDirection = moveDir;
            rb.linearVelocity = new Vector2(moveDir.x * speed, rb.linearVelocity.y);
        }

        // Flip sprite on Y-axis based on movement direction
        if (moveX != 0)
        {
            spriteRenderer.flipY = moveX < 0;
        }
    }
}

