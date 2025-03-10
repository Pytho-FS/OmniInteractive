using System.Collections;
using UnityEngine;

public class NinjaNPC : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float moveSpeed = 2f; 
    [SerializeField] private float waitTime = 1f;
    private Vector3 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager.totalNinjas++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WasFound()
    {
        gameManager.totalNinjas--;
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.IsTouchingLayers(LayerMask.GetMask("Player")))
            {

                WasFound();
            }
        }
    }
    
    IEnumerator MoveNinja()
    {
        // Move to target position
        yield return StartCoroutine(LerpPosition(targetPosition, moveSpeed));

        // Wait at the position
        yield return new WaitForSeconds(waitTime);

        // Move back to the start position
        yield return StartCoroutine(LerpPosition(startPosition, moveSpeed));
    }

    IEnumerator LerpPosition(Vector3 target, float duration)
    {
        float time = 0;
        Vector3 start = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(start, target, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = target; // Ensure it snaps exactly to target
    }
}
