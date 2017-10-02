using UnityEngine;
using System.Collections;

public class darky : MonoBehaviour {

    GameObject g;

	// Use this for initialization
	void Start () {
        g = GameObject.Find("Darkness");
        GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.8f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
