using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public static MainPillarController Instance {  get; private set; }

    [Header("Particle Systems")]
    public ParticleSystem initialEffect;
    public ParticleSystem preInteractionEffect;

    [Header("Portal Prefab")]
    [SerializeField] public GameObject portalPrefab;

    [Header("Settings")]
    public int totalTowers = 3;

    private int towerDepositCount = 0;

    [Header("Text Fields")]
    [SerializeField] private TMP_Text initialMsg;
    [SerializeField] private TMP_Text finalMsg;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        finalMsg.enabled = false;
        initialMsg.enabled = true;
    }

    public void PlayInitialEffect()
    {
        if (initialEffect != null)
        {
            initialEffect.Play();
            Debug.Log("Initital effect played on main pillar.");
        }
    }

    public void OnTowerDeposit()
    {
        towerDepositCount++;
        if (towerDepositCount >= totalTowers)
        {
            StartCoroutine(PlayPreInteractionEffect());
        }
    }

    private IEnumerator PlayPreInteractionEffect()
    {
        Debug.Log("All towers have been deposited.");
        if (preInteractionEffect != null)
        {
            preInteractionEffect.Play();
        }
        yield return new WaitForSeconds(2f);
        if (portalPrefab != null)
        {
            portalPrefab.SetActive(true);
            Debug.Log("Portal enabled!");
        }

    }

    private void Update()
    {
        if (towerDepositCount == totalTowers)
        {
            initialMsg.enabled = false;
            finalMsg.enabled = true;
        }
    }
}
