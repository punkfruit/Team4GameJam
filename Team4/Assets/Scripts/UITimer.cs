using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    TextMeshProUGUI timerText;
    GameDirector gameDirector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        timerText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float minutes = Mathf.Floor(GameDirector.Instance.currentTime / 60);
        float seconds = Mathf.Floor(GameDirector.Instance.currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
