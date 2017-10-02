using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {

    // Use this for initialization
    public static float score = 0;
    public static int attempts = 4;
    public static int lvl = 6;
    public static float[] highscores = new float[11];
    public static string[] names = new string[11];
    public static bool music = true;
    public static bool sound = true;

    void Start () {
        highscores[9] = 1000;
        highscores[8] = 22000;
        highscores[7] = 23000;
        highscores[6] = 24000;
        highscores[5] = 25000;
        highscores[4] = 26000;
        highscores[3] = 27000;
        highscores[2] = 28000;
        highscores[1] = 29000;
        highscores[0] = 30000;
        names[0] = "Urby";
        names[1] = "Bobby";
        names[2] = "Steve";
        names[3] = "John";
        names[4] = "Jezus";
        names[5] = "Krefl";
        names[6] = "Bob";
        names[7] = "Mei";
        names[8] = "Luke";
        names[9] = "Bubbles";
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
