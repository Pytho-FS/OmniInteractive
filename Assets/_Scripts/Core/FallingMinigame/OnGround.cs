using UnityEngine;

public class OnGround : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.WinGame();
        }

        GameObject.Find("Obstacles").GetComponent<ObstacleLogic>().Speed(0);
    }
}
