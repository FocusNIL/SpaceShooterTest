using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    // The scene names
    public string play_scene;
    public string start_scene;

    // Causes the button to send the user to the play scene
    public void startGame()
    {
        SceneManager.LoadScene(play_scene);
    }

    // Causes the button to quit the game
    public void exitGame()
    {
        Application.Quit();
    }

    // Causes the button to return the user to the starting screen
    public void toStartMenu()
    {
        SceneManager.LoadScene(start_scene);
    }
}
