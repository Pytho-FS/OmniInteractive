using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JunglePlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    [SerializeField] private ParticleSystem groundParticles;

    [SerializeField] float speed;
    [SerializeField] float sprintMod;
    [SerializeField] float jumpMax;
    [SerializeField] float jumpSpeed;
    [SerializeField] int jumpCount;
    [SerializeField] float acceleration = 10f;
    //[SerializeField] float deceleration = 15f;
    [SerializeField] float airControl = 0.5f;
    [SerializeField] float coyoteTime = 0.1f;
    [SerializeField] float jumpBufferTime = 0.15f;

    [SerializeField] float maxStamina;
    [SerializeField] float staminaDrainRate;
    [SerializeField] float staminaRegenRate;
    [SerializeField] float sprintThreshold;

    [SerializeField] BoxCollider2D footCollider;
    [SerializeField] BoxCollider2D activationCollider;

    float currentStamina;
    public float CurrentStamina => currentStamina;
    Vector2 lastMoveDirection = Vector2.right;
    Vector2 moveDir;
    Vector2 playerVelocity;

    bool isSprinting;
    bool canSprint;
    float coyoteTimer;
    float jumpBufferTimer;
    bool isGrounded;
    float moveInput;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        currentStamina = maxStamina;
        rb.linearDamping = 0.0f;
    }

    private void Update()
    {
        movement();
        sprint();

        if (isGrounded && Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            if (!groundParticles.isPlaying)
            {
                groundParticles.Play();
            }
        }
        else
        {
            if (groundParticles.isPlaying)
            {
                groundParticles.Stop();
            }
        }
    }

    void movement()
    {
        moveInput = Input.GetAxis("Horizontal");
        moveDir = new Vector2(moveInput, 0).normalized;
        isGrounded = footCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (isGrounded)
        {
            coyoteTimer = coyoteTime;
            jumpCount = 0;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        float targetSpeed = moveInput * speed;
        float speedDiff = targetSpeed - rb.linearVelocity.x;
        float accelRate = isGrounded ? acceleration : acceleration * airControl;
        float movement = Mathf.Clamp(speedDiff * accelRate * Time.deltaTime, -Mathf.Abs(speedDiff), Mathf.Abs(speedDiff));

        rb.linearVelocityX += movement;

        if (!Input.GetButton("Horizontal") && isGrounded)
        {
            rb.linearVelocityX = 0f;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTimer = jumpBufferTime;
            AudioManager.Instance.PlayPlayerJump();
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }

        if (jumpBufferTimer > 0 && (jumpCount < jumpMax || coyoteTimer > 0))
        {
            rb.linearVelocityY = jumpSpeed;
            jumpCount++;
            jumpBufferTimer = 0;
            coyoteTimer = 0;
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocityY > 0)
        {
            rb.linearVelocityY *= 0.5f;
        }

        // Flip sprite based on movement direction
        if (moveInput != 0)
        {
            spriteRenderer.flipX = moveInput < 0;
        }
    }

    void sprint()
    {
        if (Input.GetButton("Sprint") && currentStamina > 0 && canSprint)
        {
            if (!isSprinting)
            {
                speed *= sprintMod;
                isSprinting = true;
            }

            currentStamina -= staminaDrainRate * Time.deltaTime;
        }
        else
        {
            if (isSprinting)
            {
                speed /= sprintMod;
                isSprinting = false;
            }

            if (currentStamina < maxStamina)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
            }
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        if (currentStamina <= 0)
        {
            canSprint = false;
        }
        else if (currentStamina > sprintThreshold)
        {
            canSprint = true;
        }
    }

    public float getVelocity()
    {
        return rb.linearVelocity.x;
    }
}
