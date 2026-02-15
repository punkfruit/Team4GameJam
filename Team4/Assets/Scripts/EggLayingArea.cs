using UnityEngine;
using UnityEngine.UI;

public class EggLayingArea : MonoBehaviour
{
    [SerializeField]
    float fillAmount = 0.0f;
    [SerializeField]
    float fillRate = 0.1f;
    [SerializeField]
    float maxFillAmount = 1.0f;
    [SerializeField]
    float fillDecayRate = 0.05f; // The rate at which the egg laying area decays when not being filled
    [SerializeField]
    private Slider fillBar; // Reference to the UI element representing the fill amount
    [SerializeField]
    Sprite eggIcon; // Reference to the egg icon that will be shown when the area is fully filled

    [HideInInspector]
    public bool isBeingFilled = false;
    private bool isPlayerInArea = false;
    private bool hasLaidEgg = false;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fillBar.gameObject.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingFilled && !hasLaidEgg)
        {
            fillAmount += fillRate * Time.deltaTime;
            fillAmount = Mathf.Min(fillAmount, maxFillAmount);
            if(fillAmount == maxFillAmount)
            {
                SpawnEgg();
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

        if (hasLaidEgg)
        {
            fillBar.gameObject.SetActive(false);
        }
    }

    private void SpawnEgg()
    {
        //Plant Egg and deactivate the spawner
        GameDirector.Instance.EggLaid();
        hasLaidEgg = true;
        spriteRenderer.sprite = eggIcon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasLaidEgg)
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
