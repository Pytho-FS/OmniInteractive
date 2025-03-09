using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

   //[SerializeField] CharacterController controller;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    [SerializeField] float speed;
    [SerializeField] float sprintMod;
    [SerializeField] float gravity;
    [SerializeField] float jumpMax;
    [SerializeField] float jumpSpeed;
    [SerializeField] int jumpCount;

    [SerializeField] float maxStamina;
    [SerializeField] float staminaDrainRate;
    [SerializeField] float staminaRegenRate;
    [SerializeField] float sprintThreshold; //this is min stamina needed

    float currentStamina;
    Vector2 lastMoveDirection = Vector2.right;
    Vector2 moveDir;
    Vector2 playerVelocity;

    bool isSprinting;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        currentStamina = maxStamina;
    }
    private void Update()
    {
        movement();
        sprint();
        

    }
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
        if (!Input.GetButton("Horizontal"))
        {
            rb.linearVelocityX = 0f;
            
        }
        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            jumpCount++;

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);

        }
        // Flip sprite on Y-axis based on movement direction
        if (moveX != 0)
        {
            //spriteRenderer.flipX = moveX < 0;
        }
/*        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            jumpCount++;

            playerVelocity.y = jumpSpeed;

        }
        controller.Move(playerVelocity * Time.deltaTime);
        playerVelocity.y -= gravity * Time.deltaTime;*/

    }

    void sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            speed += sprintMod;
            isSprinting = true;


        }
        else if (Input.GetButtonUp("Sprint"))
        {
            speed /= sprintMod;
            isSprinting = false;
        }
    }


}

