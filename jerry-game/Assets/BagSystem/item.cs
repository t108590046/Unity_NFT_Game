using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newItem",menuName = "BagSystem/New Item")]

public class item : ScriptableObject //會存信息的
{
    public string itemName;
    public Sprite itemImage;
    public int itemHeld;
    [TextArea] 
    public string itemInfo;
    public int itemPrice;
    public int itemEffect;//增加親密度的值
}
