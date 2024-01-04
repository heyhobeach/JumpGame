using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 position;
    private Rigidbody2D rg2D;

    private Vector2 startPos;//�ͽ� ���۽� x pos
    private Vector2 endPos;//��ġ ���۽� y pos

    private Vector2 destination;

    public float speed = 1f;

    public bool isTouchTop = false;
    public bool isTouchBottom = false;
    public bool isTouchLeft = false;
    public bool isTouchRight = false;

    private bool isdrag = false;


    void Start()
    {
        Debug.Log("hello");
        //destination = new Vector2(-1.4f, 4);
        transform.position = new Vector2(0, -4);
        position = transform.position;
        rg2D = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector2(0.5f, 0.5f);// �ػ� ������ �°� ũ�� ���� �ٵ� ĵ������ ����
    }

    // Update is called once per frame
    void Update()
    {
        position = rg2D.velocity;
        OnTouchEvent();
        //transform.Translate(destination * speed * Time.deltaTime, Space.World);

    }


    private void OnCollisionEnter2D(Collision2D collision)//�浹�� �߻�
    {
        Debug.Log("collision");
        Debug.Log(collision.gameObject.name);
        Vector2 inDirection = GetComponent<Rigidbody2D>().velocity;
        destination = Vector2.Reflect(destination, collision.GetContact(0).normal);

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
                    //destination.x *= -1;
                    break;
                case "right":
                    isTouchRight = true;
                    isTouchLeft = false;
                    //destination.x *= -1;
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

    private void OnTouchEvent()//�̵� �̺�Ʈ
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);//ó�� ��ġ�� ����
            if (touch.phase == TouchPhase.Began)
            {
                startPos = Camera.main.ScreenToWorldPoint(touch.position);
                Debug.Log("���� :" + startPos);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isdrag = false;
                endPos = Camera.main.ScreenToWorldPoint(touch.position);
                destination = (endPos - startPos).normalized;//���� �ڵ�� ȭ�� ��� ��ġ �ϴ��� ���� �̵� ���⿡ ���� ������
                //new Vector2(transform.position.x,transform.position.y) �̰� ������ �������� ���� ������ �հ��� ��ġ
                Debug.Log("���� :" + endPos);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);//�̷��� �ؾ��� ��⸶�� ȭ���� ��ǥ�� �����ȴ�.
                isdrag = true;
                Debug.Log("�巡�� :" + pos);
            }


        }
        else
        {
            transform.Translate(destination * speed * Time.deltaTime, Space.World);//destination����ʿ�
        }
    }
}
