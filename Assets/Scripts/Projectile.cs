using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float speed = 50f;
    public GameObject pew;
    public GameObject platf;
    public float currentSpeed;
    public GameObject ball;
    public žoga ž;
    public platform p;
    public AudioSource explo = new AudioSource();

    // Use this for initialization
    void Start () {
        platf = GameObject.Find("Platform");
        p = platf.gameObject.GetComponent<platform>() as platform;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        ball = GameObject.Find("white ball");
        ž = ball.gameObject.GetComponent<žoga>() as žoga;
        if(this.name != "Projectile")
        this.gameObject.tag = "Projectile";
    }
	
	// Update is called once per frame
	void Update () {
        if(Global.sound == false)
        {
            explo.mute = true;
        }
        if (Global.sound == true)
        {
            explo.mute = false;
        }
        currentSpeed = GetComponent<Rigidbody2D>().velocity.y;
        if (currentSpeed < speed)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Block")
        {
            ž.chance = Random.Range(0, 100);
            if (ž.chance <= 25)
            {
                ž.blokXpos = other.transform.position.x;
                ž.blokYpos = other.transform.position.y;
                ž.dropPowerUp();
            }
            ž.afterShot = true;
            Destroy(other.gameObject);
            explo.Play();
            Global.score = Global.score + 50;
            p.blocks = p.blocks - 1;
            ž.IzpisiText();
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Gray Block")
        {
            Block_2_hit block_hp = other.gameObject.GetComponent<Block_2_hit>() as Block_2_hit;
            ž.afterShot = true;
            if (block_hp.hp < 1)
            {
                ž.chance = Random.Range(0, 100);
                if (ž.chance <= 25)
                {
                    ž.blokXpos = other.transform.position.x;
                    ž.blokYpos = other.transform.position.y;
                    ž.dropPowerUp();
                }
                Destroy(other.gameObject);
                explo.Play();
                Global.score = Global.score + 50;
                p.blocks = p.blocks - 1;
                ž.IzpisiText();
            }
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Monster")
        {
            Global.score = Global.score + 200;
            monster m = other.gameObject.GetComponent<monster>() as monster;
            Destroy(this.gameObject);
            explo.Play();
        }
        if(other.gameObject.name == "TOP")
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Invincible")
        {
            Destroy(this.gameObject);
        }
    }
    }
