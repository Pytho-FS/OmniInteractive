using UnityEngine;


public class NinjaPortal : Monobehaviour
{
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.LoadScene("Main Scene");
        }
    }
    private void Start()
    {

        PlayPortalEffect();
    }

}
