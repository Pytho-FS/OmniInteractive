using UnityEngine;

public class PortalController : MonoBehaviour
{
    private ParticleSystem portalParticles;

    public void PlayPortalEffect()
    {
        if (portalParticles != null)
        {
            portalParticles.Play();
            Debug.Log("Initital effect played on main pillar.");

            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.LoadScene("Main Scene");
        }
    }
    private void Start()
    {
        portalParticles = GetComponent<ParticleSystem>();
        PlayPortalEffect();
    }
}
