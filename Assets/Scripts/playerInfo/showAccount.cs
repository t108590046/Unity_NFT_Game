using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showAccount : MonoBehaviour
{
    public Text account;
    // Start is called before the first frame update
    void Start()
    {
        account.text = PlayerPrefs.GetString("Account");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
