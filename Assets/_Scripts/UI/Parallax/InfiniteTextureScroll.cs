using UnityEngine;

public class SimpleParallax : MonoBehaviour
{
    [SerializeField] public float scrollSpeed = 0.1f;

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        float uvShrink = 0.999f;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset, 0);
        GetComponent<Renderer>().material.mainTextureScale = new Vector2(uvShrink, uvShrink);
    }
}
