using UnityEngine;
using System.Collections;

public class žogamenu : MonoBehaviour {

    // Use this for initialization
    // Use this for initialization
    public Rigidbody2D Rb;
    public float ballForce = 10f;

    public float xSpeed;
    public float ySpeed;



    void Start()
    {

        Rb.AddForce(new Vector2(ballForce, ballForce));
    }

    void Update()
    {
        xSpeed = Rb.velocity.x;
        ySpeed = Rb.velocity.y;
        keepMovementXY();
    }



    void keepMovementXY()
    {
        if (Rb.GetComponent<Rigidbody2D>().velocity.x < 2.5 && xSpeed > 0.001f || Rb.GetComponent<Rigidbody2D>().velocity.x > 3.9 && xSpeed > 0.001f)
        {
            Rb.GetComponent<Rigidbody2D>().velocity = new Vector2(3.2f, Rb.GetComponent<Rigidbody2D>().velocity.y);
        }
        if (Rb.GetComponent<Rigidbody2D>().velocity.x > -2.5 && xSpeed < -0.001f || Rb.GetComponent<Rigidbody2D>().velocity.x < -3.9 && xSpeed < -0.001f)
        {
            Rb.GetComponent<Rigidbody2D>().velocity = new Vector2(-3.2f, Rb.GetComponent<Rigidbody2D>().velocity.y);
        }
        if (Rb.GetComponent<Rigidbody2D>().velocity.y < 2.5 && ySpeed > 0.001f || Rb.GetComponent<Rigidbody2D>().velocity.y > 3.9 && ySpeed > 0.001f)
        {
            Rb.GetComponent<Rigidbody2D>().velocity = new Vector2(Rb.GetComponent<Rigidbody2D>().velocity.x, 3.2f);
        }
        if (Rb.GetComponent<Rigidbody2D>().velocity.y > -2.5 && ySpeed < -0.001f || Rb.GetComponent<Rigidbody2D>().velocity.y < -3.9 && ySpeed < 0.001f)
        {
            Rb.GetComponent<Rigidbody2D>().velocity = new Vector2(Rb.GetComponent<Rigidbody2D>().velocity.x, -3.2f);
        }
    }
}
