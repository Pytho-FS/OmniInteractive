using UnityEngine;

/// <summary>
/// This script handles the logic for dropping a crystal at a specific pillar.
/// </summary>
public class CrystalDropSpot : MonoBehaviour
{
    [SerializeField] private GameObject crystalBallSprite;

    private void Awake()
    {
        if (crystalBallSprite != null)
        {
            crystalBallSprite.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collidng object is the player
        if (other.CompareTag("Player"))
        {
            if (CrystalInventoryManager.Instance.CrystalAmount > 0)
            {
                CrystalInventoryManager.Instance.RemoveCrystal();
                Debug.Log("Crystal Transferred!");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (crystalBallSprite != null)
            {
                crystalBallSprite.SetActive(true);
                Debug.Log("Crystal Pillar Activated!");
            }
            else
            {
                Debug.LogWarning("CrystalBall missing...");
            }
        }
    }
}
