using UnityEngine;

public class Beam : MonoBehaviour
{
    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0,0,angle);
    }
}
