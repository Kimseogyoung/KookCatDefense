using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFspecialSkill : MonoBehaviour
{

    private List<GameObject> collEnemys = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        attack();
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

    void attack()
    {

        if (collEnemys.Count > 0)
        {
            foreach (GameObject go in collEnemys)
            {
                // ���߿� enemy speedDown�޼ҵ� ȣ���ؼ� �ű⼭ �ð� �־ �ϱ�
                go.GetComponent<Enemy>().speed *= 0.995f;
            }
        }
        
        Destroy(gameObject, 1f);
    }

}
