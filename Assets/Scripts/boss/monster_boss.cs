using UnityEngine;
using System.Collections;

public class monster_boss : MonoBehaviour {

    Animator monstro;
    public Rigidbody2D mo;
    public GameObject m;
    public Collider2D col;
    public bool alive;

    void Start()
    {
        alive = true;
        monstro = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(alive == false)
        {
            monstro.SetTrigger("dead");
            Destroy(this.gameObject, 2.3f);
        }
    }
}
