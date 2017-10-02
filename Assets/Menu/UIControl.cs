using UnityEngine;
using System.Collections;

public class UIControl : MonoBehaviour {

    // Use this for initialization
    public AudioSource s = new AudioSource();

    void Start()
    {
        
    }

    void Update()
    {
        if(Global.music == false)
        {
            s.mute = true;
        }
        if (Global.music == true)
        {
            s.mute = false;
        }
    }

    public void StartGame()
    {
        Application.LoadLevel(6);
    }
    public void OpenOptions()
    {
        Application.LoadLevel(2);
    }
    public void OpenInformations()
    {
        Application.LoadLevel(3);
    }
    public void OpenHistory()
    {
        Application.LoadLevel(4);
    }
    public void OpenLeaderboards()
    {
        Application.LoadLevel(5);
    }
    public void GoBack()
    {
        Application.LoadLevel(1);
    }
    public void CloseGame()
    {
        Application.Quit();
    }
    public void MusicOn()
    {
        Global.music = true;
    }
    public void MusicOff()
    {
        Global.music = false;
    }

    public void SoundOn()
    {
        Global.sound = true;
    }
    public void SoundOff()
    {
        Global.sound = false;
    }

}
