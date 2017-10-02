using UnityEngine;
using System.Collections;

public class loading_screen : MonoBehaviour {

    public float currentCoolDown;
	// Use this for initialization
	void Start () {
        currentCoolDown = 3;
	}
	
	// Update is called once per frame
	void Update () {
        if (currentCoolDown < 0)
        {
            currentCoolDown = 0;
        }

        if (currentCoolDown > 0)
        {
            currentCoolDown -= Time.deltaTime;
        }

        if(currentCoolDown == 0)
        {
            Global.lvl = Global.lvl + 1;
            Application.LoadLevel(Global.lvl);
        }
        if (Input.anyKey)
        {
            currentCoolDown = 0;
        }
    }
}
