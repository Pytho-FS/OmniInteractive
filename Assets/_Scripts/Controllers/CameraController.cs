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
    private int currentMiniGame = 2;
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
    
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    NextMiniGame();
        //}
    }
    public void MoveToMiniGame(int miniGameIndex)
    {
        if (miniGameIndex >= 0 && miniGameIndex < miniGameBubbles.Length)
        {
            currentMiniGame = miniGameIndex;
            StopAllCoroutines();
            StartCoroutine(SmoothMoveAndLoad(miniGameBubbles[miniGameIndex], miniGameIndex));
        }
    }

    private IEnumerator SmoothMoveAndLoad(Transform target, int miniGameIndex)
    {
        yield return StartCoroutine(SmoothZoom(zoomOutSize));

        Vector3 targetPosition = new Vector3(target.position.x, target.position.y + 1.15f, cam.transform.position.z);

        while (Vector2.Distance(new Vector2(cam.transform.position.x, cam.transform.position.y),
                                new Vector2(targetPosition.x, targetPosition.y)) > 0.1f)
        {
            Vector3 newPos = Vector3.Lerp(cam.transform.position, targetPosition, Time.deltaTime * moveSpeed);

            cam.transform.position = new Vector3(newPos.x, newPos.y, cam.transform.position.z);

            yield return null;
        }

        yield return StartCoroutine(SmoothZoom(zoomInSize));

        yield return new WaitForSeconds(2f);

        GameManager.Instance.LoadScene("Minigame_" + miniGameIndex);
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

