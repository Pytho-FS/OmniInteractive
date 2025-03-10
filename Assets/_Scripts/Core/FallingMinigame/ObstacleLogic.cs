using System.Collections;
using System.Threading;
using UnityEngine;

public class ObstacleLogic : MonoBehaviour
{

    [SerializeField] float speed;

    private Vector3 origPosition;
    private bool isPaused = false;

    void Start()
    {
        speed = 0.01f;
        origPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed > 0)
        {
            this.gameObject.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + speed);
        }
    }

    public void OnChildCollision(ObstacleChild child)
    {
        Debug.Log("Player touched obstacle!");

        StartCoroutine(DieScreen());
    }

    private IEnumerator DieScreen()
    {
        isPaused = true;

        speed = 0;
        FadeDieScreen(1f, true);

        yield return new WaitForSeconds(2);

        FadeDieScreen(1f, false);
        this.gameObject.transform.position = origPosition;

        GameObject.Find("Sprite-Jumper").GetComponent<PlayerReset>().ResetPosition();

        yield return new WaitForSeconds(1);

        speed = 0.01f;

        isPaused = false;
    }

    public void FadeDieScreen(float targetAlpha, bool fadeIn)
    {
        GameObject dieScreenObj = GameObject.Find("DieScreen");

        if (dieScreenObj != null)
        {
            SpriteRenderer sr = dieScreenObj.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                StartCoroutine(GetComponent<SmoothOpacity>().FadeRoutine(sr, targetAlpha, fadeIn));
            }
            else
            {
                Debug.LogWarning($"DieScreen not found!");
            }
        }
    }

    public float Speed()   { return speed; }
    public bool IsPaused() { return isPaused; }
}
