using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class savePrivateKey : MonoBehaviour
{
    public Text privateKey;
    public Text privateKey_input;
    public void save()
    {
        PlayerPrefs.SetString("PrivateKey", privateKey_input.text);
    }
    public void showPrivateKey()
    {
        Debug.Log(PlayerPrefs.GetString("PrivateKey"));
        privateKey.text = PlayerPrefs.GetString("PrivateKey");
    }

}
