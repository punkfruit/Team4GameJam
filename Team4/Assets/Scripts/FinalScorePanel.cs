using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FinalScorePanel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI finalScoreText;
    [SerializeField]
    TextMeshProUGUI eggsLaidText;
    [SerializeField]
    TextMeshProUGUI timeRemainingText;

    [SerializeField]
    Button ReplayButton;
    private void OnEnable()
    {
        print("Final Score Panel Enabled");
        eggsLaidText.text = GameDirector.Instance.EggCount.ToString();
        float minutes = Mathf.Floor(GameDirector.Instance.currentTime / 60);
        float seconds = Mathf.Floor(GameDirector.Instance.currentTime % 60);

        timeRemainingText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        finalScoreText.text = GameDirector.Instance.Score.ToString();

        ReplayButton.Select();
    }

    void Update()
    {
        if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
        {
            if(enabled)
            {
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    ReplayButton.Select();
                }
            }
            
        }
    }

}
