using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class NinjaPortal : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem particleSys;

    [Header("Text Fields")]
    [SerializeField] private TMP_Text initialMsg;
    [SerializeField] private TMP_Text finalMsg;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance.totalNinjas == 0)
        {
            GameManager.Instance.SetCurrentMiniGame(1);
            GameManager.Instance.LoadScene("Main Scene");
        }
    }
    private void Start()
    {
        spriteRenderer.enabled = false;
        finalMsg.enabled = false;
        initialMsg.enabled = true;

    }
    private void Update()
    {
        if (GameManager.Instance.totalNinjas==0)
        {
            spriteRenderer.enabled = true;
            initialMsg.enabled = false;
            finalMsg.enabled = true;
        }
    }
}
