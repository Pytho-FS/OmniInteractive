using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField] float speed;

    Vector3 origPosition;

    GameObject obstacle;
    ObstacleLogic script;

    private void Start()
    {
        origPosition = transform.position;
        obstacle = GameObject.Find("Obstacles");
        script = obstacle.GetComponent<ObstacleLogic>();
    }
    void Update()
    {
        if (!script.IsPaused())
            this.transform.position = new Vector3(this.transform.position.x + speed, transform.position.y + script.Speed(), -1);

        else 
            this.transform.position = origPosition;
    }
}
