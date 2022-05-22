using System.Collections;
using System.Numerics;
using System;
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
        string[] obj = { _toAccount , _tokenId };
        string args = JsonConvert.SerializeObject(obj);
        string data = await EVM.CreateContractData(ContractInfo.abi, method, args);
        Debug.Log(data);
        string chainId = await EVM.ChainId(ContractInfo.chain, ContractInfo.network, ContractInfo.rpc);
        // send transaction
        string response = await Web3Wallet.SendTransaction(chainId, ContractInfo.contract, value, data, ContractInfo.gasLimit, ContractInfo.gasPrice);
        Debug.Log(response);
        return response;
    }

    public static async Task<List<int>> GetSubTokens(string _tokenId)
    {
        string method = "getSubTokens";
        string[] obj = { _tokenId };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(ContractInfo.chain, ContractInfo.network, ContractInfo.contract, ContractInfo.abi, method, args, ContractInfo.rpc);
        try 
        {
            string[] responses = JsonConvert.DeserializeObject<string[]>(response);
            List<int> tokenIds = new List<int>();
            for (int i = 0; i < responses.Length; i++)
            {
                tokenIds.Add(int.Parse(responses[i]));
            }
            return tokenIds;
        } 
        catch 
        {
            Debug.LogError(response);
            throw;
        }
    }
    public static async Task<string> Separate(string _tokenId)
    {
        string method = "separate";
        // amount of wei to send
        string value = "0"; 
        string[] obj = { _tokenId };
        string args = JsonConvert.SerializeObject(obj);
        string data = await EVM.CreateContractData(ContractInfo.abi, method, args);
        string chainId = await EVM.ChainId(ContractInfo.chain, ContractInfo.network, ContractInfo.rpc);
        // send transaction
        string response = await Web3Wallet.SendTransaction(chainId, ContractInfo.contract, value, data, ContractInfo.gasLimit, ContractInfo.gasPrice);
        Debug.Log(response);
        return response;
    }

    public static async Task<string> SeparateOne(string _tokenId, string subTokenId)
    {
        string method = "separateOne";
        // amount of wei to send
        string value = "0"; 
        string[] obj = { _tokenId, subTokenId};
        string args = JsonConvert.SerializeObject(obj);
        string data = await EVM.CreateContractData(ContractInfo.abi, method, args);
        string chainId = await EVM.ChainId(ContractInfo.chain, ContractInfo.network, ContractInfo.rpc);
        // send transaction
        string response = await Web3Wallet.SendTransaction(chainId, ContractInfo.contract, value, data, ContractInfo.gasLimit, ContractInfo.gasPrice);
        Debug.Log(response);
        return response;
    }

    public static async Task<List<string>> GetAllNFTTokenID()
    {
        string method = "tokenOfOwnerByIndex";
        int numberOfNFT = await ERC721.BalanceOf(ContractInfo.chain,ContractInfo.network,ContractInfo.contract,ContractInfo.account,ContractInfo.rpc);
        string[][] obj = new string[numberOfNFT][];
        for (int i = 0; i < numberOfNFT; i++)
        {
            obj[i] = new string[2] {ContractInfo.account, i.ToString() };
        };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.MultiCall(ContractInfo.chain,ContractInfo.network,ContractInfo.contract, ContractInfo.abi, method, args, "", ContractInfo.rpc);
        try 
        {
            string[] responses = JsonConvert.DeserializeObject<string[]>(response);
            List<string> TokenIDs = new List<string>();
            for (int i = 0; i < responses.Length; i++)
            {
                string tokenId = responses[i].Substring(2);
                tokenId = Convert.ToInt32(tokenId, 16).ToString();
                TokenIDs.Add(tokenId);
            }
            return TokenIDs;
        } 
        catch 
        {
            Debug.LogError(response);
            throw;
        }  
    }

    public static async Task<string> Combine(string _tokenId, string[] _subTokenIds)
    {
        string method = "combine";
        string value = "0"; 
        int[][] obj = new int[2][];
        obj[0] = new int [1]{int.Parse(_tokenId)};
        obj[1] = Array.ConvertAll(_subTokenIds, s => int.Parse(s));
        string args = JsonConvert.SerializeObject(obj);
        Debug.Log(args);
        string data = await EVM.CreateContractData(ContractInfo.abi, method, args);
        string chainId = await EVM.ChainId(ContractInfo.chain, ContractInfo.network, ContractInfo.rpc);
        // send transaction
        string response = await Web3Wallet.SendTransaction(chainId, ContractInfo.contract, value, data, ContractInfo.gasLimit, ContractInfo.gasPrice);
        Debug.Log(response);
        return response;
    }
    

}

