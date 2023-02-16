using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // This loads the scene 1 where the main gameplay will happen
    public void playGame()
    {
        SceneManager.LoadScene("Scene 1");
    }

}
