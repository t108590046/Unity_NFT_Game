using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Bag",menuName = "BagSystem/New Item")]

public class item : ScriptableObject //會存信息的
{
    public string itemName;
    public Sprite itemImage;
    public int itemHeld;
    [TextArea] 
    public string itemInfo;
 

}
