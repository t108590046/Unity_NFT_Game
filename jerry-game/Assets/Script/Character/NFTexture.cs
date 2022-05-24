using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text;
using Newtonsoft.Json;
using Unity.VectorGraphics;
using System.IO;


public class NFTexture : MonoBehaviour
{
    [SerializeField]Text inputTokenID;
    [SerializeField]Button showBtn;
    [SerializeField]SVGImage NFT;
    public VectorUtils.TessellationOptions tesselationOptions;
    public class Response {
    public string name;
    public string description;
    public string image;
    }
    
    void Start()
    {
        tesselationOptions.MaxCordDeviation = float.MaxValue;
        tesselationOptions.MaxTanAngleDeviation = float.MaxValue;
        tesselationOptions.StepDistance = 1f;
        tesselationOptions.SamplingStepSize = 0.1f;
        showBtn.onClick.AddListener(delegate {ShowNFT();});
    }
    async void ShowNFT()
    {
        print(inputTokenID.text);
        // fetch uri from chain
        string uri = await ERC721.URI(ContractInfo.chain, ContractInfo.network, ContractInfo.contract,inputTokenID.text);
        // fetch json from uri
        byte[] decodedBytes = Convert.FromBase64String (uri.Substring(29));
        string decodedText = Encoding.UTF8.GetString (decodedBytes);
        
        Response data = JsonConvert.DeserializeObject<Response>(decodedText);
        string imageBase64 = data.image.Substring(26);
        byte[] imagedecodedBytes = Convert.FromBase64String (imageBase64);
        string imagedecodedText = Encoding.UTF8.GetString (imagedecodedBytes);
        print(imagedecodedText);
        using (StringReader reader = new StringReader(imagedecodedText))
         {
             SVGParser.SceneInfo sceneInfo = SVGParser.ImportSVG(reader);
             List<VectorUtils.Geometry> geoms = VectorUtils.TessellateScene(sceneInfo.Scene, tesselationOptions);
 
             // Build a sprite with the tessellated geometry.
             Sprite sprite = VectorUtils.BuildSprite(geoms, 100.0f, VectorUtils.Alignment.Center, Vector2.zero, 128, true);
             NFT.sprite = sprite;
         }
        /*
        // parse json to get image uri
       
        print("imageUri: " + imageUri);

        // fetch image and display in game
        UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(imageUri);
        await textureRequest.SendWebRequest();
        this.gameObject.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)textureRequest.downloadHandler).texture;
        */
    }
    
}




