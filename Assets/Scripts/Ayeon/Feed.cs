using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feed : MonoBehaviour
{

    public Vector3 targetPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(targetPosition * Time.deltaTime * 5.0f);

        // 나와 부모의 사이가 일정거리 도달하면 삭제
        float distance = Vector3.Distance(transform.position, transform.parent.position);
        if (distance > transform.parent.GetComponent<CircleCollider2D>().radius)
        {
            Destroy(gameObject);
        }

        // 화면 밖
        //if (transform.position.x < -0.64f || transform.position.x > 13.44f || transform.position.y < -0.64f || transform.position.y > 8.32f)
        //{
            //Destroy(gameObject);
        //}
    }
}
