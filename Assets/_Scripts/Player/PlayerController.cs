using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    [SerializeField] float speed;
    [SerializeField] float sprintMod;
    [SerializeField] float jumpMax;
    [SerializeField] float jumpSpeed;
    [SerializeField] int jumpCount;


    [SerializeField] float maxStamina;
    [SerializeField] float staminaDrainRate;
    [SerializeField] float staminaRegenRate;
    [SerializeField] float sprintThreshold; //this is min stamina needed

    [SerializeField] BoxCollider2D footCollider;
    [SerializeField] BoxCollider2D activationCollider;
    float currentStamina;
    public float CurrentStamina => currentStamina;
    Vector2 lastMoveDirection = Vector2.right;
    Vector2 moveDir;
    Vector2 playerVelocity;

    bool isSprinting;
    bool canSprint;
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
        if (Input.GetButtonDown("Interact"))
        {
            if (activationCollider.IsTouchingLayers(LayerMask.GetMask("Button"))) { 
                //talk to game, activate gate for opening function.
            }
        }

    }
/*    private void OnTriggerEnter2D(Collider2D collision)
    {
        jumpCount = 0;
    }*/
    void movement() { 
/*    if (controller.isGrounded)
        {
            playerVelocity = Vector2.zero;
            jumpCount = 0;
        }*/
        float moveX = Input.GetAxis("Horizontal");
        // Set movement vector
        moveDir = new Vector2(moveX, 0).normalized;
        if (moveDir != Vector2.zero)
        {
            lastMoveDirection = moveDir;
            rb.linearVelocity = new Vector2(moveDir.x * speed, rb.linearVelocity.y);
        }
        if (!Input.GetButton("Horizontal")&& footCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rb.linearVelocityX = 0f;
            
        }
        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            jumpCount++;
            //if horizontal is not pressed, set x velocity to 0 if jumping and grounded
            if (!Input.GetButton("Horizontal") && footCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                rb.linearVelocityX = 0.0f;
                rb.angularVelocity = 0f;
            }
            
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);

        }
        // Flip sprite on Y-axis based on movement direction
        if (moveX != 0)
        {
            spriteRenderer.flipX = moveX < 0;
        }
        if (footCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))&& rb.linearVelocity.y <=0) {

            jumpCount = 0;
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
            //regenerate stamina when not sprinting
            if (currentStamina < maxStamina)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
            }
        }

        //clamp stam stat
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        //no sprinting when stamina is empty
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

