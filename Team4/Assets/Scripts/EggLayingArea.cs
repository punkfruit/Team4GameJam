using UnityEngine;
using UnityEngine.UI;

public class EggLayingArea : MonoBehaviour
{
    [SerializeField]
    float fillAmount = 0.0f; // The current fill amount of the egg laying area
    [SerializeField]
    float fillRate = 0.1f; // The rate at which the egg laying area fills up
    [SerializeField]
    float maxFillAmount = 1.0f; // The maximum fill amount of the egg laying area
    [SerializeField]
    float fillDecayRate = 0.05f; // The rate at which the egg laying area decays when not being filled
    [SerializeField]
    private Slider fillBar; // Reference to the UI element representing the fill amount

    [HideInInspector]
    public bool isBeingFilled = false; // Whether the egg laying area is currently being filled

    private bool isPlayerInArea = false; // Whether the player is currently in the egg laying area
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fillBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingFilled)
        {
            fillAmount += fillRate * Time.deltaTime;
            fillAmount = Mathf.Min(fillAmount, maxFillAmount);
            if(fillAmount == maxFillAmount)
            {
                //Plant Egg and deactivate the spawner
            }
        }
        else
        {
            fillAmount -= fillDecayRate * Time.deltaTime;
            fillAmount = Mathf.Max(fillAmount, 0.0f);
        }
        fillBar.value = fillAmount / maxFillAmount;

        if(!isPlayerInArea && fillAmount <= 0.0f)
        {
            fillBar.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInArea = true;
            fillBar.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInArea = false;
        }
    }
}
