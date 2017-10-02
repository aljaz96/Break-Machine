using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;



public class žoga : MonoBehaviour {

    // Use this for initialization
    public AudioSource explo = new AudioSource();
    public Rigidbody2D Rb;
    Animator ba;
    public GameObject platf;
    public GameObject ball;
    public GameObject monst;
    public platform p;
    public monster m;
    public float speed = 5f;
    public float platformSpeed = 30f;
    public float ballForce = 10f;
    public bool movingBall = false;
    public float ballMinSpeed = 2f; /// 2,5
    public float ballSpeed = 3.2f; 
    public float ballMaxSpeed = 4.4f; // 3,9

    public float ballCoolDown = 2;
    public float currentBallCoolDown = 0;
    public float triggerCooldown = 0;

    Random rnd = new Random();
    Random rndP = new Random();

    public float blokXpos;
    public float blokYpos;
    public float chance;
    public int numberOfPowerUp;

    public float ballPositionX;
   
    public Text attemptText;
    public Text scoreText;
    public Text blockText;

    public bool afterShot = false;

    Vector3 lastPosition = Vector3.zero;
    public float xSpeed;
    public float ySpeed;
    public bool ballIsCopy = false;

    void Start()
    {
        ballCoolDown = 1.4f;
        ba = GetComponent<Animator>();
        currentBallCoolDown = ballCoolDown;
        Rb.isKinematic = true;
        platf = GameObject.Find("Platform");
        ball = GameObject.Find("white ball");
        monst = GameObject.Find("Monster");
        p = platf.gameObject.GetComponent<platform>() as platform;
        m = monst.gameObject.GetComponent<monster>() as monster;
        //platf.GetComponent<Rigidbody2D>().transform.position = new Vector3(0, -4.32f, 0);
        //this.GetComponent<Rigidbody2D>().transform.position = new Vector3(0, -4, 0);


        if ((currentBallCoolDown == 0 && movingBall == false) || ballIsCopy == true)
        {
            Rb.isKinematic = false;
            Rb.AddForce(new Vector2(ballForce, ballForce));
            movingBall = true;
        }
    }

    void FixedUpdate()
    {
        if (movingBall == false)
        {
            float move = Input.GetAxis("Horizontal");
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * speed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }
	
	void Update () {
        if (Global.sound == false)
        {
            explo.mute = true;
        }
        if (Global.sound == true)
        {
            explo.mute = false;
        }
        platformSpeed = PlayerPrefs.GetFloat("platformSpeed");
         xSpeed = Rb.velocity.x;
         ySpeed = Rb.velocity.y;

        PowerUpEffects();
        IzpisiText();
        coolDown();
        playBall();

        if(movingBall == false)
        {
            if (Input.GetKeyDown("space"))
                currentBallCoolDown = 0;     
        }

        keepMovementXY();
    }

    //Normal Ball
    void OnCollisionEnter2D(Collision2D other)
    {
       // powerups(other);
        if (other.gameObject.tag == "Bottom")
        {
            if (p.numberOfBalls <= 1)
            {
                p.X = false;
                p.gameIsBeingPlayed = false;
                Global.attempts--;
                p.numberOfBalls--;
                IzpisiText();
                currentBallCoolDown = ballCoolDown;
                Rb.isKinematic = true;
                movingBall = false;
                afterShot = false;
                ballIsCopy = false;
                foreach (GameObject x in GameObject.FindGameObjectsWithTag("PowerUp"))
                {
                    Destroy(x);
                }
                foreach (GameObject x in GameObject.FindGameObjectsWithTag("Monster"))
                {
                    Destroy(x);
                    m.monsters = m.monsters - 1;
                }
                foreach (GameObject x in GameObject.FindGameObjectsWithTag("Projectile"))
                {
                    Destroy(x);
                }
                platf.GetComponent<Rigidbody2D>().transform.position = new Vector3(0, -4.32f, 0);
                this.GetComponent<Rigidbody2D>().transform.position = new Vector3(0, -4, 0);
                m.currentCoolDown = 15;
                p.numberOfBalls = 1;

            }
            else
            {
                Destroy(this.gameObject);
                p.numberOfBalls--;
            }
        }

        if (other.gameObject.tag == "Block")
        {
            chance = Random.Range(0, 100);
            if(chance <= 25)
            {
                blokXpos = other.transform.position.x;
                blokYpos = other.transform.position.y;
                dropPowerUp();
            }
            afterShot = true;
            Destroy(other.gameObject);
            explo.Play();
            Global.score = Global.score + 50;
            p.blocks = p.blocks - 1;
            IzpisiText();
        }

        if (other.gameObject.tag == "Gray Block")
        {
            Block_2_hit block_hp = other.gameObject.GetComponent<Block_2_hit>() as Block_2_hit;
            afterShot = true;
            if (block_hp.hp < 1)
            {
                chance = Random.Range(0, 100);
                if (chance <= 25)
                {
                    blokXpos = other.transform.position.x;
                    blokYpos = other.transform.position.y;
                    dropPowerUp();
                }
                Destroy(other.gameObject);
                explo.Play();
                Global.score = Global.score + 50;
                p.blocks = p.blocks - 1;
                IzpisiText();
            }
        }
        if (other.gameObject.tag == "Monster")
        {
            Global.score = Global.score + 200;
            m.monsters = m.monsters - 1;
            explo.Play();
        }

        if(other.gameObject.name == "Platform" && p.G == true)
        {
            currentBallCoolDown = ballCoolDown;
            Rb.isKinematic = true;
            movingBall = false;
            afterShot = false;
        }
    }
    //Blue Ball
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Block")
        {
            chance = Random.Range(0, 100);
            if (chance <= 25)
            {
                blokXpos = other.transform.position.x;
                blokYpos = other.transform.position.y;
                dropPowerUp();
            }
            afterShot = true;
            Destroy(other.gameObject);
            explo.Play();
            Global.score = Global.score + 50;
            p.blocks = p.blocks - 1;
            IzpisiText();
        }
        if(other.gameObject.tag == "Border" || other.gameObject.name == "Platform")
        {
            if (triggerCooldown == 0)
            {
                this.GetComponent<Collider2D>().isTrigger = false;
                triggerCooldown = 0.01f;
            }
        }
        if (other.gameObject.tag == "Bottom")
        {
            if (p.numberOfBalls <= 1)
            {
                p.X = false;
                p.gameIsBeingPlayed = false;
                Global.attempts--;
                p.numberOfBalls--;
                IzpisiText();
                currentBallCoolDown = ballCoolDown;
                Rb.isKinematic = true;
                movingBall = false;
                afterShot = false;
                ballIsCopy = false;
                foreach (GameObject x in GameObject.FindGameObjectsWithTag("PowerUp"))
                {
                    Destroy(x);
                }
                foreach (GameObject x in GameObject.FindGameObjectsWithTag("Monster"))
                {
                    Destroy(x);
                    m.monsters = m.monsters - 1;
                }
                foreach (GameObject x in GameObject.FindGameObjectsWithTag("Projectile"))
                {
                    Destroy(x);
                }
                platf.GetComponent<Rigidbody2D>().transform.position = new Vector3(0, -4.32f, 0);
                this.GetComponent<Rigidbody2D>().transform.position = new Vector3(0, -4, 0);
                m.currentCoolDown = 15;
                p.numberOfBalls = 1;

            }
            else
            {
                Destroy(this.gameObject);
                p.numberOfBalls--;
            }
        }
        if (other.gameObject.tag == "Gray Block")
        {
            chance = Random.Range(0, 100);
            if (chance <= 25)
            {
                blokXpos = other.transform.position.x;
                blokYpos = other.transform.position.y;
                dropPowerUp();
            }
            afterShot = true;
            Destroy(other.gameObject);
            explo.Play();
            Global.score = Global.score + 50;
            p.blocks = p.blocks - 1;
            IzpisiText();
        }
        if (other.gameObject.tag == "Monster")
        {
            Global.score = Global.score + 200;
            explo.Play();
            m.monsters = m.monsters - 1;
        }
    }

    public void IzpisiText()
    {
        attemptText.text = "Attempt: " + Global.attempts.ToString();
        scoreText.text = "Score: " + Global.score.ToString();
        blockText.text = "Blocks:" + p.blocks.ToString();
    }

    void coolDown()
    {
        if (currentBallCoolDown < 0)
        {
            currentBallCoolDown = 0;
        }

        if (currentBallCoolDown > 0)
        {
            currentBallCoolDown -= Time.deltaTime;
        }

        if (triggerCooldown < 0)
        {
            triggerCooldown = 0;
        }
        if(triggerCooldown > 0)
        {
            triggerCooldown -= Time.deltaTime;
        }
    }

    void playBall()
    {
        if (currentBallCoolDown == 0 && movingBall == false)
        {
            Rb.isKinematic = false;
            Rb.AddForce(new Vector2(ballForce, ballForce));
            movingBall = true;
            p.gameIsBeingPlayed = true;
        }
    }

    void keepMovementXY()
    {
        if (Rb.GetComponent<Rigidbody2D>().velocity.x < ballMinSpeed && xSpeed > 0.001f || Rb.GetComponent<Rigidbody2D>().velocity.x > ballMaxSpeed && xSpeed > 0.001f)
        {
            Rb.GetComponent<Rigidbody2D>().velocity = new Vector2(ballSpeed, Rb.GetComponent<Rigidbody2D>().velocity.y);
        }
        if (Rb.GetComponent<Rigidbody2D>().velocity.x > -ballMinSpeed && xSpeed < -0.001f || Rb.GetComponent<Rigidbody2D>().velocity.x < -ballMaxSpeed && xSpeed < -0.001f)
        {
            Rb.GetComponent<Rigidbody2D>().velocity = new Vector2(-ballSpeed, Rb.GetComponent<Rigidbody2D>().velocity.y);
        }
        if (Rb.GetComponent<Rigidbody2D>().velocity.y < ballMinSpeed && ySpeed > 0.001f || Rb.GetComponent<Rigidbody2D>().velocity.y > ballMaxSpeed && ySpeed > 0.001f)
        {
            Rb.GetComponent<Rigidbody2D>().velocity = new Vector2(Rb.GetComponent<Rigidbody2D>().velocity.x, ballSpeed);
        }
        if (Rb.GetComponent<Rigidbody2D>().velocity.y > -ballMinSpeed && ySpeed < -0.001f || Rb.GetComponent<Rigidbody2D>().velocity.y < -ballMaxSpeed && ySpeed < 0.001f )
        {
            Rb.GetComponent<Rigidbody2D>().velocity = new Vector2(Rb.GetComponent<Rigidbody2D>().velocity.x, -ballSpeed);
        }
    }

    void PowerUpEffects()
    {   
        if (p.A)
        {
            ballPositionX = Rb.transform.position.x;
            platf.GetComponent<Rigidbody2D>().transform.position = new Vector3(ballPositionX, -4.32f, 0);
        }
        if (p.X == true)
        {
            ba.SetTrigger("blue");
            if (triggerCooldown == 0)
            {
                this.GetComponent<Collider2D>().isTrigger = true;
            }
        }
        if (p.X == false)
        {
            this.GetComponent<Collider2D>().isTrigger = false;
            ba.SetTrigger("white");
        }
    }

    public void dropPowerUp()
    {
        numberOfPowerUp = Random.Range(0, 26);
        if(numberOfPowerUp >= 0 && numberOfPowerUp < 2)
        {
            Instantiate(GameObject.Find("A"), new Vector3(Random.Range(blokXpos - 0.2f, blokXpos + 0.2f), Random.Range(blokYpos -0.2f, blokYpos + 0.2f), 0), Quaternion.identity);
            foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "A(Clone)"))
            {
                x.tag = "PowerUp";
            }
        }
        if (numberOfPowerUp >= 2 && numberOfPowerUp < 4)
        {
            Instantiate(GameObject.Find("E"), new Vector3(Random.Range(blokXpos - 0.2f, blokXpos + 0.2f), Random.Range(blokYpos - 0.2f, blokYpos + 0.2f), 0), Quaternion.identity);
            foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "E(Clone)"))
            {
                x.tag = "PowerUp";
            }
        }
        if (numberOfPowerUp >= 4 && numberOfPowerUp < 6)
        {
            Instantiate(GameObject.Find("SKULL"), new Vector3(Random.Range(blokXpos - 0.2f, blokXpos + 0.2f), Random.Range(blokYpos - 0.2f, blokYpos + 0.2f), 0), Quaternion.identity);
            foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "SKULL(Clone)"))
            {
                x.tag = "PowerUp";
            }
        }
        if (numberOfPowerUp >= 6 && numberOfPowerUp < 8)
        {
            Instantiate(GameObject.Find("F"), new Vector3(Random.Range(blokXpos - 0.2f, blokXpos + 0.2f), Random.Range(blokYpos - 0.2f, blokYpos + 0.2f), 0), Quaternion.identity);
            foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "F(Clone)"))
            {
                x.tag = "PowerUp";
            }
        }
        if (numberOfPowerUp >= 8 && numberOfPowerUp < 10)
        {
            Instantiate(GameObject.Find("N"), new Vector3(Random.Range(blokXpos - 0.2f, blokXpos + 0.2f), Random.Range(blokYpos - 0.2f, blokYpos + 0.2f), 0), Quaternion.identity);
            foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "N(Clone)"))
            {
                x.tag = "PowerUp";
            }
        }
        if (numberOfPowerUp >= 10 && numberOfPowerUp < 12)
        {
            Instantiate(GameObject.Find("I"), new Vector3(Random.Range(blokXpos - 0.2f, blokXpos + 0.2f), Random.Range(blokYpos - 0.2f, blokYpos + 0.2f), 0), Quaternion.identity);
            foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "I(Clone)"))
            {
                x.tag = "PowerUp";
            }
        }
        if (numberOfPowerUp >= 12 && numberOfPowerUp < 14)
        {
            Instantiate(GameObject.Find("L"), new Vector3(Random.Range(blokXpos - 0.2f, blokXpos + 0.2f), Random.Range(blokYpos - 0.2f, blokYpos + 0.2f), 0), Quaternion.identity);
            foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "L(Clone)"))
            {
                x.tag = "PowerUp";
            }
        }
        if (numberOfPowerUp >= 14 && numberOfPowerUp < 16)
        {
            Instantiate(GameObject.Find("M"), new Vector3(Random.Range(blokXpos - 0.2f, blokXpos + 0.2f), Random.Range(blokYpos - 0.2f, blokYpos + 0.2f), 0), Quaternion.identity);
            foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "M(Clone)"))
            {
                x.tag = "PowerUp";
            }
        }
        if (numberOfPowerUp >= 16 && numberOfPowerUp < 18)
        {
            Instantiate(GameObject.Find("X"), new Vector3(Random.Range(blokXpos - 0.2f, blokXpos + 0.2f), Random.Range(blokYpos - 0.2f, blokYpos + 0.2f), 0), Quaternion.identity);
            foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "X(Clone)"))
            {
                x.tag = "PowerUp";
            }
        }
        if (numberOfPowerUp >= 18 && numberOfPowerUp < 20)
        {
            Instantiate(GameObject.Find("S"), new Vector3(Random.Range(blokXpos - 0.2f, blokXpos + 0.2f), Random.Range(blokYpos - 0.2f, blokYpos + 0.2f), 0), Quaternion.identity);
            foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "S(Clone)"))
            {
                x.tag = "PowerUp";
            }
        }
        if (numberOfPowerUp >= 20 && numberOfPowerUp < 22)
        {
            Instantiate(GameObject.Find("R"), new Vector3(Random.Range(blokXpos - 0.2f, blokXpos + 0.2f), Random.Range(blokYpos - 0.2f, blokYpos + 0.2f), 0), Quaternion.identity);
            foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "R(Clone)"))
            {
                x.tag = "PowerUp";
            }
        }
        if (numberOfPowerUp >= 22 && numberOfPowerUp < 24)
        {
            Instantiate(GameObject.Find("G"), new Vector3(Random.Range(blokXpos - 0.2f, blokXpos + 0.2f), Random.Range(blokYpos - 0.2f, blokYpos + 0.2f), 0), Quaternion.identity);
            foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "G(Clone)"))
            {
                x.tag = "PowerUp";
            }
        }
        if (numberOfPowerUp >= 24 && numberOfPowerUp < 25)
        {
            Instantiate(GameObject.Find("Z"), new Vector3(Random.Range(blokXpos - 0.2f, blokXpos + 0.2f), Random.Range(blokYpos - 0.2f, blokYpos + 0.2f), 0), Quaternion.identity);
            foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Z(Clone)"))
            {
                x.tag = "PowerUp";
            }
        }
        if (numberOfPowerUp >= 25 && numberOfPowerUp < 26)
        {
            Instantiate(GameObject.Find("P"), new Vector3(Random.Range(blokXpos - 0.2f, blokXpos + 0.2f), Random.Range(blokYpos - 0.2f, blokYpos + 0.2f), 0), Quaternion.identity);
            foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "P(Clone)"))
            {
                x.tag = "PowerUp";
            }
        }
    }
}

