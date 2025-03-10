using UnityEngine;
using UnityEngine.UI;

public class NewEmptyCSharpScript : MonoBehaviour
{
        [SerializeField] public Image StaminaBar;
    [SerializeField] GameObject player;
     void Awake()
    {
        StaminaBar = GetComponent<Image>();
        
    }

    private void Update()
    {
        if (StaminaBar != null) {
            UpdateStaminaBar();
        }
    }

    public void UpdateStaminaBar()
    {
        StaminaBar.fillAmount = player.GetComponent<PlayerController>().CurrentStamina;

    }
}
