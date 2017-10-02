using UnityEngine;
using System.Collections;

public class musiq : MonoBehaviour {

    // Use this for initialization
    public AudioSource s = new AudioSource();

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Global.music == true)
        {
            s.mute = false;
        }
        if(Global.music == false)
        {
            s.mute = true;
        }
	}
}
