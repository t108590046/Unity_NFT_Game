using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myShopManager : MonoBehaviour
{
    public bag playerBag;
    public item thisItem;
    public Text shopItemInfo;
    public Text CoinsRemain;
    public coin playerCoin;
    public Text notice;
    public GameObject alert;
    

    public void buyNewItem()
    {
        if (playerCoin.coinRemain >= thisItem.itemPrice)
        {
            playerCoin.coinRemain -= thisItem.itemPrice;
            CoinsRemain.text = playerCoin.coinRemain.ToString();
            if (!playerBag.itemList.Contains(thisItem))
            {
                playerBag.itemList.Add(thisItem);
                thisItem.itemHeld += 1;
            }
            else
            {
                thisItem.itemHeld += 1;
            }
            bagManager.refreshItem();
        }
        else
        {
            alert.SetActive(true);
            notice.text = "ª÷¹ô¾lÃB¤£¨¬!";
        }
    }

    public void shopItemImgClick()
    {
        shopItemInfo.text = thisItem.itemInfo;
    }

}



