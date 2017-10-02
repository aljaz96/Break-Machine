using UnityEngine;
using System.Collections;

public class Projectile_boss : MonoBehaviour {

    public float speed = 100f;
    public GameObject pew;
    public GameObject platf;
    public float currentSpeed;
    public GameObject ball;
    public žoga_boss ž;
    public platform_boss p;


    // Use this for initialization
    void Start()
    {
        ball = GameObject.Find("white ball");
        platf = GameObject.Find("Platform");
        p = platf.gameObject.GetComponent<platform_boss>() as platform_boss;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        ž = ball.gameObject.GetComponent<žoga_boss>() as žoga_boss;
        if (this.name != "Projectile")
            this.gameObject.tag = "Projectile";
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = GetComponent<Rigidbody2D>().velocity.y;
        if (currentSpeed < speed)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BOSS")
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.name == "TOP")
        {
            Destroy(this.gameObject);
        }
    }
}
