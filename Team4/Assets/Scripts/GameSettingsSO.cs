using UnityEngine;

[CreateAssetMenu(fileName = "GameSettingsData", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettingsSO : ScriptableObject
{
    public float timeLimit = 300f; // Time limit for the game in seconds
    public float eggMultiplier = 1.2f; //Score multiplier for each egg laid
    public float timeMultiplier = 1.1f; // Score multiplier for time remaining at the end of the game
}
