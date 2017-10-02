using UnityEngine;
using System.Collections;

public class loadMenu : MonoBehaviour {
    public float currentCoolDown;
	// Use this for initialization
	void Start () {
        currentCoolDown = 192;
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
        if (currentCoolDown == 0)
        {
            Application.LoadLevel(1);
        }
        if (Input.anyKey)
        {
            Application.LoadLevel(1);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Application.LoadLevel(1);
        }
    }
}
