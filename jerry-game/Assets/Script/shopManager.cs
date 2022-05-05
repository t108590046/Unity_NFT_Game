using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopManager : MonoBehaviour
{
    GameObject ItemTemplate;
    GameObject newProduct;
    [SerializeField] Transform ShopScrollView;

    void Start()
    {
        ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        
        for(int i=0;i<10;i++)
        {
            newProduct = Instantiate(ItemTemplate,ShopScrollView);
        }
        Destroy(ItemTemplate);
    }

}
