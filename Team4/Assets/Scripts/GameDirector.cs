using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is a singleton that handles the finer details of the game,
/// such as tracking the number of eggs laid and triggering events when certain conditions are met (e.g., when an egg is laid).
/// </summary>
public class GameDirector : MonoBehaviour
{
    public static GameDirector Instance { get; private set; } // Singleton instance
    public int EggCount { get => eggCount; private set => eggCount = value; }
    public int Score { get => score; private set => score = value; }

    [HideInInspector]
    public float currentTime = 0f;
    [SerializeField]
    UnityEvent OnEggLaid; // Event triggered when an egg is laid
    [SerializeField]
    UnityEvent OnTimeLimitReached; // Event triggered when the time limit is reached
    [SerializeField]
    UnityEvent OnGoalReached;
    [SerializeField]
    GameSettingsSO gameSettings;

    private float timeLimit = 60f; // Time limit for the game in seconds

    private bool isRunning = false;
    private int eggCount = 0;
    private int score = 0;
    void Awake()
    {
        // Ensure that there is only one instance of GameDirector
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        timeLimit = gameSettings.timeLimit;
    }

    private void Start()
    {
        StartTimer();
    }

    private void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0.0f)
            {
                isRunning = false;
                currentTime = 0.0f;
                CalculateScore();
                OnTimeLimitReached.Invoke();
            }
        }
    }

    private void StartTimer()
    {
        isRunning = true;
        currentTime = timeLimit;
    }

    public void EggLaid()
    {
        OnEggLaid.Invoke();
        EggCount++;
    }

    public void GoalReached()
    {
        print("Goal reached!");
        isRunning = false;
        CalculateScore();
        OnGoalReached.Invoke();
    }

    private void CalculateScore()
    {
        int timeBonus = Mathf.RoundToInt(currentTime * gameSettings.timeMultiplier);
        int eggBonus = Mathf.RoundToInt(EggCount * gameSettings.eggMultiplier);
        Score = timeBonus + eggBonus;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
