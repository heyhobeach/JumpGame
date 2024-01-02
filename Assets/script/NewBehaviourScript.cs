using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 position;

    private Vector2 startPos;//�ͽ� ���۽� x pos
    private Vector2 endPos;//��ġ ���۽� y pos

    public float speed = 2f;

    public bool isTouchTop = false;
    public bool isTouchBottom = false;
    public bool isTouchLeft = false;
    public bool isTouchRight = false;

    private bool isdrag = false;

    void Start()
    {
        Debug.Log("hello");
        transform.position = new Vector2(0, -4);
        position= transform.position;
        //transform.localScale = new Vector2(0.5f, 0.5f);// �ػ� ������ �°� ũ�� ���� �ٵ� ĵ������ ����
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isTouchLeft)
        //{
        //    position.x += -0.5f * speed * Time.deltaTime;
        //    transform.position = position;
        //}else if (isTouchLeft)
        //{
        //    position.x += 0.5f * speed * Time.deltaTime;
        //    transform.position = position;
        //}

        //if (isTouchLeft)
        //{
        //    position.x = -1;
        //}
        //if (Input.GetMouseButtonDown(0))
        //{
        //   Debug.Log("click");
        //}
        //Debug.Log(Input.touchCount);
        OnTouchEvent();
    }

    /*private void OnMouseDrag()
    {
        //Debug.Log("Drag");
        isdrag = true;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position = pos;
        transform.position = pos;
        //Debug.Log(transform.position);
        //transform.position=ray

    }*/

    private void OnMouseUp()
    {
        Debug.Log("Ŭ����");
        isdrag = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");
        if (collision.gameObject.tag == "border")//�� �� ��迡 �������
        {
            switch (collision.gameObject.name)
            {
                case "top":
                    isTouchTop = true;
                    break;
                case "bottom":
                    isTouchBottom = true;
                    break;
                case "left":
                    isTouchLeft = true;
                    isTouchRight = false;
                    break;
                case "right":
                    isTouchRight = true;
                    isTouchLeft = false;
                    break;

            }
        }
    }

    private void OnTriggerExit(Collider other)//���� ���� if �� �� ����
    {
        Debug.Log("Exit");
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "border")//�� �� ��迡 �������
        {
            Debug.Log("������");
            switch (other.gameObject.name)
            {
                case "top":
                    isTouchTop = false;
                    break;
                case "bottom":
                    isTouchBottom = false;
                    break;
                case "left":
                    isTouchLeft = false;
                    break;
                case "right":
                    isTouchRight = false;
                    break;

            }
        }
    }

    private void OnTouchEvent()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);//ó�� ��ġ�� ����
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("��ġ ����");
                startPos= Camera.main.ScreenToWorldPoint(touch.position);
                //transform.position = startPos;
                Debug.Log("���� :"+startPos);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isdrag = false;
                endPos = Camera.main.ScreenToWorldPoint(touch.position);
                Debug.Log("���� :"+endPos);
                //transform.position = Vector2.MoveTowards(startPos, endPos,speed*Time.deltaTime);
                Debug.Log("��ġ ����");
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);//�̷��� �ؾ��� ��⸶�� ȭ���� ��ǥ�� �����ȴ�.
                isdrag = true;
                Debug.Log("�巡�� :"+pos);
                //position.x = pos.x;
                //transform.position = position;
            }


        }
        //transform.position = Vector2.MoveTowards(startPos, endPos, speed * Time.deltaTime);
    }
}
