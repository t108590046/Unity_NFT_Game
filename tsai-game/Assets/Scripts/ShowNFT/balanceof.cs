using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

public class balanceof : MonoBehaviour
{
    [SerializeField] private Text num;
    async public void showBalanceOf()
    {
        string chain = "ethereum";
        string network = "rinkeby";
        string contract = "0x6Fe543EE03940F5d2Cf3D69BB967005CE03723B5";
        string account = PlayerPrefs.GetString("Account");
        Debug.Log(account);

        int balance = await ERC721.BalanceOf(chain, network, contract, account);
        num.text = "" + balance;

    }
}
