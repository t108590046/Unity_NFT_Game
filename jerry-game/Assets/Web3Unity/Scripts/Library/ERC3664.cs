using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class ERC3664 
{
    
    public static async Task<string> mint(string _toAccount,string _tokenId)
    {
        string method = "mint";
        // amount of wei to send
        string value = "5000000000000000"; //0.005 ether
        string rpc = "";
        string[] obj = { _toAccount , _tokenId };
        string args = JsonConvert.SerializeObject(obj);
        string data = await EVM.CreateContractData(ContractInfo.abi, method, args);
        Debug.Log(data);
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";
        string chainId = await EVM.ChainId(ContractInfo.chain, ContractInfo.network, rpc);
        Debug.Log(chainId);
        // send transaction
        string response = await Web3Wallet.SendTransaction(chainId, ContractInfo.contract, value, data, gasLimit, gasPrice);
        Debug.Log(response);
        return response;
    }

}

