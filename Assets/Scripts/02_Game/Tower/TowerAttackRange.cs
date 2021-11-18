using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerAttackRange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            transform.parent.GetComponent<Tower>().collEnemys.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        int TowerId = transform.parent.GetComponent<Tower>().TowerId;
        List<GameObject> collEnemys = transform.parent.GetComponent<Tower>().collEnemys;

        switch (TowerId)
        {
            case 1:
                break;

            default: // Ÿ��1�� �����ϰ��, ���� ���� ������ ������
                foreach (GameObject go in collEnemys)
                {
                    if (go == collision.gameObject)
                    {
                        collEnemys.Remove(go);
                        break;
                    }
                }
                break;
        }
    }

}
