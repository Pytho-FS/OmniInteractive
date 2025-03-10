using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the main pillar's beam behaviour.
/// Sequence:
///     1.  Wait for the player to collect all required crystals.
///     2.  Trigger an initial particle effect.
///     3.  After the effect, continuously fire a beam straight upward.
///     4.  Once the player has placed crystals on all secondary pillars,
///             trigger a second (stronger) particle effect and display a message.
///     5.  When the player presses 'E', disable player movement and enter beam alignment mode.
///             The player can then only rotate the beams direction. 
///             Each successful alignment (when the beam "hits" a designated target on a secondary pillar)
///             registers a hit. When all pillars are hit, a final effect occurs and a portal is enabled.
/// </summary>
public class MainPillarController : MonoBehaviour
{
    private enum PillarState
    {
        WaitingForCrystals, // Waiting for player to collect all crystals.
        InitialEffect,      // Play Initial particle effect for collecting all crystals.
        BeamShooting,       // Fire beam upward
        WaitingForSecondaryPillars, // Waiting for crystals to be placed.
        PreInteractionEffect,       // Playing a stronger effect.
        BeamAlignment,      // Player interacts with movement disabled only beam movement
        Completed,          // Puzzle complete open portal to advance.
    }
    [Header("Beam Settings")]
    public Transform beamStartingPoint;
    public GameObject beamPrefab;
    public float beamInterval = 2f;

    [Header("Pillar Targets")]
    public Transform[] pillarTargets;

    [Header("Portal Settings")]
    public GameObject portalPrefab;

    public GameObject interactMessage;
    public int requiredCrystals = 3;
    public int totalSecondaryPillars = 3;

    [Header("Particle Systems")]
    public ParticleSystem initialEffect;
    public ParticleSystem preInteractionEffect;

    // Internal state variables
    private PillarState currentState = PillarState.WaitingForCrystals;
    private float beamTimer = 0f;
    private int currentSecondaryPillarHits = 0;
    private bool isPlayerAligning = false;


    private void Update()
    {
        switch (currentState)
        {
            case PillarState.WaitingForCrystals:
                {
                    // Check if the player has collected all required crystals.
                    if (CrystalInventoryManager.Instance != null &&
                        CrystalInventoryManager.Instance.CrystalAmount >= requiredCrystals)
                    {
                        // Transition into initial effect.
                        StartCoroutine(StartInitialEffect());
                    }
                    break;
                }

            case PillarState.BeamShooting:
                {
                    // Main pillar continuously shoots a beam upward.
                    beamTimer += Time.deltaTime;
                    if (beamTimer >= beamInterval)
                    {
                        FireBeam(Vector2.up);
                        beamTimer = 0f;
                    }
                    // External logic that tracks when a player has placed crystals on secondary pillars
                    if (SecondaryPillarsActivated())
                    {
                        StartCoroutine(StartPreInteractionEffect());
                    }
                    break;
                }

            case PillarState.BeamAlignment:
                {
                    // In BeamAlignment, the player should be prevented from normal movement.
                    // They can only rotate the beam.
                    if (isPlayerAligning)
                    {
                        float rotationInput = Input.GetAxis("Horizontal");
                        if (rotationInput != 0)
                        {
                            // Rotate the beams direction
                            Debug.Log("Rotating beam!");
                        }

                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            currentSecondaryPillarHits++;
                            Debug.Log("Secondary Pillar" + currentSecondaryPillarHits + " aligned!");
                            if (currentSecondaryPillarHits >= totalSecondaryPillars)
                            {
                                CompletePuzzle();
                            }
                        }
                        // Allow exit from alignment with Esc
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            isPlayerAligning = false;
                            Debug.Log("Exiting alignment mode!");
                        }
                    }
                    break;
                }

            default:
                break;
        }
    }

    /// <summary>
    /// Instantiate a beam at the beamStartingPoint in the given direction.
    /// </summary>
    /// <param name="direction">Normalized direction vector for the beam.</param>
    private void FireBeam(Vector2 direction)
    {
        if (beamPrefab == null || beamStartingPoint == null)
        {
            Debug.LogWarning("Beam prefab or start point not assigned.");
            return;
        }
        GameObject beamObj = Instantiate(beamPrefab, beamStartingPoint.position, Quaternion.identity);
        Beam beam = beamObj.GetComponent<Beam>();
        if (beam != null)
        {
            beam.SetDirection(direction);
        }
        Destroy(beamObj, 1f);
    }

    private IEnumerator StartInitialEffect()
    {
        Debug.Log("All crystals collected. Playing initial effect.");
        currentState = PillarState.InitialEffect;

        if (initialEffect != null)
        {
            initialEffect.Play();
        }

        yield return new WaitForSeconds(2f);
        currentState = PillarState.BeamShooting;
        Debug.Log("Initial effect complete. Main pillar now shooting beam upward.");
    }

    private bool SecondaryPillarsActivated()
    {
        // Check if crystals have been placed
        if (CrystalInventoryManager.Instance != null && CrystalInventoryManager.Instance.CrystalAmount == 0 && currentState != PillarState.WaitingForCrystals)
        {
            return true;
        }

        return false;
    }

    private IEnumerator StartPreInteractionEffect()
    {
        Debug.Log("All secondary pillars activated. Playing pre-interaction effect.");
        currentState = PillarState.PreInteractionEffect;

        if (preInteractionEffect != null)
        {
            preInteractionEffect.Play();
        }

        yield return new WaitForSeconds(2f);
        if (interactMessage != null)
        {
            interactMessage.SetActive(true);
        }
        currentState = PillarState.BeamAlignment;
        isPlayerAligning = true;
    }

    private void CompletePuzzle()
    {
        Debug.Log("All secondary pillar aligned. Puzzle Completed!");
        isPlayerAligning = false;
        if (interactMessage != null)
        {
            interactMessage.SetActive(false);
        }

        if (portalPrefab != null)
        {
            portalPrefab.SetActive(true);
            Debug.Log("Portal enabled!");
        }

        currentState = PillarState.Completed;
    }
}
