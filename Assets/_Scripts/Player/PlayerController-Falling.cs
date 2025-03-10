using UnityEngine;

public class PlayerControllerFalling : MonoBehaviour {

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Collider2D capsuleCollider;

    [SerializeField] float speed;
    [SerializeField] float gravity;

    Vector2 moveDir;
    Vector2 playerVelocity;

    private bool isFacingLeft = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        capsuleCollider = GetComponent<Collider2D>();
    }
    private void Update()
    {
        movement();
    }

    void movement() 
    {
        float moveX = Input.GetAxis("Horizontal");

        // Apply movement
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

        // Flip sprite and collider only if direction changes

        if (moveX > 0 && isFacingLeft) // Moving right but facing left
        {
            FlipCharacter(false);
        }
        else if (moveX < 0 && !isFacingLeft) // Moving left but facing right
        {
            FlipCharacter(true);
        }
    }

    void FlipCharacter(bool faceLeft)
    {
        isFacingLeft = faceLeft; // Update facing direction

        // Flip sprite
        spriteRenderer.flipY = faceLeft;

        // Invert collider offset only when direction changes
        capsuleCollider.offset = new Vector2(capsuleCollider.offset.x, -capsuleCollider.offset.y);
    }
}

