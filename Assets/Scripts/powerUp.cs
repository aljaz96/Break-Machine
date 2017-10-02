using UnityEngine;
using System.Collections;

public class powerUp : MonoBehaviour {

    // Use this for initialization
    public float fallSpeed = -0.9f;
    public float currentFallSpeed;
    GameObject platform;
	void Start () {
        platform = GameObject.Find("Platform");
        fallSpeed = -0.9f;
    }
	
	// Update is called once per frame
	void Update () {
        currentFallSpeed = GetComponent<Rigidbody2D>().velocity.y;

        if (currentFallSpeed < -1)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, fallSpeed);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Platform")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.name == "BOTTOM")
        {
            Destroy(gameObject);
        }
    }

}
