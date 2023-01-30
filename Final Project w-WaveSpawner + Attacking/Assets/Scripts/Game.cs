using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour
{
    #region SIngleton:Game
    public static Game Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] Text[] allCoinsUIText;


    public int Coins;


    public Shop shopReference;
    public bool isShopOpen;

    public Text deathText;
    public GameObject restartButton;
    public bool isGameActive;


    void Start()
    {
        UpdateAllCoinsUIText();

       
    }
    public void UseCoins(int amount)
    {
        Coins -= amount;
    }
    public bool HasEnoughCoins(int amount)
    {
        return (Coins >= amount);
    }
    public void UpdateAllCoinsUIText()
    {
        for (int i = 0; i < int.MaxValue; i++)
        {
            allCoinsUIText[i].text = Coins.ToString();
        }
    }

    public void OpenShop()
    {
        shopReference.ShopPanel.gameObject.SetActive(true);
        isShopOpen = true;
    }

    
    public void CloseShop()
    {
        shopReference.ShopPanel.gameObject.SetActive(false);
    }

    public void UnlockCursor()
    {
        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.None;
    }

    public void LockCursor()
    {
        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void StartGame()
    {
        isGameActive = true;


    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // This will Reload our scene everytime we press restart or play again button.
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        deathText.gameObject.SetActive(true);
        isGameActive = false;
    }

}
