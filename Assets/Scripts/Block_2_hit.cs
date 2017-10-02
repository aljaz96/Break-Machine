using UnityEngine;
using System.Collections;

public class Block_2_hit : MonoBehaviour
{

    public int hp = 3;
    // Use this for initialization
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
            if (other.gameObject.tag == "Ball")
            {
                hp = hp - 1;
            }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            hp = hp - 1;
        }
    }
}
