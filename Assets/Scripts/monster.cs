using UnityEngine;
using System.Collections;

public class monster : MonoBehaviour {

    // Use this for initialization
    Animator monstro;
    public Rigidbody2D mo;
    public GameObject enemy;
    public Collider2D col;
    public GameObject clone;
    public float coolDown = 15;
    public float currentCoolDown = 0;
    public float speed = 3f;
    public int monsters = 0;
    public float currentSpeedX;
    public float currentSpeedY;

    public float xSpeed;
    public float ySpeed;

    void Start () {
        monsters = 0;
        currentCoolDown = coolDown;
        monstro = GetComponent<Animator>();
        mo.AddForce(new Vector2(speed, speed));
        enemy = GameObject.Find("Monster");
    }
	
	// Update is called once per frame
	void Update () {
        CoolDown();
        currentSpeedX = mo.GetComponent<Rigidbody2D>().velocity.x;
        currentSpeedY = mo.GetComponent<Rigidbody2D>().velocity.y;
        //keepMovementXY();
        xSpeed = mo.velocity.x;
        ySpeed = mo.velocity.y;
        if (currentCoolDown == 0 && monsters < 3 && this.gameObject.name == ("Monster"))
        {
            currentCoolDown = coolDown;
            GameObject m = (GameObject)Instantiate(enemy, new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(4.3f, 3.3f), 0), Quaternion.identity);
            m.tag = "Monster";
            monsters++;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            killMonster();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            killMonster();
        }
        if (other.gameObject.tag == "Projectile")
        {
            killMonster();
        }
    }


    void keepMovementXY()
    {
        if (mo.GetComponent<Rigidbody2D>().velocity.x < 0.5 && xSpeed > 0.001f || mo.GetComponent<Rigidbody2D>().velocity.x > 1.5 && xSpeed > 0.001f)
        {
            mo.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, mo.GetComponent<Rigidbody2D>().velocity.y);
        }
        if (mo.GetComponent<Rigidbody2D>().velocity.x > -0.5 && xSpeed < -0.001f || mo.GetComponent<Rigidbody2D>().velocity.x < -1.5 && xSpeed < -0.001f)
        {
            mo.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f/2, mo.GetComponent<Rigidbody2D>().velocity.y);
        }
        if (mo.GetComponent<Rigidbody2D>().velocity.y < 0.5 && ySpeed > 0.001f  || mo.GetComponent<Rigidbody2D>().velocity.y > 1.5 && ySpeed > 0.001f)
        {
            mo.GetComponent<Rigidbody2D>().velocity = new Vector2(mo.GetComponent<Rigidbody2D>().velocity.x,1f);
        }
        if (mo.GetComponent<Rigidbody2D>().velocity.y > -0.5 && ySpeed < -0.001f || mo.GetComponent<Rigidbody2D>().velocity.y < -1.5 && ySpeed < 0.001f)
        {
            mo.GetComponent<Rigidbody2D>().velocity = new Vector2(mo.GetComponent<Rigidbody2D>().velocity.x, -1f);
        }
        if(mo.transform.localScale.y == 0)
        {
            mo.GetComponent<Rigidbody2D>().velocity = new Vector2(mo.GetComponent<Rigidbody2D>().velocity.x, -1f);
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

    void killMonster()
    {
        monstro.SetTrigger("dead");
        mo.isKinematic = true;
        this.GetComponent<Collider2D>().isTrigger = true;
        Destroy(this.gameObject, 2.3f);
        monsters = monsters - 1;
    }
}
