using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedLauncher : MonoBehaviour
{
    // Start is called before the first frame update

    // skill ���� ����
    float skillGague;
    float maxSkillGauge = 100.0f;
    float chargeTime = 0.5f;
    float attackTime = 0.5f;

    // �� ����Ʈ
    private List<GameObject> collEnemys = new List<GameObject>();

    // �Ѿ�
    public GameObject Feed = null;

    // Ư����ų ����
    public GameObject specialSkillRange = null;

    // �⺻ ���� �ð�
    private float fTime = 0.0f;

    void Start()
    {
        skillGague = 0;
        // maxSkillGauge���� skillGauge�� �۴ٸ�, �ð����� �������ֱ�
        StartCoroutine("chargeSkillGauge", chargeTime);

    }

    // Update is called once per frame
    void Update()
    {
        attack();

        if (skillGague >= maxSkillGauge)
        {
            if (Input.GetMouseButtonDown(0))
            {
                specialSkillAttack();
                //skillGague = 0.0f;
                //StartCoroutine("chargeSkillGauge", chargeTime);
            }
        }

    }

    public IEnumerator chargeSkillGauge(float chargeTime)
    {
        if (skillGague < maxSkillGauge)
        {
            yield return new WaitForSeconds(chargeTime);
            StartCoroutine("chargeSkillGauge", chargeTime);
            Debug.Log("Gauge: " + skillGague);
            skillGague += 10.0f;
        }
    }

    void attack()
    {
        fTime += Time.deltaTime;
        if (collEnemys.Count > 0)
        {
            GameObject target = collEnemys[0];
            
            if (target != null && fTime > attackTime)
            {
                fTime = 0.0f;
                var aFeed = Instantiate(Feed, transform.position, Quaternion.identity, transform);
                aFeed.GetComponent<Feed>().targetPosition = (target.transform.position - transform.position).normalized;
                target.GetComponent<SpriteRenderer>().color = Color.blue;
            }

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            collEnemys.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (GameObject go in collEnemys)
        {
            if (go == collision.gameObject)
            {
                collEnemys.Remove(go);
                break;
            }
        }
    }

    void specialSkillAttack()
    {
        // �ϴ� �ð��� ���߰�
        // Time.timeScale = 0; ���߸� Ư�� ��ų ������ �ȸԾ �ϴ� ����

        // Ŀ���� ����ٴϸ鼭 ���� ���� ǥ��
        var specialSkill = Instantiate(specialSkillRange, transform.position, Quaternion.identity, transform);

        skillGague = 0.0f;
        StartCoroutine("chargeSkillGauge", chargeTime);
    }


}
