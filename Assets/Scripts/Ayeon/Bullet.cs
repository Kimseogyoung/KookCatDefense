using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector3 targetPosition = Vector3.zero;
    public float attackDmg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(targetPosition * Time.deltaTime * 10.0f);

        // ���� �θ��� ���̰� �����Ÿ� �����ϸ� ����
        float distance = Vector3.Distance(transform.position, transform.parent.position);
        if (distance > transform.parent.GetComponent<CircleCollider2D>().radius)
        {
            Destroy(gameObject);
        }

        // ȭ�� ��
        //if (transform.position.x < -0.64f || transform.position.x > 13.44f || transform.position.y < -0.64f || transform.position.y > 8.32f)
        //{
            //Destroy(gameObject);
        //}
    }

    // ���� ������ ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            Destroy(gameObject);
    }
}
