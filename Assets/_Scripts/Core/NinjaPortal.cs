using UnityEngine;


public class NinjaPortal : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem particleSys;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance.totalNinjas == 0)
        {
            //GameManager.Instance.GetCurrentMiniGame() +=1;
            GameManager.Instance.LoadScene("Main Scene");
        }
    }
    private void Start()
    {
        spriteRenderer.enabled = false;

    }
    private void Update()
    {
        if (GameManager.Instance.totalNinjas==0)
        {
            spriteRenderer.enabled = true;
            
        }
    }
}
