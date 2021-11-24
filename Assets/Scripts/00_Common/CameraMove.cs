using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour
{

    public float minSize;
    public float maxSize;

    Camera camera;
    bool trigger = false;
    Vector3 oldPos; Vector3 panOrigin;
    public float sensitivity ;
    public float speed;

    public Vector3 originPos;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.localPosition;
        camera = GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") ;
        if(camera.orthographicSize < maxSize && scroll>0)
        {//���
            StartCoroutine(ZoomOut());
            //camera.orthographicSize = Mathf.Min(maxSize, camera.orthographicSize + speed );
        }
        else if (camera.orthographicSize > minSize && scroll < 0)
        {//Ȯ��
            StartCoroutine(ZoomIn());
            
            //camera.orthographicSize = Mathf.Max(minSize, camera.orthographicSize - speed );
            

        }

        if (Input.GetMouseButtonDown(0)) { 
            oldPos = transform.position; 
            panOrigin = camera.ScreenToViewportPoint(Input.mousePosition); 
            return; 
        }
        if (Input.GetMouseButton(0))
        {

            if (camera.orthographicSize < maxSize && !trigger) //ī�޶��� orthographicSize�� �ִ밪���� �۰� trigger�� false�϶� �۵� 
            {
                Vector3 pos = camera.ScreenToViewportPoint(Input.mousePosition) - panOrigin; //moveSpeed�� �����̴� �ӵ� ���� 
                transform.position = oldPos + -pos * sensitivity; //(ī�޶� ����� �پ�� ��) * 10 
            }
        }
               
    }

    IEnumerator  ZoomIn()
    {
        float value = 0;
        float i = 0.3f;
        while (value<speed)
        {
            value += i;
            camera.orthographicSize = Mathf.Max(minSize, camera.orthographicSize - i);
            i *= 0.9f;//���� ��������
            yield return new WaitForSeconds(0.025f);
        }
        
    }
    IEnumerator ZoomOut()
    {
        float value = 0;
        float i = 0.3f;
        while (value < speed)
        {
            value += i;
            camera.orthographicSize = Mathf.Min(maxSize, camera.orthographicSize + i);
            i *= 0.9f;

            //�ܾƿ��� �Ǹ鼭 ��ǥ�� �ʱⰪ�� 0,0,0���� ������ �κ� 
            //���� �̻���..
            Vector3 tempVector = new Vector3(0, 0, -10);
            camera.transform.position = Vector3.Lerp(camera.transform.position, tempVector, 0.1f);


            yield return new WaitForSeconds(0.025f);
        }

    }

    public IEnumerator Shake(float _amount, float _duration)
    {
        float timer = 0;
        while (timer <= _duration)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * _amount + originPos;

            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;

    }
}
