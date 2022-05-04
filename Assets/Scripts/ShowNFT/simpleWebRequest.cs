using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class simpleWebRequest : MonoBehaviour
{
    // heruko server url = "https://secret-oasis-58410.herokuapp.com"
    const string hostName = "http://localhost:8000";
    // player attributes
    public class Player 
    {
        public string userId;
        public string tokenId;
        public string balanceOf;
    }
    // Start is called before the first frame update 
    void Start()
    {
        StartCoroutine(GetWebData("0"));
    }
    //向伺服器發送request
    IEnumerator GetWebData(string userId)
    {
        UnityWebRequest www = UnityWebRequest.Get(hostName + "/player/" + userId);
        yield return www.SendWebRequest();
        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("something went wrong " + www.error);
        }
        else
        {
            string outputJson = www.downloadHandler.text;
            //Player deserializedPlayer = JsonConvert.DeserializeObject<Player>(outputJson);
            //Debug.Log("tokenId : " + deserializedPlayer.tokenId);
           
            Debug.Log(outputJson);
        }
    }
}
