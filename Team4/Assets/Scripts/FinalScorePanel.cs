using TMPro;
using UnityEngine;

public class FinalScorePanel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI finalScoreText;
    [SerializeField]
    TextMeshProUGUI eggsLaidText;
    [SerializeField]
    TextMeshProUGUI timeRemainingText;
    private void OnEnable()
    {
        print("Final Score Panel Enabled");
        eggsLaidText.text = GameDirector.Instance.EggCount.ToString();
        float minutes = Mathf.Floor(GameDirector.Instance.currentTime / 60);
        float seconds = Mathf.Floor(GameDirector.Instance.currentTime % 60);

        timeRemainingText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        finalScoreText.text = GameDirector.Instance.Score.ToString();
    }
}
