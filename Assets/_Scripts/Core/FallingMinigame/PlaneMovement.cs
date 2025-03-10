using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField] float speed;

    GameObject obstacle;
    ObstacleLogic script;

    private void Start()
    {
        obstacle = GameObject.Find("Obstacles");
        script = obstacle.GetComponent<ObstacleLogic>();
    }
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x + speed, transform.position.y + script.Speed(), -1);
    }
}
