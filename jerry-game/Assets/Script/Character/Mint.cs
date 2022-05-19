using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mint : MonoBehaviour
{
    [SerializeField] Text input_tokenId;
    async public void ClickMint()
    {
        Debug.Log(input_tokenId.text);
        string response = await ERC3664.mint(ContractInfo.account, input_tokenId.text);
    }

}
