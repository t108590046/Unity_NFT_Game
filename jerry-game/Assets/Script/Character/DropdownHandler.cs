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
        
        dropdown.options.Clear();

        List<string> items = new List<string>();
        items.Add("balanceOf");
        items.Add("Mint");
        items.Add("GetSubTokens");
        items.Add("Separate");

        foreach(var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData(){text = item});
        }

        DropdownItemSelected(dropdown);
        dropdown.onValueChanged.AddListener(delegate {DropdownItemSelected(dropdown);});
        runBtn.onClick.AddListener(delegate {RunMethod();});
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        method = dropdown.options[index].text;
        switch (method)
        {
            case "balanceOf":
                input.placeholder.GetComponent<Text>().text = "";
                break;
            case "Mint":
                input.placeholder.GetComponent<Text>().text = "Enter New TokenID...";
                break;
            case "GetSubTokens":
                input.placeholder.GetComponent<Text>().text = "Enter your NFT TokenID";
                break;
            case "Separate":
                input.placeholder.GetComponent<Text>().text = "Enter your NFT TokenID";
                break;
            default:
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
}