using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float attackDmg;

    public float attackSpeed;

    public GameObject target = null;
    public Vector3 targetPosition;

    public List<Enemy> enterEnemys = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {

        targetPosition = (target.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.Translate(targetPosition * Time.deltaTime * attackSpeed);
        }
        else
        {
            Destroy(gameObject);
        }

        // �Ѿ��� �ִ� �Ÿ� ������ ������ ����
        float distance = Vector3.Distance(transform.position, transform.parent.position);
        if (distance > transform.parent.GetComponent<Tower>().hitSize)
        {
            Destroy(gameObject);
        }

    }

    // ���� ������ ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (!enemy.isDead)
            {
                enterEnemys.Add(enemy);

                enterEnemys[0].AddAffection(attackDmg);

                Destroy(gameObject);
            }
        }
    }
}
