using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bagManager : MonoBehaviour
{
    static bagManager instance;

    public bag mybag;
    public GameObject slotGrid;
    public slot slotPrefab;
    public Text itemInfo;
    public Text intimacyText;
    public intimacy myIntimacy;

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    
    private void OnEnable()
    {
        refreshItem();
    }

    public static void updateItemInfo(string itemDescripition)//更新物品介紹
    {
        instance.itemInfo.text = itemDescripition;
    }

    public static void createNewItem(item item)//介面中新增slot
    {
        slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.gameObject.transform.localScale = new Vector3(1, 1, 1);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotNum.text = item.itemHeld.ToString();
    }

    public static void destoryItem(item item)//刪除slot
    {
        for (int i = 0; i < instance.mybag.itemList.Count; i++)
        {
            if (instance.mybag.itemList[i] == item)
            {
                Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
                instance.mybag.itemList.RemoveAt(i);
            }
        }
        refreshItem();
    }

    public static void refreshItem() //刷新介面
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
            {
                break;
            }
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < instance.mybag.itemList.Count; i++)
        {
            createNewItem(instance.mybag.itemList[i]);
        }
    }

    public static void increaseIntimacy(item item)
    {
        instance.myIntimacy.currentIntimacy += item.itemEffect;
        instance.intimacyText.text = instance.myIntimacy.currentIntimacy.ToString();
    }

}
