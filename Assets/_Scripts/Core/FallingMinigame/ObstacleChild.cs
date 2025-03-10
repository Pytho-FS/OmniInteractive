using UnityEngine;

public class ObstacleChild : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Find "Obstacles" in the hierarchy
            Transform parent = transform;

            while (parent != null && parent.name != "Obstacles")
                parent = parent.parent; // Move up the hierarchy

            if (parent != null)
            {
                ObstacleLogic parentManager = parent.GetComponent<ObstacleLogic>();

                if (parentManager != null)
                    parentManager.OnChildCollision(this);

                else
                    Debug.LogWarning($"ObstaclesManager not found on {parent.name}!");
            }
            else
            {
                Debug.LogWarning($"Obstacles not found for {gameObject.name}!");
            }
        }
    }
}
