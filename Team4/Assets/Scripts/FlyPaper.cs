using Unity.Cinemachine;
using UnityEngine;

public class FlyPaper : MonoBehaviour
{
    [SerializeField, RangeAttribute(0.001f,1.0f)]
    private float slowMultiplier = .9f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("Player entered");
            PlayerController pc = collision.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.horizontalSpeed *= slowMultiplier;
                pc.speedLimit *= slowMultiplier;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("Player exited");
            PlayerController pc = collision.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.horizontalSpeed /= slowMultiplier;
                pc.speedLimit /= slowMultiplier;
            }
        }
    }
}
