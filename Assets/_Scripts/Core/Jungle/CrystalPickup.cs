using UnityEngine;

/// <summary>
/// This script allows the player to pick up a crystal by colliding with it.
/// Attatch this script to the Crystal gameobject.
/// </summary>
public class CrystalPickup : MonoBehaviour
{
    [SerializeField] public Sprite crystalSprite;

    private bool crystalPickedUp = false;

    /// <summary>
    /// Called when another collider enters this object's trigger collider.
    /// If the collider belongs to the Player, add the crystal to the inventory and destroy the crystal.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Add the crystal to the inventory
            if (CrystalInventoryManager.Instance != null)
            {
                CrystalInventoryManager.Instance.AddCrystal(crystalSprite);

                MainPillarController.Instance?.PlayInitialEffect();

                crystalPickedUp = true;
                if (crystalPickedUp)
                {
                    // Destroy the crystal object afters it's been picked up.
                    Destroy(gameObject);
                }
            }
            else
            {
                Debug.Log("Player did not pickup the crystal!");
            }
        }
    }
}
