using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERC721URIExample : MonoBehaviour
{
    async void Start()
    {
        string chain = "ethereum";
        string network = "rinkeby";
        string contract = "0x5812693d19B203175F4b888312F52FD0e1FDd992";
        string tokenId = "1";

        string uri = await ERC721.URI(chain, network, contract, tokenId);
        print(uri);
    }
}
