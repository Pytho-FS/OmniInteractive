using System.Collections;
using UnityEngine;

public class NinjaNPC : MonoBehaviour
{
    [SerializeField] BoxCollider2D colliderNinja;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private float waitTime = 1f;
    bool destroying = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        GameManager.Instance.totalNinjas++;

        StartCoroutine(toggleNinja());
    }

    // Update is called once per frame
    void Update()
    {
        if (colliderNinja.IsTouchingLayers(LayerMask.GetMask("Player"))&& spriteRenderer.enabled)
        {

            WasFound();
        }
    }
    public void WasFound()
    {
        if (!destroying)
        {
            destroying = true;
            GameManager.Instance.totalNinjas--;
            Destroy(gameObject);
        }
    }
    IEnumerator toggleNinja()
    {
        while (true)
        {
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(waitTime);
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(waitTime+waitTime/2);

        }
    }
    /*    IEnumerator MoveNinja()
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
        }*/
}
