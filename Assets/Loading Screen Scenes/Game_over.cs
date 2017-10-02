using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game_over : MonoBehaviour {

    // Use this for initialization
    GUIStyle myStyle = new GUIStyle();
    float currentCooldown = 0;
    public Text finalscore;
    public AudioSource s = new AudioSource();

    void Start()
    {
        if (Global.music == true)
        {
            s.Play();
        }
        currentCooldown = 5;
        myStyle.fontSize = 1;
    }

    // Update is called once per frame
    void Update()
    {
        finalscore.text = "Final Score: " + Global.score.ToString();
        if (currentCooldown < 0)
        {
            currentCooldown = 0;
        }

        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (currentCooldown == 0)
        {
            if (Global.score > Global.highscores[9])
            {
                Application.LoadLevel(19);
            }
            else
            {
                Global.lvl = 6;
                Global.attempts = 4;
                Global.score = 0;
                Application.LoadLevel(1);
            }
        }
    }
}
