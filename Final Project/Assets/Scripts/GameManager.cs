using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;

    void Start()
    {
    }
    public void gameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }
   
    void Update()
    {
    }
}
