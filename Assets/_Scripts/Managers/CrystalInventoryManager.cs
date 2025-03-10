using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Manages the player's crystal inventory and updates the UI accordingly.
/// This script is a singleton.
/// </summary>

public class CrystalInventoryManager : MonoBehaviour
{
    public static CrystalInventoryManager Instance {  get; private set; }

    [Header("UI References")]
    [SerializeField] private Image uiCrystalSlot;
    [SerializeField] private TMP_Text crystalAmountText;

    // Current count of crystals in the inventory.
    private int crystalAmount = 0;

    public int CrystalAmount => crystalAmount;

    /*
     *  Various Helper Methods 
     * 
    */

    /// <summary>
    /// Adds a crystal to the inventory and updates the UI.
    /// </summary>
    /// <param name="crystalSprite">The sprite to display in the UI slot when a crystal is picked up.</param>
    public void AddCrystal(Sprite crystalSprite)
    {
        crystalAmount++;

        // Set the UI slot sprite and ensure its visible
        if (uiCrystalSlot != null)
        {
            uiCrystalSlot.sprite = crystalSprite;
            uiCrystalSlot.enabled = true;
        }

        UpdateUIText();
    }

    /// <summary>
    /// Removes one crystal from the inventory and updates the UI.
    /// </summary>
    public void RemoveCrystal()
    {
        if (crystalAmount > 0)
        {
            crystalAmount--; 

            // If no crystals remain, clear the UI slot.
            if (crystalAmount == 0 && uiCrystalSlot != null)
            {
                uiCrystalSlot.sprite = null;
                uiCrystalSlot.enabled = false;
            }

            Debug.Log($"Crystals:  + {crystalAmount}");
            UpdateUIText();
        }
    }

    /// <summary>
    /// Updates the CrystalAmount Text
    /// </summary>
    private void UpdateUIText()
    {
        if (crystalAmountText != null)
        {
            crystalAmountText.text = "" + crystalAmount;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
