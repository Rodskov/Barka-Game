using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


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

}
