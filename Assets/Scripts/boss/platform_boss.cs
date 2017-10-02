using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class platform_boss : MonoBehaviour
{
    public AudioSource s = new AudioSource();
    Animator plat_anim;
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
    public bool L = true;
    public bool M = false;
    public bool X = false;
    public bool E = false;
    public bool R = false;

    public int A_hits = 0;
    public int I_hits = 0;
    public int N_hits = 0;

    public float PballSpeed;
    public float PballMaxSpeed;
    public float PballMinSpeed;

    public int blocks = 0;

    // Use this for initialization
    void Start()
    {
        LoadStart();
        blocks = 0;
        blocks = 0;
        plat_anim = GetComponent<Animator>();
        laser = GameObject.Find("Laser main");
        laser1 = GameObject.Find("laser1");
        laser2 = GameObject.Find("laser2");
        projectile = GameObject.Find("Projectile");
        isInvisible = false;
        neki = false;
        L = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.sound == false)
        {
            s.mute = true;
        }
        if (Global.sound == true)
        {
            s.mute = false;
        }
        PowerUpEffects();

        if (!gameIsBeingPlayed)
        {
            PowerUpsToFalse();
        }
        test();
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
            L = true;
        }
        if (other.gameObject.name == "R(Clone)")
        {
            this.transform.localScale = new Vector2(0.5f, 1f);
            N = false;
            L = true;
        }
        if (other.gameObject.name == "S(Clone)")
        {
            //na privzeto
            žoga_script.ballSpeed = PballSpeed;
            žoga_script.ballMaxSpeed = PballMaxSpeed;
            žoga_script.ballMinSpeed = PballMinSpeed;
            //upočasni
            žoga_script.ballSpeed = žoga_script.ballSpeed / 2;
            žoga_script.ballMaxSpeed = žoga_script.ballMaxSpeed / 2;
            žoga_script.ballMinSpeed = žoga_script.ballMinSpeed / 2;
        }
        if (other.gameObject.name == "F(Clone)")
        {
            //na privzeto
            žoga_script.ballSpeed = PballSpeed;
            žoga_script.ballMaxSpeed = PballMaxSpeed;
            žoga_script.ballMinSpeed = PballMinSpeed;
            //upočasni
            žoga_script.ballSpeed = žoga_script.ballSpeed / 2 + žoga_script.ballSpeed;
            žoga_script.ballMaxSpeed = žoga_script.ballMaxSpeed / 2 + žoga_script.ballMaxSpeed;
            žoga_script.ballMinSpeed = žoga_script.ballMinSpeed / 2 + žoga_script.ballMinSpeed;
        }
        if (other.gameObject.name == "I(Clone)")
        {
            I_hits = 20;
            this.transform.localScale = new Vector2(1f, 1f);
            N = false;
            I = true;
        }
        if (other.gameObject.name == "A(Clone)")
        {
            A_hits = 20;
            this.transform.localScale = new Vector2(1f, 1f);
            I = false;
            A = true;
            N = false;
        }
        if (other.gameObject.name == "N(Clone)")
        {
            N_hits = 20;
            this.transform.localScale = new Vector2(1f, 1f);
            L = true;
            I = false;
            N = true;
        }
        if (other.gameObject.name == "G(Clone)")
        {
            this.transform.localScale = new Vector2(1f, 1f);
            X = false;
            L = true;
            G = true;

        }
        if (other.gameObject.name == "M(Clone)")
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
            L = true;
            G = false;
            M = false;
        }
        if (other.gameObject.name == "L(Clone)")
        {
            this.transform.localScale = new Vector2(1f, 1f);
            X = false;
            G = false;
            L = true;
            N = false;
        }
        if (other.gameObject.name == "X(Clone)")
        {
            L = true;
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
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "white ball")
        {
            A_hits--;
            N_hits--;
            I_hits--;
        }
        if(other.gameObject.name == "BP1(Clone)")
        {
            Application.LoadLevel(18);
            Global.attempts = Global.attempts - 1;
        }
    }

    void PowerUpsToFalse()
    {
        I = false;
        A = false;
        N = false;
        G = false;
        L = true;
        M = false;
        X = false;
        E = false;
        R = false;
        this.transform.localScale = new Vector2(1f, 1f);
    }

    void LoadStart()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Global.lvl = 6;
            Global.attempts = 4;
            Global.score = 0;
            Application.LoadLevel(1);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(Time.timeScale == 0)
            Time.timeScale = 1;
            if (Time.timeScale == 1)
                Time.timeScale = 0;
        }
    }

    void test()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.transform.localScale = new Vector2(1.3f, 1f);
            N = false;
            L = true;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.transform.localScale = new Vector2(0.5f, 1f);
            N = false;
            L = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //na privzeto
            žoga_script.ballSpeed = PballSpeed;
            žoga_script.ballMaxSpeed = PballMaxSpeed;
            žoga_script.ballMinSpeed = PballMinSpeed;
            //upočasni
            žoga_script.ballSpeed = žoga_script.ballSpeed / 2;
            žoga_script.ballMaxSpeed = žoga_script.ballMaxSpeed / 2;
            žoga_script.ballMinSpeed = žoga_script.ballMinSpeed / 2;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            //na privzeto
            žoga_script.ballSpeed = PballSpeed;
            žoga_script.ballMaxSpeed = PballMaxSpeed;
            žoga_script.ballMinSpeed = PballMinSpeed;
            //upočasni
            žoga_script.ballSpeed = žoga_script.ballSpeed / 2 + žoga_script.ballSpeed;
            žoga_script.ballMaxSpeed = žoga_script.ballMaxSpeed / 2 + žoga_script.ballMaxSpeed;
            žoga_script.ballMinSpeed = žoga_script.ballMinSpeed / 2 + žoga_script.ballMinSpeed;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            I_hits = 20;
            this.transform.localScale = new Vector2(1f, 1f);
            N = false;
            I = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            A_hits = 20;
            this.transform.localScale = new Vector2(1f, 1f);
            I = false;
            A = true;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            N_hits = 20;
            this.transform.localScale = new Vector2(1f, 1f);
            L = true;
            I = false;
            N = true;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            this.transform.localScale = new Vector2(1f, 1f);
            X = false;
            L = true;
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
            L = true;
            G = false;
            M = false;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            L = true;
            G = false;
            X = true;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            blocks = 0;
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
                Instantiate(projectile, new Vector3(laser1.transform.position.x, laser2.transform.position.y + 0.1f, 0), Quaternion.identity);
                Instantiate(projectile, new Vector3(laser2.transform.position.x, laser2.transform.position.y + 0.1f, 0), Quaternion.identity);
                s.Play();
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

    IEnumerator SwitchLvl()
    {
        float fadeTime = GameObject.Find("Platform").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
    }
}