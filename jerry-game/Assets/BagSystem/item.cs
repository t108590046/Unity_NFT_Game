using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newItem",menuName = "BagSystem/New Item")]

public class item : ScriptableObject //�|�s�H����
{
    public string itemName;
    public Sprite itemImage;
    public int itemHeld;
    [TextArea] 
    public string itemInfo;
    public int itemPrice;
    public int itemEffect;//�W�[�˱K�ת���
}
