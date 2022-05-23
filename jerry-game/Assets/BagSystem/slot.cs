using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slot : MonoBehaviour
{
    public item slotItem;
    public Image slotImage;
    public Text slotNum;


    public void bagItemOnClick()
    {
        bagManager.updateItemInfo(slotItem.itemInfo);
    }

    public void bagUseBtnClick()
    {
        slotItem.itemHeld -= 1;
        bagManager.increaseIntimacy(slotItem);
        bagManager.refreshItem();
        if (slotItem.itemHeld == 0)
        {
            bagManager.destoryItem(slotItem);
        }
    }
}
