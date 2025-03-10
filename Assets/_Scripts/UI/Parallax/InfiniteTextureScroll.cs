using UnityEngine;

public class InfiniteTextureScroll : MonoBehaviour
{
    [SerializeField] public float scrollSpeed = 0.1f;
    [SerializeField] private float smoothTime = 0.2f;
    private PlayerController playerController;
    private float currentScrollSpeed = 0f;
    private float velocityRef = 0f;
    private float accumulatedOffset = 0f;

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }
    void Update()
    {
        float playerVelocity = playerController.getVelocity();
        float targetSpeed = playerVelocity * scrollSpeed;

        currentScrollSpeed = Mathf.SmoothDamp(currentScrollSpeed, targetSpeed, ref velocityRef, smoothTime);

        if (currentScrollSpeed > 0.001f)
        {
            accumulatedOffset += currentScrollSpeed * Time.deltaTime;
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(accumulatedOffset, 0);
        }


        //float offset = Time.time * scrollSpeed;
        //float uvShrink = 0.999f;
        //GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset, 0);
        //GetComponent<Renderer>().material.mainTextureScale = new Vector2(uvShrink, uvShrink);
    }
}
