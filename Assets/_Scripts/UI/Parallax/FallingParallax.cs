using Unity.VisualScripting;
using UnityEngine;

public class FallingParallax : MonoBehaviour
{
    [SerializeField] public float scrollSpeed = 0.1f;
    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] Material material;
    private PlayerControllerFalling playerController;
    private float currentScrollSpeed = 0f;
    private float velocityRef = 0f;
    private float accumulatedOffset = 0f;

    float playerVelocity;

    private void Start()
    {
        playerController = GameObject.Find("Sprite-Jumper").GetComponent<PlayerControllerFalling>();
    }
    void Update()
    {
        if (playerController != null)
            playerVelocity = playerController.getVelocity();
        else
            Debug.LogWarning("PlayerController is null!");

        
        float targetSpeed = playerVelocity * scrollSpeed;

        currentScrollSpeed = Mathf.SmoothDamp(currentScrollSpeed, targetSpeed, ref velocityRef, smoothTime);

        if (currentScrollSpeed > 0.001f)
        {
            accumulatedOffset += currentScrollSpeed * Time.deltaTime;
            material.mainTextureOffset  = new Vector2(0, accumulatedOffset);
        }
    }
}
