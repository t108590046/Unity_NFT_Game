using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Bag", menuName = "BagSystem/New Bag")]

public class bag : ScriptableObject
{
    public List<item> itemList = new List<item>();


}
