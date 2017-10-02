using UnityEngine;
using System.Collections;

public class invincible_block : MonoBehaviour {

    Animator block;
    // Use this for initialization
    void Start () {
        block = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

      

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            block.SetTrigger("trigger true");
            block.SetTrigger("trigger false");
      
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            block.SetTrigger("trigger true");
            block.SetTrigger("trigger false");
        }
    }
}
