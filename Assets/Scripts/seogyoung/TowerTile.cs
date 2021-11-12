using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerTile : MonoBehaviour
{
    private GameObject tower;

    bool isOver = false; 
    void Update()
    {
        if (isOver && Input.GetMouseButtonDown(0))
        { //��Ŭ�� �̺�Ʈ 
            if (tower != null)
            {
                //Ÿ���� ��ġ���¸� Ŭ�� �ȵǰ�
            }
            else
            {
                BulidTower(GameManager.Instance.currentTowerObj);
                Debug.Log("click");
            }
            
        } 
 
    } 
    public void BulidTower(GameObject p_towerObj)
    {
        Tower p_tower = p_towerObj.GetComponent<Tower>(); 
        if (p_tower != null)
        {
            if (GameManager.Instance.coin >=p_tower.price)
            {
                //GameManager.Instance.coin += p_tower.price;
                //�����߰�

                tower = Instantiate(p_towerObj);
                tower.transform.parent = gameObject.transform;//Ÿ���� �ڽ����� ����
                tower.transform.localPosition = new Vector3(0, 0, 0);


            }
            else
            {
                Debug.Log("�� ����");
            }
        }
        else
        {
            Debug.Log("��ġ ���õ� Ÿ�� ����");
        }
        
    }
    void OnMouseOver() {
        if (isOver == false)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                isOver = true;
                Debug.Log("over");
            }


        }
            
    }
    void OnMouseExit()
    {
        if (isOver == true)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                isOver = false;
            }


        }

    }

}
