using System.Collections;
using UnityEngine;

public class NinjaNPC : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] BoxCollider2D colliderNinja;
    [SerializeField] private float moveX;
    [SerializeField] private float moveY;
    [SerializeField] private float moveZ;
    [SerializeField] private float moveSpeed = 2f; 
    [SerializeField] private float waitTime = 1f;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        if (gameManager != null)
        {
            gameManager.totalNinjas++;
        }
        targetPosition.x = startPosition.x + moveX; 
        targetPosition.y = startPosition.y + moveY;
        targetPosition.z = startPosition.z + moveZ;
        StartCoroutine(toggleNinja());
    }

    // Update is called once per frame
    void Update()
    {
        if (colliderNinja.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            //talk to game, activate gate for opening function.
        }
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
            if (collision.IsTouchingLayers(LayerMask.GetMask("Ninja")))
            {

                WasFound();
            }
        }
    }
    
    IEnumerator MoveNinja()
    {
        while (true)
        {
           // yield return StartCoroutine(LerpPosition(startPosition,targetPosition, moveSpeed));
           transform.position = Vector3.Lerp(startPosition, targetPosition, 0.3f);
            yield return new WaitForSeconds(waitTime);
            //yield return StartCoroutine(LerpPosition(targetPosition,startPosition, moveSpeed));
            transform.position = Vector3.Lerp(targetPosition, startPosition, 0.3f);
            yield return new WaitForSeconds(waitTime);
        }
    }
    IEnumerator toggleNinja()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);

    }
    IEnumerator LerpPosition(Vector3 start, Vector3 target,float duration)
    {
        float time = 0;
        
        while (time < duration)
        {
            transform.position = Vector3.Lerp(start, target, time/duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = start; // Ensure it snaps exactly to target
    }
}
