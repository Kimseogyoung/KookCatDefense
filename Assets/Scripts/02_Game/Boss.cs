using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss :Enemy
{

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public float skillTime;
    public LayerMask targetMask;
    public bool useGizmo;
    public List<Transform> visibleTargets = new List<Transform>();

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
        Debug.Log("Tower" + tower.info.name + " cut!");
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
            idx = Random.Range(0, visibleTargets.Count);
            return visibleTargets[idx].GetComponent<Tower>();
        }
        else
        {
            return null;
        }

    }
}
