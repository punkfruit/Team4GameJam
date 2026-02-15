using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is a singleton that handles the finer details of the game,
/// such as tracking the number of eggs laid and triggering events when certain conditions are met (e.g., when an egg is laid).
/// </summary>
public class GameDirector : MonoBehaviour
{
    public static GameDirector Instance { get; private set; } // Singleton instance

    private int eggCount = 0;
    public UnityEvent OnEggLayed; // Event triggered when an egg is laid

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

    public void EggLayed()
    {
        OnEggLayed.Invoke();
        eggCount++;
    }
}
