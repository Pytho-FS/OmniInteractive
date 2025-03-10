using System.Collections;
using System.Threading;
using UnityEngine;

public class ObstacleLogic : MonoBehaviour
{

    [SerializeField] float speed;

    private Vector2 origPosition;
    void Start()
    {
        speed = 0.01f ;
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
        speed = 0;
        GetComponent<SmoothOpacity>().FadeDieScreen(1f, true);

        yield return new WaitForSeconds(2);

        GetComponent<SmoothOpacity>().FadeDieScreen(1f, false);
        this.gameObject.transform.position = origPosition;

        GameObject.Find("Sprite-Jumper").GetComponent<PlayerReset>().ResetPosition();

        yield return new WaitForSeconds(1);

        speed = 0.01f;
    }
}
