using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [System.Serializable] class ShopItem
    {
        public Sprite Image;
        public int Price;
        public bool IsPurchased = false;
    }

    [SerializeField] List<ShopItem> ShopItemsList;
    [SerializeField] Animator NoCoinsAnim;

    [SerializeField] GameObject ItemTemplate; 
    GameObject Item;
    [SerializeField] Transform ShopScrollView;
    public GameObject ShopPanel;
    Button buyBtn;

    public Game gameReference;



    // This script manages the shop and unlocks the cursor
    void Start()
    {
        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.None;

        int noofItems = ShopItemsList.Count;

        for (int i = 0; i < noofItems; i++)
        {
            Item = Instantiate(ItemTemplate, ShopScrollView); 
            Item.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
            Item.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = ShopItemsList[i].Price.ToString();
            buyBtn = Item.transform.GetChild(2).GetComponent<Button>();
            if (ShopItemsList[i].IsPurchased)
            {
                DisableBuyBtn();
            }
            buyBtn.AddEventListener(i, OnShopItemBtnClicked);
        }   
    }
    // If the player has enough coins, the shop lets them buy the chosen item
    // If the player does not have enough coins, the item cannot be bought
    void OnShopItemBtnClicked(int itemIndex)
    {
        int itemPrice = ShopItemsList[itemIndex].Price;

        if (gameReference.HasEnoughCoins(itemPrice))
        {
            gameReference.UseCoins(itemPrice); // Purchase Item

            ShopItemsList[itemIndex].IsPurchased = true; // Disable Button
            
            gameReference.Coins -= itemPrice;

            buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
            DisableBuyBtn();

            // Change UI Text: Coins
            gameReference.UpdateAllCoinsUIText();
        }
        else
        {
            NoCoinsAnim.SetTrigger("NoCoins");
            Debug.Log("Not Enough Coins..");
        }
        

    }
    // If the chosen item is already bought, then it cannot be bought again and is shown as "ACQUIRED"
    void DisableBuyBtn()
    {
        buyBtn.interactable = false;
        buyBtn.transform.GetChild(0).GetComponent<Text>().text = "ACQUIRED";
    }

   
}
