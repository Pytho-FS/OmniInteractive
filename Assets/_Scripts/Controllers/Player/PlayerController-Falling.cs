using UnityEngine;

public class PlayerControllerFalling : MonoBehaviour {
    
    Rigidbody2D _rb;
    SpriteRenderer _spriteRenderer;

    [SerializeField] float speed;
    
    Vector2 _moveDir;
    Vector2 _playerVelocity;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.freezeRotation = true;
    }
    private void Update()
    {
        Movement();
    }

    private void Movement() 
    { 
        // Move on arrow key direction
        float moveX = Input.GetAxis("Horizontal");
        
        // Set movement vector
        _moveDir = new Vector2(moveX, 0).normalized;
        if (_moveDir != Vector2.zero)
        {
            _rb.linearVelocity = new Vector2(_moveDir.x * speed, _rb.linearVelocity.y);
        }
        if (!Input.GetButton("Horizontal"))
        {
            _rb.linearVelocityX = 0f;
        }
        
        // Flip sprite on Y-axis based on movement direction
        if (moveX != 0)
        {
            _spriteRenderer.flipX = moveX < 0;
        }
    }
}

