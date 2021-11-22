using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingBullet : MonoBehaviour
{
    public GameObject target;
    public float attackDmg;

    public float attackSpeed;

    Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManager.Instance.PlayGameSFX(GameSFX.Bullet);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            //targetPosition = (target.transform.position - transform.parent.position).normalized;
            Vector3 pos = (target.transform.position - transform.position).normalized;
            // ����(dot)�� ���� ������ ����. (Acos�� ���� ������ ������ �� ���� ����)
            float dot = Vector3.Dot(transform.up, pos);
            if (dot < 1.0f)
            {
                float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

                // ������ ���� ������ ������ �Ǻ�.
                Vector3 cross = Vector3.Cross(transform.up, pos);
                // ���� ��� ���� ���� ���� �ݿ�
                if (cross.z < 0)
                {
                    angle = transform.rotation.eulerAngles.z - Mathf.Min(10, angle);
                }
                else
                {
                    angle = transform.rotation.eulerAngles.z + Mathf.Min(10, angle);
                }

                // angle�� �� ����� target�� ����.
                // do someting.
                transform.Translate(pos * Time.deltaTime * attackSpeed);
            }

        }
        else
        {
            Destroy(gameObject);
        }
        
        
    }

    // Ÿ���� ������ ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy == target.GetComponent<Enemy>() && !enemy.isDead)
            {
                enemy.AddAffection(attackDmg);
                Destroy(gameObject);

            }
        }
    }

}
