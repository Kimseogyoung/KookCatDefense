using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class EnemyAttackRange: MonoBehaviour
{
    private Wall attackTarget;

    public event System.Action OnFindWall;
    public event System.Action OnMissWall;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {


    }
    public Wall GetAttckTarget()
    {
        return attackTarget;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Wall")

        {//�浹�� ������Ʈ�� ���� ��
            
            
            if (attackTarget == null ){
                //Ÿ���� ������ ->Ÿ������ ����
                
                
                attackTarget = coll.gameObject.GetComponent<Wall>();

                if (OnFindWall != null)
                {
                    OnFindWall();
                }
            }
            else
            {//�̹� Ÿ�� �� ������

            }
            
            //Enemy���ݸ��
        }
        

    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            if (attackTarget != null)
            {
                attackTarget = null;
                if (OnMissWall != null)
                {
                    OnMissWall();
                }
            }
        }
    }
}
