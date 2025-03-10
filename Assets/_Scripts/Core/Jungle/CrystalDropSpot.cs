using UnityEngine;

/// <summary>
/// This script handles the logic for dropping a crystal at a specific pillar.
/// </summary>
public class CrystalDropSpot : MonoBehaviour
{
    [SerializeField] private GameObject crystalBallSprite;

    private bool depositMade = false;

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
        if (!depositMade && other.CompareTag("Player"))
        {
            if (CrystalInventoryManager.Instance != null && CrystalInventoryManager.Instance.CrystalAmount > 0)
            {
                CrystalInventoryManager.Instance.RemoveCrystal();
                depositMade = true;
                if (crystalBallSprite != null)
                {
                    crystalBallSprite.SetActive(true);
                    Debug.Log("Crystal Pillar Activated!");
                }
                else
                {
                    Debug.LogWarning("CrystalBall missing...");
                }

                // Notify main pillar
                MainPillarController.Instance?.OnTowerDeposit();
            }
            else
            {
                Debug.Log("No crystals avaailable for deposit.");
            }
        }
    }
}
