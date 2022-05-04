using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;


//Used by metadata class for storing attributes
public class Attributes
{
    //The type or name of a given trait
    public string trait_type;
    //The value associated with the trait_type
    public string value;
}

public class NFT_Attributes
{
    public string tokenId{ get; set; }
    public string name{ get; set; }
    public string satiety{ get; set; }
    public string Favorability{ get; set; }
}

//Used for storing NFT metadata from standard NFT json files
public class Metadata
{
    //List storing attributes of the NFT
    public List<Attributes> attributes { get; set; }
    //Description of the NFT
    public string description { get; set; }
    //An external_url related to the NFT (often a website)
    public string external_url { get; set; }
    //image stores the NFTs URI for image NFTs
    public string image { get; set; }
    //Name of the NFT
    public string name { get; set; }
}

//Interacting with blockchain
public class ShowNFT : MonoBehaviour
{
    //The chain to interact with, using Polygon here
     string chain = "ethereum";
    //The network to interact with (mainnet, testnet)
     string network = "rinkeby";
    //Contract to interact with, contract below is "Project: Pigeon Smart Contract"
     string contract = "0x6Fe543EE03940F5d2Cf3D69BB967005CE03723B5";
    //Token ID to pull from contract
    public Text input_tokenId;
    public Text name_text;
    public Text satiety_text;
    public Text Favorability_text;
    //Used for storing metadata
    Metadata metadata;
    NFT_Attributes nft_attributes;
    //show on gameobject
    public GameObject NFT;

    string ipfsURI = "https://gateway.pinata.cloud/ipfs/";
    string img ;

    async public void GetNFTImage()
    {
        string tokenId = input_tokenId.text.ToString();
        //Interacts with the Blockchain to find the URI related to that specific token
        string URI = await ERC721.URI(chain, network, contract, tokenId);
        URI = ipfsURI + URI.Substring(7);
        Debug.Log(URI);
        //Perform webrequest to get JSON file from URI
        using (UnityWebRequest webRequest = UnityWebRequest.Get(URI))
        {
            try
            {
                //Sends webrequest
                await webRequest.SendWebRequest();
                //Gets text from webrequest
                string metadataString = webRequest.downloadHandler.text;
                //Converts the metadata string to the Metadata object
                metadata = JsonConvert.DeserializeObject<Metadata>(metadataString);
                img = ipfsURI + metadata.image.Substring(7);
                Debug.Log(img);
            }
            catch(Exception ex)
            {
                 Debug.Log("Exception Message: " + ex.Message);
            }
        }

        //Performs another web request to collect the image related to the NFT
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(img))
        {
            try
            {
                //Sends webrequest
                await webRequest.SendWebRequest();
                //Gets the image from the web request and stores it as a texture
                Texture texture = DownloadHandlerTexture.GetContent(webRequest);
                //Sets the objects main render material to the texture
                NFT.GetComponent<MeshRenderer>().material.mainTexture = texture;
            }
            catch(Exception ex)
            {
                Debug.Log("Exception Message: " + ex.Message);
            }

        }
        string attributeURI = "https://secret-oasis-58410.herokuapp.com/player/getAttributes/" + tokenId;
         using (UnityWebRequest webRequest = UnityWebRequest.Get(attributeURI))
        {
            try
            {
                //Sends webrequest
                await webRequest.SendWebRequest();
                //Gets text from webrequest
                string metadataString = webRequest.downloadHandler.text;
                //Converts the metadata string to the Metadata object
                nft_attributes = JsonConvert.DeserializeObject<NFT_Attributes>(metadataString);
                Debug.Log(metadataString);
                satiety_text.text = nft_attributes.satiety + '%';
                Favorability_text.text = nft_attributes.Favorability + '%';
                name_text.text = nft_attributes.name;
            }
            catch(Exception ex)
            {
                 Debug.Log("Exception Message: " + ex.Message);
            }
        }
    }
}

