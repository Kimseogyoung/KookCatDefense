using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Razer : MonoBehaviour
{

    public float attackDmg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 0.3f);
        //transform.localScale = new Vector3(0.3f, transform.GetComponent<CircleCollider2D>().radius, 1);
    }

    private void OnTriggerStay2d(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("Enemy!");
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy.isDead == false)
                enemy.AddAffection(attackDmg);
        }
    }

}
