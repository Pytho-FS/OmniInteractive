using UnityEngine;

public class MainMenuParallax : MonoBehaviour
{
    [SerializeField] public float scrollSpeed = 0.1f;

    private void Update()
    {
        float offset = Time.time * scrollSpeed;
        float uvShrink = 0.999f;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset, 0);
        GetComponent<Renderer>().material.mainTextureScale = new Vector2(uvShrink, uvShrink);
    }
}
