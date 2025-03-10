using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position; // Store starting position
    }

    public void ResetPosition()
    {
        transform.position = initialPosition; // Reset to the original position
        Debug.Log("Player position reset!");
    }
}
