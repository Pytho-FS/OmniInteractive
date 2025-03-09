using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    public Transform[] miniGameBubbles;
    public float zoomSpeed = 2f;
    public float moveSpeed = 2f;
    public float defaultZoomSize = 5f;
    private Camera cam;
    private int currentMiniGame = 0;
    public float zoomInSize = 3f;
    public float zoomOutSize = 6f;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        cam = Camera.main;
    }

    private void Start()
    {
        MoveToMiniGame(currentMiniGame);
    }

    //For testing purposes
    private void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextMiniGame();
        }
    }
    public void MoveToMiniGame(int miniGameIndex)
    {
        if (miniGameIndex >= 0 && miniGameIndex < miniGameBubbles.Length)
        {
            currentMiniGame = miniGameIndex;
            StopAllCoroutines();
            StartCoroutine(SmoothMoveTo(miniGameBubbles[miniGameIndex]));
        }
    }

    private IEnumerator SmoothMoveTo(Transform target)
    {
        yield return StartCoroutine(SmoothZoom(zoomOutSize));

        while (Vector2.Distance(cam.transform.position, target.position) > 0.1f)
        {
            Vector3 newPos = Vector2.Lerp(cam.transform.position, target.position, Time.deltaTime * moveSpeed);
            cam.transform.position = new Vector3(newPos.x, newPos.y, cam.transform.position.z); // Keep Z constant
            yield return null;
        }

        yield return StartCoroutine(SmoothZoom(zoomInSize));

        yield return new WaitForSeconds(1f);

      //  yield return StartCoroutine(SmoothZoom(defaultZoomSize));
    }

    private IEnumerator SmoothZoom(float targetSize)
    {
        while (Mathf.Abs(cam.orthographicSize - targetSize) > 0.1f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);
            yield return null;
        }
    }

    public void NextMiniGame()
    {
        currentMiniGame++;
        if (currentMiniGame >= miniGameBubbles.Length)
        {
            currentMiniGame = 0;
        }
        MoveToMiniGame(currentMiniGame);
    }
}

