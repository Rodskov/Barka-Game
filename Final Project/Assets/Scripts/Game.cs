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

    public TextMeshProUGUI coinText;

    private PlayerStats PlayerStatsScript;
    public float Coins;


    public Shop shopReference;
    public bool isShopOpen;

    public TextMeshProUGUI deathText;
    public GameObject restartButton;
    public bool isGameActive;

    // This script initializes the coins from the PlayerStats script
    void Start()
    {
        PlayerStatsScript = GameObject.Find("Player").GetComponent<PlayerStats>();
        Coins = PlayerStatsScript.playerCoins;
        UpdateAllCoinsUIText();
        isGameActive = true;
    }
    // The game checks if the palyer has enough money for the items in the shop
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
        coinText.text = "" + Coins;
    }
    // The shop opens after a wave is finished and is then closed after a certain amount of time
    public void OpenShop()
    {
        shopReference.ShopPanel.gameObject.SetActive(true);
        isShopOpen = true;
    }
    public void CloseShop()
    {
        shopReference.ShopPanel.gameObject.SetActive(false);
    }
    // This unlocks the cursor during shop sequence to let the player buy items and locks it again
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
    // This will Reload our scene everytime we press restart or play again button.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
