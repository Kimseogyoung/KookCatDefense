using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpawner : MonoBehaviour
{
    [SerializeField]
    private TowerDataViewer towerDataViewer;
    [SerializeField]
    private GameObject[] skillPrefab;
    [SerializeField]
    private GameObject[] followskillPrefab;
    private bool isOnSkillButton = false;
    private GameObject followSkillClone = null;
    private GameObject skillClone = null;
    private int skillType;
    private void Start()
    {
        
        for (int i = 0; i < skillPrefab.Length; i++)
        {
            skillPrefab[i] = Resources.Load<GameObject>("Prefabs/Skill/Skill" + GameData.Instance.selectedSkills[i]);
            followskillPrefab[i] = Resources.Load<GameObject>("Prefabs/UI/FollowSkill" + GameData.Instance.selectedSkills[i]); 
        }
    }
    public void ReadytoSpawnSkill(int type)
    {
        skillType = type;

        towerDataViewer.OnPanelSkill(skillPrefab[skillType]);

        Skill skill = skillPrefab[skillType].GetComponent<Skill>();

        if (isOnSkillButton == true)
        {
            return;
        }

        if (skill.Price > GameManager.Instance.coin)
        {
            return;
        }
        
        isOnSkillButton = true;

        followSkillClone = Instantiate(followskillPrefab[skillType]);

        StartCoroutine("OnSkillCancelSystem");
    }

    public void SpawnSkill(Vector3 tileTransform)
    {
        if (isOnSkillButton == false)
        {
            return;
        }

        //Tile tile = tileTransform.GetComponent<Tile>();

        isOnSkillButton = false;

        Skill skill = skillPrefab[skillType].GetComponent<Skill>();

        GameManager.Instance.coin -= (int)skill.Price;

        skillClone = Instantiate(skillPrefab[skillType], tileTransform, Quaternion.identity);

        Destroy(followSkillClone);

        StopCoroutine("OnSkillCancelSystem");

        towerDataViewer.OffPanel();

        //Destroy(skillClone, 5); �̰� ��ų���� �ٸ��� ����ǰ� �ٲ��ּ���
        //if 
        //skill script�� �Ҹ�ð� �޾Ƽ� �ڷ�ƾ���� ¥��(��ü �Ҹ������ʴ� �� �̶� ���� )
        //��ų 3�� �ִϸ��̼� �̺�Ʈ�� �ڷ�ƾ ���� �Ҹ��ؼ� ���⼭ ���� ������� �ȵ� �� ���ƿ�
        if(skill.info.id==0 || skill.info.id == 1 || skill.info.id == 2)//�ӽ�
            Destroy(skillClone, 5);
    }

    private IEnumerator OnSkillCancelSystem()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                isOnSkillButton = false;
                Destroy(followSkillClone);
                towerDataViewer.OffPanel();
                break;
            }

            yield return null;
        }
    }
}
