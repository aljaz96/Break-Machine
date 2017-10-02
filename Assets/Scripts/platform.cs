using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class platform : MonoBehaviour {
    Animator plat_anim;
    public AudioSource s = new AudioSource();
    public GameObject darkness;
    public GameObject plat;
    public GameObject laser;
    public GameObject laser1;
    public GameObject laser2;
    public GameObject projectile;
    public Rigidbody2D rb;
    public float pSpeed = 30f;
    public GameObject ball;
    žoga žoga_script;
    public bool isInvisible;
    public bool neki;
    public int numberOfBalls = 1;
    public bool gameIsBeingPlayed = false;

    public bool I = false;
    public bool A = false;
    public bool N = false;
    public bool G = false;
    public bool L = false;
    public bool M = false;
    public bool X = false;
    public bool E = false;
    public bool R = false;
    public bool SKULL = false;

    public int A_hits = 0;
    public int I_hits = 0;
    public int N_hits = 0;
    public int SKULL_hits = 0;

    public float PballSpeed;
    public float PballMaxSpeed;
    public float PballMinSpeed;

    public int blocks = 0;

    // Use this for initialization
    void Start () {
        blocks = 0;
        blocks = 0;
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Block"))
        {
            blocks = blocks + 1;
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Gray Block"))
        {
            blocks = blocks + 1;
        }
        plat_anim = GetComponent<Animator>();
        darkness = GameObject.Find("Darkness");
        ball = GameObject.Find("white ball");
        laser = GameObject.Find("Laser main");
        laser1 = GameObject.Find("laser1");
        laser2 = GameObject.Find("laser2");
        projectile = GameObject.Find("Projectile");
        žoga_script = ball.gameObject.GetComponent<žoga>() as žoga;
        isInvisible = false;
        neki = false;
        PballSpeed = žoga_script.ballSpeed;
        A_hits = 0;
        I_hits = 0;
        N_hits = 0;
        SKULL_hits = 0;
        PballMaxSpeed = žoga_script.ballMaxSpeed;
        PballMinSpeed = žoga_script.ballMinSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        LoadStart();
        if (Global.sound == false)
        {
            s.mute = true;
        }
        if (Global.sound == true)
        {
            s.mute = false;
        }
        if (Global.attempts <= -1)
        {
            Application.LoadLevel(18);
        }
        PowerUpEffects();

        if (!gameIsBeingPlayed)
        {
            PowerUpsToFalse();
        }
        test();
        if(blocks <= 0)
        {
            Global.lvl = Global.lvl + 1;
            SwitchLvl();
            SceneManager.LoadScene(Global.lvl);
        }
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * pSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "E(Clone)")
        {
            this.transform.localScale = new Vector2(1.3f, 1f);
            N = false;
            L = false;
        }
        if (other.gameObject.name == "R(Clone)")
        {
            this.transform.localScale = new Vector2(0.5f, 1f);
            N = false;
            L = false;
        }
        if (other.gameObject.name == "S(Clone)")
        {
            foreach (GameObject x in GameObject.FindGameObjectsWithTag("Ball"))
            {
                žoga b = x.gameObject.GetComponent<žoga>() as žoga;
                b.ballSpeed = PballSpeed;
                b.ballMaxSpeed = PballMaxSpeed;
                b.ballMinSpeed = PballMinSpeed;
                //upočasni
                b.ballSpeed = b.ballSpeed / 2;
                b.ballMaxSpeed = b.ballMaxSpeed / 2;
                b.ballMinSpeed = b.ballMinSpeed / 2;
            }
        }
        if (other.gameObject.name == "F(Clone)")
        {
            //na privzeto
            foreach (GameObject x in GameObject.FindGameObjectsWithTag("Ball"))
            {
                žoga b = x.gameObject.GetComponent<žoga>() as žoga;
                b.ballSpeed = PballSpeed;
                b.ballMaxSpeed = PballMaxSpeed;
                b.ballMinSpeed = PballMinSpeed;
                //fast
                b.ballSpeed = b.ballSpeed / 2 + b.ballSpeed;
                b.ballMaxSpeed = b.ballMaxSpeed / 2 + b.ballMaxSpeed;
                b.ballMinSpeed = b.ballMinSpeed / 2 + b.ballMinSpeed;
            }
        }
        if (other.gameObject.name == "I(Clone)")
        {
            I_hits = 20;
            A_hits = 0;
            N_hits = 0;
            SKULL_hits = 0;
            DefaultBallSpeed();
            this.transform.localScale = new Vector2(1f, 1f);
            N = false;
            I = true;
            SKULL = false;
        }
        if (other.gameObject.name == "A(Clone)")
        {
            A_hits = 20;
            I_hits = 0;
            SKULL_hits = 0;
            N_hits = 0;
            this.transform.localScale = new Vector2(1f, 1f);
            I = false;
            A = true;
            N = false;
        }
        if (other.gameObject.name == "N(Clone)")
        {
            N_hits = 20;
            A_hits = 0;
            SKULL_hits = 0;
            I_hits = 0;
            this.transform.localScale = new Vector2(1f, 1f);
            L = false;
            I = false;
            N = true;
        }
        if (other.gameObject.name == "G(Clone)")
        {
            this.transform.localScale = new Vector2(1f, 1f);
            DefaultBallSpeed();
            X = false;
            L = false;
            G = true;
           
        }
        if(other.gameObject.name == "M(Clone)")
        {
            GameObject test = new GameObject();
            test = GameObject.Find("white ball");
            float žogaX = test.transform.position.x;
            float žogaY = test.gameObject.transform.position.y;
            numberOfBalls = numberOfBalls + 2;
            GameObject whiteball1 = (GameObject)Instantiate(test, new Vector3(žogaX - 0.2f, žogaY - 0.1f), Quaternion.identity);
            GameObject whiteball2 = (GameObject)Instantiate(test, new Vector3(žogaX + 0.2f, žogaY - 0.1f), Quaternion.identity);
            whiteball1.name = "white ball";
            žoga žoga_script1 = whiteball1.gameObject.GetComponent<žoga>() as žoga;
            žoga_script1.ballIsCopy = true;
            whiteball2.name = "white ball";
            žoga žoga_script2 = whiteball2.gameObject.GetComponent<žoga>() as žoga;
            žoga_script2.ballIsCopy = true;
            X = false;
            L = false;
            G = false;
            M = false;
        }
        if(other.gameObject.name == "L(Clone)")
        {
            this.transform.localScale = new Vector2(1f, 1f);
            X = false;
            G = false;
            L = true;
            N = false;
        }
        if (other.gameObject.name == "X(Clone)")
        {
            L = false;
            G = false;
            X = true;
        }
        if (other.gameObject.name == "Z(Clone)")
        {
            blocks = 0;
        }
        if (other.gameObject.name == "P(Clone)")
        {
            Global.attempts = Global.attempts + 1;
        }
        if (other.gameObject.name == "SKULL(Clone)")
        {
            I = false;
            A = false;
            SKULL = true;
            SKULL_hits = 10;
            A_hits = 0;
            N_hits = 0;
            I_hits = 0;
            darkness.transform.localScale = new Vector2(1000f, 1000f);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "white ball")
        {
            A_hits--;
            N_hits--;
            I_hits--;
            SKULL_hits--;
        }
    }

    void PowerUpsToFalse()
    {
        I = false;
        A = false;
        N = false;
        G = false;
        L = false;
        M = false;
        X = false;
        E = false;
        R = false;
        DefaultBallSpeed();
        this.transform.localScale = new Vector2(1f, 1f);
        darkness.transform.localScale = new Vector2(0f, 0f);
    }

    void DefaultBallSpeed()
    {
        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Ball"))
        {
            žoga b = x.gameObject.GetComponent<žoga>() as žoga;
            b.ballSpeed = PballSpeed;
            b.ballMaxSpeed = PballMaxSpeed;
            b.ballMinSpeed = PballMinSpeed;
        }
    }

    void test()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.transform.localScale = new Vector2(1.3f, 1f);
            N = false;
            L = false;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.transform.localScale = new Vector2(0.5f, 1f);
            N = false;
            L = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            foreach (GameObject x in GameObject.FindGameObjectsWithTag("Ball"))
            {
                žoga b = x.gameObject.GetComponent<žoga>() as žoga;
                b.ballSpeed = PballSpeed;
                b.ballMaxSpeed = PballMaxSpeed;
                b.ballMinSpeed = PballMinSpeed;
                //upočasni
                b.ballSpeed = b.ballSpeed / 2;
                b.ballMaxSpeed = b.ballMaxSpeed / 2;
                b.ballMinSpeed = b.ballMinSpeed / 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (GameObject x in GameObject.FindGameObjectsWithTag("Ball"))
            {
                žoga b = x.gameObject.GetComponent<žoga>() as žoga;
                b.ballSpeed = PballSpeed;
                b.ballMaxSpeed = PballMaxSpeed;
                b.ballMinSpeed = PballMinSpeed;
                //upočasni
                b.ballSpeed = b.ballSpeed / 2 + b.ballSpeed;
                b.ballMaxSpeed = b.ballMaxSpeed / 2 + b.ballMaxSpeed;
                b.ballMinSpeed = b.ballMinSpeed / 2 + b.ballMinSpeed;
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            I_hits = 20;
            this.transform.localScale = new Vector2(1f, 1f);
            DefaultBallSpeed();
            N_hits = 0;
            A_hits = 0;
            SKULL_hits = 0;
            N = false;
            I = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            A_hits = 20;
            this.transform.localScale = new Vector2(1f, 1f);
            I = false;
            I_hits = 0;
            N_hits = 0;
            SKULL_hits = 0;
            SKULL = false;
            A = true;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            N_hits = 20;
            this.transform.localScale = new Vector2(1f, 1f);
            L = false;
            I_hits = 0;
            SKULL_hits = 0;
            A_hits = 0;
            I = false;
            N = true;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            DefaultBallSpeed();
            this.transform.localScale = new Vector2(1f, 1f);
            X = false;
            L = false;
            G = true;
            A = false;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            X = false;
            G = false;
            L = true;
            A = false;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameObject test = new GameObject();
            test = GameObject.Find("white ball");
            float žogaX = test.transform.position.x;
            float žogaY = test.gameObject.transform.position.y;
            numberOfBalls = numberOfBalls + 2;
            GameObject whiteball1 = (GameObject)Instantiate(test, new Vector3(žogaX - 0.2f, žogaY - 0.1f), Quaternion.identity);
            GameObject whiteball2 = (GameObject)Instantiate(test, new Vector3(žogaX + 0.2f, žogaY - 0.1f), Quaternion.identity);
            whiteball1.name = "white ball";
            žoga žoga_script1 = whiteball1.gameObject.GetComponent<žoga>() as žoga;
            žoga_script1.ballIsCopy = true;
            whiteball2.name = "white ball";
            žoga žoga_script2 = whiteball2.gameObject.GetComponent<žoga>() as žoga;
            žoga_script2.ballIsCopy = true;
            X = false;
            L = false;
            G = false;
            M = false;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            L = false;
            G = false;
            X = true;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            blocks = 0;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Global.attempts = Global.attempts + 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SKULL_hits = 20;
            A_hits = 0;
            N_hits = 0;
            I_hits = 0;
            A = false;
            I = false;
            darkness.transform.localScale = new Vector2(1000f, 1000f);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Global.score = Global.score + 1000;
        }
    }

    void PowerUpEffects()
    {
        if (A_hits == 0)
        {
            A = false;
        }
        if (I_hits == 0)
        {
            I = false;
        }
        if (N_hits == 0)
        {
            N = false;
        }
        if(SKULL_hits == 0)
        {
            darkness.transform.localScale = new Vector2(0f, 0f);
            SKULL = false;
        }

        if (I == true)
        {
            pSpeed = -5f;
        }
        if (I == false)
        {
            pSpeed = 5f;
        }
        if (N == true && isInvisible == false)
        {
            plat_anim.SetTrigger("platform trigger true");
            isInvisible = true;
        }
        if (N == false && isInvisible == true)
        {
            plat_anim.SetTrigger("platform trigger false");
            isInvisible = false;
        }
        if (L == true)
        {
            laser.transform.localScale = new Vector2(1f, 1f);
            if (Input.GetKeyDown(KeyCode.Space) && GameObject.Find("Projectile(Clone)") == null)
            {
                s.Play();
                Instantiate(projectile, new Vector3(laser1.transform.position.x, laser2.transform.position.y + 0.1f, 0), Quaternion.identity);
                Instantiate(projectile, new Vector3(laser2.transform.position.x, laser2.transform.position.y + 0.1f, 0), Quaternion.identity);
            }
        }
        if (L == false)
        {
            laser.transform.localScale = new Vector2(0, 0);
        }
        if (X == true)
        {
            // DO STUFF
        }
        if (X == false)
        {
            // CANCEL STUFF
        }
        if (A == true)
        {
            // DO STUFF
        }
        if (A == false)
        {
            // CANCEL STUFF
        }
    }

    void LoadStart()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            Global.lvl = 6;
            Global.attempts = 4;
            Global.score = 0;
            Application.LoadLevel(1);
        }
    }

    IEnumerator SwitchLvl()
    {
        float fadeTime = GameObject.Find("Platform").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
    }
}
