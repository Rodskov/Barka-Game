using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public GameObject Background;
    public Button restartButton;
    public Button quitButton;
   

    // Locks the cursor when the game starts
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Unlocks the cursor and shows the game over screen with restart and quit buttons
    public void gameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameOverText.gameObject.SetActive(true);
        Background.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }
    // This restarts the game
    public void restartGame()
    {
        SceneManager.LoadScene("Scene 1");
    }
    // This quits the game, bringing the user to the start menu
    public void quitGame()
    {
        SceneManager.LoadScene("Start Game");
    }
}
