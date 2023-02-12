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
   


    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void gameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameOverText.gameObject.SetActive(true);
        Background.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }
    public void restartGame()
    {
        SceneManager.LoadScene("Scene 1");
    }
    public void quitGame()
    {
        SceneManager.LoadScene("Start Game");
    }
}
