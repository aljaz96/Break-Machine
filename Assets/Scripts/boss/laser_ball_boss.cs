using UnityEngine;
using System.Collections;

public class laser_ball_boss : MonoBehaviour {

    // Use this for initialization
    GameObject bos;
    float speed = 30f;
    float speedY;
    public float BossX;
    public float BossY;
    public float currentCoolDown;
    public float randomCoolDown;
    public float coolDownNumber;
    public Rigidbody2D r;
    public GameObject l;
    public float pSpeedX;
    public float pSpeedY;
    public boss b;

    void Start () {
        coolDownNumber = 4;
        pSpeedX = Random.Range(-100, 100);
        pSpeedY = Random.Range(-100, -300);
        bos = GameObject.Find("BOSS");
        l = GameObject.Find("BP1");
        randomCoolDown = Random.Range(0, coolDownNumber);
        currentCoolDown = randomCoolDown;
        r.AddForce(new Vector2(pSpeedX, pSpeedY));
        b = bos.gameObject.GetComponent<boss>() as boss;
    }
	
	// Update is called once per frame
	void Update () {
        if (b.alive == true)
        {
            CoolDown();
            BossX = bos.transform.position.x;
            BossY = bos.transform.position.y;
            SpawnProjectile();
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

    void SpawnProjectile()
    {
        if (currentCoolDown == 0 && this.name == "BP1")
        {
            randomCoolDown = Random.Range(0, coolDownNumber);
            currentCoolDown = randomCoolDown;
            GameObject m = (GameObject)Instantiate(l, new Vector3(BossX, BossY - 1, 0), Quaternion.identity);
        }      
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "BOTTOM")
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.name == "Platform")
        {
            Destroy(this.gameObject);
        }
    }
}
