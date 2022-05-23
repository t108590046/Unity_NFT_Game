using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopManager : MonoBehaviour
{
    [System.Serializable] class ShopItem
    {
        public Sprite Image;
        public int Price;
        public bool IsPurchased = false;
    }

    [SerializeField] List<ShopItem> ShopItemsList;


    GameObject ItemTemplate;
    GameObject newProduct;
    [SerializeField] Transform ShopScrollView;
    [SerializeField] Text CoinsText;
    Button buyBtn;

    void Start()
    {
        ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        
        int len = ShopItemsList.Count;
        for(int i = 0; i < len; i++)
        {
            newProduct = Instantiate(ItemTemplate,ShopScrollView);
            newProduct.transform.GetChild(0).GetComponent<Image> ().sprite = ShopItemsList[i].Image;
            newProduct.transform.GetChild(2).GetComponent<Text> ().text = ShopItemsList[i].Price.ToString();
            buyBtn = newProduct.transform.GetChild(3).GetComponent<Button>();
            buyBtn.interactable = !ShopItemsList[i].IsPurchased;
            buyBtn.AddEventListener(i,OnShopItemBtnClicked);
        }
        Destroy(ItemTemplate);

        SetCoinsUI();
    }

    void OnShopItemBtnClicked(int itemIndex)
    {
        if(coin.Instance.HasEnoughCoins(ShopItemsList[itemIndex].Price))
        {
            coin.Instance.UseCoins(ShopItemsList[itemIndex].Price);
            ShopItemsList[itemIndex].IsPurchased = true;

            buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(3).GetComponent<Button>();
            buyBtn.interactable = false;
            buyBtn.transform.GetChild(0).GetComponent<Text> ().text ="PURCHASED";
            SetCoinsUI();
        }
        else
        {
            Debug.Log("You don't have enough coins!!");
        }
    }
    
    void SetCoinsUI()
    {
        CoinsText.text = coin.Instance.Coins.ToString();
    }

}
