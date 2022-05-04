using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class homeGameManager : MonoBehaviour
{
    public bool backgroundMusicOn = true;
    public GameObject soundButtonImage;
    public GameObject settingBox;
    public GameObject infoBox;
    protected AudioSource backgroundMusic;

    
    public virtual void Start()
    {
        backgroundMusic = GetComponent<AudioSource>();
        settingBox = GameObject.Find("setting");
        infoBox = GameObject.Find("info");
        settingBox.SetActive(false);
        infoBox.SetActive(false);
    }
    
    
    public void onStartGame(string scneneName)
    {
       SceneManager.LoadScene(scneneName); //讀取場景(場景名稱)
    }

    public void soundButtomClick()
    {
        this.backgroundMusicOn = !backgroundMusicOn;
        Image soundBtnImg = soundButtonImage.GetComponent<Image>();

        if (backgroundMusicOn)
        {
            backgroundMusic.Play();
            soundBtnImg.sprite = Resources.Load<Sprite>("new/speaker");
        }
        else
        {
            backgroundMusic.Pause();
            soundBtnImg.sprite = Resources.Load<Sprite>("new/x");
        }
    }

    public void settingButtomClick()
    {
        settingBox.SetActive(true);
    }

    public void infoButtomClick()
    {
        infoBox.SetActive(true);
    }

    public void settingBoxClose()
    {
        settingBox.SetActive(false);
    }

    public void infoBoxClose()
    {
        infoBox.SetActive(false);
    }
}
