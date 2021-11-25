using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss :Enemy
{

    public Transform skillObj;
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public float skillTime;
    public LayerMask targetMask;
    public bool useGizmo;
    public List<Transform> visibleTargets = new List<Transform>();
    private List<Transform> hitList = new List<Transform>();

    void Start()
    { //�÷��� �� FindTargetsDelay �ڷ�ƾ�� �����Ѵ�. 0.5�� �������� 
        StartCoroutine("FindTargetsDelay", 0.1f);

    }
    void OnDrawGizmos()
    {
        if (useGizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, viewRadius);
        }
    }
    void useSkill(Tower tower)
    {
        if (tower != null)
        {
            animator.SetTrigger("UseSkill");
            Debug.Log("Tower" + tower.info.name + " cut!");
            tower.SetAttckTime(1.5f);
            Vector3 dirToTarget = (tower.transform.position - transform.position).normalized;
            StartCoroutine(MoveSkillObj(dirToTarget));
        }

    }
    IEnumerator MoveSkillObj(Vector3 dirToTarget)
    {
        skillObj.gameObject.SetActive(true);
        float sec = 0.5f;
        for(int i=0; i < (int)(sec*20); i++)
        {
            skillObj.Translate(dirToTarget * viewRadius/(sec*20), Space.World);
            yield return new WaitForSeconds(0.05f);
        }
        skillObj.localPosition = Vector3.zero;
        skillObj.gameObject.SetActive(false);
    }
    
    
    IEnumerator FindTargetsDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            Tower t=FindTargets();
            if (t != null)
            {
                useSkill(t);                 
                yield return new WaitForSeconds(skillTime);
            }
        }
    }

    Tower FindTargets()
    {
        visibleTargets.Clear();
        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetInViewRadius.Length; i++)
        //ViewRadius �ȿ� �ִ� Ÿ���� ���� = �迭�� �������� i�� ���� �� for ���� 
        {
            Transform target = targetInViewRadius[i].transform; //Ÿ��[i]�� ��ġ 
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            //vector3Ÿ���� Ÿ���� ���� ���� ���� = Ÿ���� ���⺤��, Ÿ���� position - �� ���ӿ�����Ʈ�� position) normalized  = ���� ũ�� ����ȭ = ��������ȭ
            if (Vector3.Angle(forwardDir, dirToTarget) < viewAngle / 2)
            // ���� ���Ϳ� Ÿ�ٹ��⺤���� ũ�Ⱑ �þ߰��� 1/2�̸� = �þ߰� �ȿ� Ÿ�� ���� 
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position); //Ÿ�ٰ��� �Ÿ��� ��� 

                visibleTargets.Add(target.parent);
                Debug.DrawRay(transform.position, dirToTarget * viewRadius, Color.green, 2f);
                
            }
        }
        int idx = 0;
        if (visibleTargets.Count > 0)
        {
            while (visibleTargets.Count > 0)
            {
                idx = Random.Range(0, visibleTargets.Count);
                if (!hitList.Contains(visibleTargets[idx]))
                {
                    hitList.Add(visibleTargets[idx]);
                    return visibleTargets[idx].GetComponent<Tower>();
                }
                visibleTargets.RemoveAt(idx);
            }
            
        }
        return null;


    }
}
