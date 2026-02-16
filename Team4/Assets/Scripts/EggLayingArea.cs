using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class EggLayingArea : MonoBehaviour
{
    [SerializeField] float fillAmount = 0.0f;
    [SerializeField] float fillRate = 0.1f;
    [SerializeField] float maxFillAmount = 1.0f;
    [SerializeField] float fillDecayRate = 0.05f;

    [SerializeField] private Slider fillBar;
    [SerializeField] Sprite eggIcon;

    [Header("Prompt Icon")]
    [SerializeField] private PlayerInput playerInput;   
    [SerializeField] private Image promptIconImage;      
    [SerializeField] private Sprite keyboardPromptSprite;
    [SerializeField] private Sprite gamepadPromptSprite;

    [HideInInspector] public bool isBeingFilled = false;

    private bool isPlayerInArea = false;
    private bool hasLaidEgg = false;
    private SpriteRenderer spriteRenderer;

    

    void Start()
    {
        if (playerInput == null)
            playerInput = GameObject.FindGameObjectWithTag("Player")
                        .GetComponent<PlayerInput>();//get player input component for the controller glyphs to work

        fillBar.gameObject.SetActive(false);

        if (promptIconImage != null)
            promptIconImage.gameObject.SetActive(false); 

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isBeingFilled && !hasLaidEgg)
        {
            fillAmount += fillRate * Time.deltaTime;
            fillAmount = Mathf.Min(fillAmount, maxFillAmount);

            if (fillAmount == maxFillAmount)
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

        if (!isPlayerInArea && fillAmount <= 0.0f)
        {
            fillBar.gameObject.SetActive(false);
            if (promptIconImage != null) promptIconImage.gameObject.SetActive(false); 
        }

        if (hasLaidEgg)
        {
            fillBar.gameObject.SetActive(false);
            if (promptIconImage != null) promptIconImage.gameObject.SetActive(false); 
        }
    }

    private void SpawnEgg()
    {
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

            UpdatePromptIcon(); 
            if (promptIconImage != null) promptIconImage.gameObject.SetActive(true); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInArea = false;
        }
    }

    
    


    private void UpdatePromptIcon()
    {
        if (promptIconImage == null || playerInput == null) return;

        bool isGamepad = playerInput.currentControlScheme == "Gamepad";
        promptIconImage.sprite = isGamepad ? gamepadPromptSprite : keyboardPromptSprite;
    }

}
