using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bagManager : MonoBehaviour
{
    static bagManager instance;

    public bag Mybag;
    public GameObject slotGrid;
    public slot slotPrefab;
    public TextAlignment itemInfo;

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

}
