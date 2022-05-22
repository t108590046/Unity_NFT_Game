using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
    private string method;
   [SerializeField] Button runBtn;
   [SerializeField] InputField input;
   [SerializeField] Text showText;
    void Start()
    {
        var dropdown = transform.GetComponent<Dropdown>();
        //dropdown.options.Clear();

        List<string> items = new List<string>();
        items.Add("balanceOf");
        items.Add("Mint");
        items.Add("GetSubTokens");
        items.Add("Separate");
        items.Add("SeparateOne");
        items.Add("GetAllNFTTokenID");
        items.Add("Combine");

        foreach(var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData(){text = item});
        }

        DropdownItemSelected(dropdown);
        dropdown.RefreshShownValue();
        dropdown.onValueChanged.AddListener(delegate {DropdownItemSelected(dropdown);});
        runBtn.onClick.AddListener(delegate {RunMethod();});
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        method = dropdown.options[index].text;
        switch (method)
        {
            case "Mint":
                input.placeholder.GetComponent<Text>().text = "Enter New TokenID...";
                break;
            case "GetSubTokens":
                input.placeholder.GetComponent<Text>().text = "Enter your NFT TokenID";
                break;
            case "Separate":
                input.placeholder.GetComponent<Text>().text = "Enter your NFT TokenID";
                break;
            case "Combine":
                input.placeholder.GetComponent<Text>().text = "Enter NFT-TokenID,SubTokenID1,...";
                break;
            case "SeparateOne":
                input.placeholder.GetComponent<Text>().text = "Enter NFT-TokenID,SubTokenID";
                break;
            default:
                input.placeholder.GetComponent<Text>().text ="";
                break;
        }
    }

    void RunMethod()
    {
        switch (method)
        {
            case "balanceOf":
                ShowBalanceOf();
                break;
            case "Mint":
                Mint();
                break;
            case "GetSubTokens":
                ShowSubTokens();
                break;
            case "Separate":
                 Separate();
                break;
            case "GetAllNFTTokenID":
                 GetAllNFTTokenID();
                break;
            case "Combine":
                 Combine();
                break;
            case "SeparateOne":
                 SeparateOne();
                break;
            default:
                break;
        }
    }

   async void ShowBalanceOf()
    {
        int balance = await ERC721.BalanceOf(ContractInfo.chain, ContractInfo.network, ContractInfo.contract, ContractInfo.account);
        showText.text = "您目前擁有的NFT數量是: " + balance;
    }

    async void Mint()
    {
        string inputTokenID = input.text;
        string response = await ERC3664.mint(ContractInfo.account, inputTokenID);
        if (response.StartsWith("0x") && response.Length == 66)
        {
            showText.text = "Mint NFT 成功 !!";
        }
        else
        {
            showText.text = "Mint NFT 失敗 !!";
        }
    }

    async void ShowSubTokens()
    {
        string inputTokenID = input.text;
        List<int> responses = await ERC3664.GetSubTokens(inputTokenID);
        if(responses.Count == 0) showText.text = "Error!! Try Again";
        else 
        {
            showText.text = "您的NFT擁有的配件ID是: ";
            for (int i = 0; i < responses.Count; i++)
            {
                if(i == 0)  showText.text += responses[i];
                else showText.text += ","+ responses[i];
            }
        }
    }

    async void Separate()
    {
        string inputTokenID = input.text;
        string response = await ERC3664.Separate(inputTokenID);
        if (response.StartsWith("0x") && response.Length == 66)
        {
            showText.text = "Separate NFT 成功 !!";
        }
        else
        {
            showText.text = "Separate NFT 失敗 !!";
        }
        
    }

    async void GetAllNFTTokenID()
    {
        List<string> responses = await ERC3664.GetAllNFTTokenID();
        if(responses.Count == 0) showText.text = "Error!! Try Again";
        else 
        {
            showText.text = "您的NFT-TokenIDs: ";
            for (int i = 0; i < responses.Count; i++)
            {
                if(i == 0)  showText.text += responses[i];
                else showText.text += ","+ responses[i];
            }
        }
    }

    async void Combine()
    {
        string inputData = input.text;
        char [] c = {','};
        string[] tokenIds = inputData.Split(c,2);
        string nftTokenID = tokenIds[0];
        string []subTokenIds = tokenIds[1].Split(c);
        string response = await ERC3664.Combine(nftTokenID, subTokenIds);
        if (response.StartsWith("0x") && response.Length == 66)
        {
            showText.text = "Combine NFT 成功 !!";
        }
        else
        {
            showText.text = "Combine NFT 失敗 !!";
        }
    }

    async void SeparateOne()
    {
        string inputData = input.text;
        char [] c = {','};
        string[] tokenIds = inputData.Split(c,2);
        string nftTokenID = tokenIds[0];
        string subTokenId = tokenIds[1];
        string response = await ERC3664.SeparateOne(nftTokenID, subTokenId);
        if (response.StartsWith("0x") && response.Length == 66)
        {
            showText.text = "SeparateOne  成功 !!";
        }
        else
        {
            showText.text = "SeparateOne  失敗 !!";
        }
    }
}