using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    async void Start()
    {
        int balance = await ERC721.BalanceOf(ContractInfo.chain, ContractInfo.network, ContractInfo.contract, ContractInfo.account);
        GetComponent<Text>().text += " " + balance.ToString();
    }



}
