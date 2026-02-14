using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    private Collider2D collision;

    void Start()
    {
        collision = GetComponent<Collider2D>();
    }

    public void OnTrigger()
    {
        collision.enabled = false;
    }
}
