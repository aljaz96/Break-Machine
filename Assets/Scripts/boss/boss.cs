using UnityEngine;
using System.Collections;

public class boss : MonoBehaviour {

    // Use this for initialization
    public GameObject b;
    public int hp;
    public GameObject m;
    public float speed = 2f;
    public float currentCoolDown = 1;
    public bool moving;
    public float currentSpeed;
    public int wtf = 0;
    public int monsters = 11;
    public int direction;
    public GameObject pew;
    public laser_ball_boss shock;
    public bool alive;
    bool gameOver;

    void Start () {
        moving = false;
        alive = true;
        b = GameObject.Find("BOSS");
        hp = 45;
        gameOver = false;
        pew = GameObject.Find("BP1");
        shock = pew.gameObject.GetComponent<laser_ball_boss>() as laser_ball_boss;
    }
	
	// Update is called once per frame
	void Update () {
        if(moving == true)
        {
            speed = GetComponent<Rigidbody2D>().velocity.x;
        }
        ChangeDirection();
        currentSpeed = GetComponent<Rigidbody2D>().velocity.x;
        BossChanges();
        if (currentCoolDown == 0 && moving == false)
        {
            moving = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        }
        CoolDown();
        if(hp <= 0 && gameOver == false)
        {
            alive = false;
            this.GetComponent<Rigidbody2D>().isKinematic = true;
            // Destroy(this.gameObject, 2);
            GameObject g = new GameObject();
            g = GameObject.Find("BP1 (1)");
            Destroy(g.gameObject);
            Global.score = Global.score + 5000;
            currentCoolDown = 4;
            gameOver = true;
            GetComponent<Renderer>().material.color = Color.black;
        }
        if(gameOver == true)
        {
            if(currentCoolDown == 0)
            {
                Application.LoadLevel(18);
            }
        }
	   
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            hp--;
        }
    }
    void CoolDown()
    {
        if (currentCoolDown < 0)
        {
            currentCoolDown = 0;
        }

        if (currentCoolDown > 0)
        {
            currentCoolDown -= Time.deltaTime;
        }
    }

    void CheckDirection()
    {
        if(currentSpeed > 0.00001)
        {
            speed = speed * 1; ;
        }
        else
        {
            speed = speed * -1;
        }
    }

    void IncreaseSpeed()
    {
        if(currentSpeed > 0.0001)
        {
            speed = speed + 0.45f;
        }
        if(currentSpeed < -0.0001)
        {
            speed = speed - 0.45f;
        }
    }

    void ChangeDirection()
    {
        if(gameObject.transform.position.x < -4.6f && currentCoolDown == 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
            speed = GetComponent<Rigidbody2D>().velocity.x;
            currentCoolDown = 1;
            wtf = wtf + 2;
        }
        if (gameObject.transform.position.x > 4.5f && currentCoolDown == 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
            speed = GetComponent<Rigidbody2D>().velocity.x;
            currentCoolDown = 1;
            wtf = wtf + 1;
        }
    }
    
    void Rand()
    {
        int d = Random.Range(0, 1);
        if(d == 0)
        {
            direction = -1;
        } 
        if(d == 1)
        {
            direction = 1;
        }
    }

    void BossChanges()
    {
        if (hp == 0)
        {
            //do stuff
        }
        if (hp <= 42 && monsters == 11)
        {
            Rand();
            GameObject g = GameObject.Find("Monster1");
            monster_boss m = g.gameObject.GetComponent<monster_boss>() as monster_boss;
            m.alive = false;
            IncreaseSpeed();
            CheckDirection();
            GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed, 0);
            monsters--;
        }
        if (hp <= 39 && monsters == 10)
        {
            Rand();
            GameObject g = GameObject.Find("Monster2");
            monster_boss m = g.gameObject.GetComponent<monster_boss>() as monster_boss;
            m.alive = false;
            IncreaseSpeed();
            CheckDirection();
            GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed, 0);
            monsters--;
        }
        if (hp <= 36 && monsters == 9)
        {
            Rand();
            GameObject g = GameObject.Find("Monster3");
            monster_boss m = g.gameObject.GetComponent<monster_boss>() as monster_boss;
            m.alive = false;
            IncreaseSpeed();
            CheckDirection();
            GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed, 0);
            monsters--;
        }
        if (hp <= 33 && monsters == 8)
        {
            Rand();
            shock.coolDownNumber = 3;
            GameObject g = GameObject.Find("Monster4");
            monster_boss m = g.gameObject.GetComponent<monster_boss>() as monster_boss;
            m.alive = false;
            IncreaseSpeed();
            CheckDirection();
            GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed, 0);
            monsters--;
        }
        if (hp <= 30 && monsters == 7)
        {
            Rand();
            GameObject g = GameObject.Find("Monster5");
            monster_boss m = g.gameObject.GetComponent<monster_boss>() as monster_boss;
            m.alive = false;
            IncreaseSpeed();
            CheckDirection();
            GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed, 0);
            monsters--;
        }
        if (hp <= 27 && monsters == 6)
        {
            Rand();
            GameObject g = GameObject.Find("Monster6");
            monster_boss m = g.gameObject.GetComponent<monster_boss>() as monster_boss;
            m.alive = false;
            IncreaseSpeed();
            CheckDirection();
            GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed, 0);
            monsters--;
        }
        if (hp <= 24 && monsters == 5)
        {
            Rand();
            GameObject g = GameObject.Find("Monster7");
            monster_boss m = g.gameObject.GetComponent<monster_boss>() as monster_boss;
            m.alive = false;
            IncreaseSpeed();
            CheckDirection();
            GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed, 0);
            monsters--;
        }
        if (hp <= 21 && monsters == 4)
        {
            Rand();
            shock.coolDownNumber = 2;
            GameObject g = GameObject.Find("Monster8");
            monster_boss m = g.gameObject.GetComponent<monster_boss>() as monster_boss;
            m.alive = false;
            IncreaseSpeed();
            CheckDirection();
            GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed, 0);
            monsters--;
        }
        if (hp <= 18 && monsters == 3)
        {
            Rand();
            GameObject g = GameObject.Find("Monster9");
            monster_boss m = g.gameObject.GetComponent<monster_boss>() as monster_boss;
            m.alive = false;
            IncreaseSpeed();
            CheckDirection();
            GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed, 0);
            monsters--;
        }
        if (hp <= 15 && monsters == 2)
        {
            Rand();
            GameObject g = GameObject.Find("Monster10");
            monster_boss m = g.gameObject.GetComponent<monster_boss>() as monster_boss;
            m.alive = false;
            IncreaseSpeed();
            CheckDirection();
            GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed, 0);
            monsters--;
        }
        if (hp <= 12 && monsters == 1)
        {
            Rand();
            GameObject g = GameObject.Find("Monster11");
            monster_boss m = g.gameObject.GetComponent<monster_boss>() as monster_boss;
            m.alive = false;
            IncreaseSpeed();
            CheckDirection();
            GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed, 0);
            monsters--;
        }
        if (hp <= 9  && monsters == 0)
        {
            shock.coolDownNumber = 1;
            Rand();
            IncreaseSpeed();
            CheckDirection();
            GetComponent<Rigidbody2D>().velocity = new Vector2((direction * speed) * 1.7f, 0);
            monsters--;
        }
    }

}
