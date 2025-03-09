using UnityEngine;

public class MiniGameController : MonoBehaviour
{

    public void CompleteMiniGame()
    {
        Debug.Log("MiniGame " + GameManager.Instance.GetCurrentMiniGame() + " Completed!");
        GameManager.Instance.StartNextMiniGame();
    }
}

