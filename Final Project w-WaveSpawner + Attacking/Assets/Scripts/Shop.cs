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

    // Start is called before the first frame update
    void Start()
    {
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
    void OnShopItemBtnClicked(int itemIndex)
    {
        if (Game.Instance.HasEnoughCoins(ShopItemsList[itemIndex].Price))
        {
            Game.Instance.UseCoins(ShopItemsList[itemIndex].Price);
            // Purchase Item
            ShopItemsList[itemIndex].IsPurchased = true;
            // Disable Button
            buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
            DisableBuyBtn();

            // Change UI Text: Coins
            Game.Instance.UpdateAllCoinsUIText();
        }
        else
        {
            NoCoinsAnim.SetTrigger("NoCoins");
            Debug.Log("Not Enough Coins..");
        }
        

    }

    void DisableBuyBtn()
    {
        buyBtn.interactable = false;
        buyBtn.transform.GetChild(0).GetComponent<Text>().text = "PURCHASED";
    }

   
    
   
}
