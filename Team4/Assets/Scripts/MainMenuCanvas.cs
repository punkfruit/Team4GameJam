using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour
{
    public Button startButton;

    private void Start()
    {
        startButton.Select();//initally selects the start button so gamepad compatibility is maintained!
    }
    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                startButton.Select();
            }
        }
    }
}
