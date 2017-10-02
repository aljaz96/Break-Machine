using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoBack : MonoBehaviour {

    public Text[] ScoreText = new Text[10];
    public Text[] NameText = new Text[10];
    // Use this for initialization
    void Start () {
	    if(Application.loadedLevel == 5)
        {
            ScoreText[0].text = "Score: " + Global.highscores[0].ToString();
            ScoreText[1].text = "Score: " + Global.highscores[1].ToString();
            ScoreText[2].text = "Score: " + Global.highscores[2].ToString();
            ScoreText[3].text = "Score: " + Global.highscores[3].ToString();
            ScoreText[4].text = "Score: " + Global.highscores[4].ToString();
            ScoreText[5].text = "Score: " + Global.highscores[5].ToString();
            ScoreText[6].text = "Score: " + Global.highscores[6].ToString();
            ScoreText[7].text = "Score: " + Global.highscores[7].ToString();
            ScoreText[8].text = "Score: " + Global.highscores[8].ToString();
            ScoreText[9].text = "Score: " + Global.highscores[9].ToString();

            NameText[0].text = "Player: " + Global.names[0].ToString();
            NameText[1].text = "Player: " + Global.names[1].ToString();
            NameText[2].text = "Player: " + Global.names[2].ToString();
            NameText[3].text = "Player: " + Global.names[3].ToString();
            NameText[4].text = "Player: " + Global.names[4].ToString();
            NameText[5].text = "Player: " + Global.names[5].ToString();
            NameText[6].text = "Player: " + Global.names[6].ToString();
            NameText[7].text = "Player: " + Global.names[7].ToString();
            NameText[8].text = "Player: " + Global.names[8].ToString();
            NameText[9].text = "Player: " + Global.names[9].ToString();
        }
	}
	
	// Update is called once per frame
	void Update () {
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
