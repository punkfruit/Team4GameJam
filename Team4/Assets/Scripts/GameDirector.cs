using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is a singleton that handles the finer details of the game,
/// such as tracking the number of eggs laid and triggering events when certain conditions are met (e.g., when an egg is laid).
/// </summary>
public class GameDirector : MonoBehaviour
{
    public static GameDirector Instance { get; private set; } // Singleton instance
    
    [HideInInspector]
    public float currentTime = 0f;
    [SerializeField]
    UnityEvent OnEggLayed; // Event triggered when an egg is laid
    [SerializeField]
    UnityEvent OnTimeLimitReached; // Event triggered when the time limit is reached
    [SerializeField]
    UnityEvent OnGoalReached;

    [SerializeField]
    private float timeLimit = 300f; // Time limit for the game in seconds
    
    private bool isRunning = false;
    private int eggCount = 0;
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
            DontDestroyOnLoad(gameObject);
        }
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
                OnTimeLimitReached.Invoke();
            }
        }
    }

    private void StartTimer()
    {
        isRunning = true;
        currentTime = timeLimit;
    }

    public void EggLayed()
    {
        OnEggLayed.Invoke();
        eggCount++;
    }

    public void GoalReached()
    {
        print("Goal reached!");
        OnGoalReached.Invoke();
    }
}
