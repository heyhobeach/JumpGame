using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 position;
    private Rigidbody2D rg2D;

    private Vector2 startPos;//�ͽ� ���۽� x pos
    private Vector2 endPos;//��ġ ���۽� y pos

    private Vector2 destination;

    public float speed = 3f;// private�� �����ϰ� speed= tapPower�� speed=dragPower�� ���� �������� ������

    public float tapPower;
    public float dragPower;

    public float scale = 0.5f;

    private float grabTime;//�׷� ���ӽð�
    private float grabCoolTime;//�׷� ��Ÿ�� üũ

    public bool isTouchTop = false;
    public bool isTouchBottom = false;
    public bool isTouchLeft = false;
    public bool isTouchRight = false;

    public float controllRatio = 33;
    private float screenYpos;

    public bool isStrat = true;

    private bool touchStart = false;


    private float touchPosx;

    private float posDistance;

    private float touchStartTime;
    private float touchTime;

    public bool isWallHit = false;

    public int reflectCnt;

    public bool canTouch=true;//���� private�� ���� �ʿ�

    void Start()
    {
        //destination = new Vector2(-1.4f, 4);
        transform.position = new Vector2(0, -4.5f);
        position = transform.position;

        rg2D = GetComponent<Rigidbody2D>();
        rg2D.gravityScale = 0.2f;
        transform.localScale = new Vector2(0.5f, 0.5f);// �ػ� ������ �°� ũ�� ���� �ٵ� ĵ������ ����

        touchPosx = transform.localPosition.x;
        screenYpos = CameraSet.limitPos;//screenYpos�� ��ü �ϸ� �� �ش� ��ġ�� ĳ������ ��ġ�� �׻� ���뿡 �������� �̰ź��� ĳ���Ͱ� �� �����ִٸ� ī�޶� ������
    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        position = rg2D.velocity;// �� �κ��� �ʿ��?
        OnTouchEvent();
        //transform.Translate(destination * speed * Time.deltaTime, Space.World);
        if (touchStart && transform.position.y >= screenYpos)
        {
            isWallHit = true;
        }
        if(!canTouch&&transform.position.y>screenYpos)
        {
            canTouch = true;
        }

    }

    public float setGravityScale()
    {
        return scale;
    }
    private void OnCollisionEnter2D(Collision2D collision)//�浹�� �߻�
    {
        //Debug.Log("collision");
        Debug.Log(collision.gameObject.name);
        //Vector2 inDirection = GetComponent<Rigidbody2D>().velocity;
        destination = Vector2.Reflect(destination, collision.GetContact(0).normal);

        if (collision.gameObject.tag == "border")//�� �� ��迡 �������
        {
            isWallHit = true;//���� �浹�Ұ�� �ش� ������ �־�� ���� �ٽ� ��ġ ������ ����? �ȿ��� �浹�Ͼ �ٽ� ������츦 ���Ѱ�
            canTouch = true;
            speed /= 2;//���� �浹�� �ӵ� ����
            switch (collision.gameObject.name)
            {
                case "top"://�ش� �κ��� �Ƹ� �� ���� �����Ű�����
                    isTouchTop = true;
                    break;
                case "bottom":
                    isTouchBottom = true;
                    isTouchLeft = false;
                    isTouchRight = false;
                    
                    break;
                case "left":
                    rg2D.gravityScale = setGravityScale();//���� �κп��� ����� ���� �߷��� ���ܹ�����
                    isTouchLeft = true;
                    //isTouchRight = false;

                    break;
                case "right":
                    rg2D.gravityScale = setGravityScale();
                    isTouchRight = true;
                    //isTouchLeft = false;
                    break;

            }
        }
    }

    private void OnTriggerExit(Collider other)//���� ���� if �� �� ����
    {
        //Debug.Log("Exit");
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

        if (canTouch&&Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);//ó�� ��ġ�� ����
            Vector2 screenpos = Camera.main.ScreenToWorldPoint(touch.position);
            
            if (CameraSet.limitPos >= transform.position.y && isWallHit == false)
            {
                Move();
                //canTouch = true;
            }
            else if (CameraSet.limitPos < transform.position.y)
            {
                isWallHit = true; //�浹���� ���� ���� �������� �� ��� �ϱ�����
                Move();
            }
            
            else if (CameraSet.limitPos >= transform.position.y)//���⿡ grabTime<=1 ������ and �� �ְ� 1�� ���ѽð�
            {
                grabTime += Time.deltaTime;

                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("Began");
                    HandleTouchBegan(touch);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    Debug.Log("End");
                    HandleTouchEnd(touch);
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Debug.Log("Move");
                    HandleTouchMove(touch);
                }
                else if (touch.phase == TouchPhase.Stationary)
                {
                    if (!touchStart)//������Ʈ�� �������� �̸� Ȧ�� �ϴ°�� 
                    {
                        Debug.Log("�׳� ��� ��ġ��");
                        Move(); 
                    }
                    else
                    {
                        reflectCnt = 0;//�ݻ� Ƚ�� 
                        Debug.Log("hold :"+grabTime);
                        if (grabTime > 1)//Ȧ�� ���� ������� �̶��� false
                        {
                            Shoot();
                            canTouch = false;
                            rg2D.velocity = Vector2.zero;
                            Debug.Log("1�� ������ ����");
                        }
                    }
                }

            }
            else//�� �϶�
            {

                Move();
                //transform.Translate(destination * speed * Time.deltaTime, Space.World);//�̵� �κ�, destination����ʿ� ������ �κ� Move�Լ��� ��ü
                Debug.Log("���� ��");
            }


        }
        else//��ġ�� �� �Ǿ�����
        {
            grabTime = 0;//��ġ�ϰ� ���������� 0���� ��������
            grabCoolTime += Time.deltaTime;//��Ÿ�� ���ϱ�
            if (!isTouchBottom)//�ٴڿ� ���� �ʾ����� ����ؾ��� ������ ���������� ���� ����
            {

                Move();
                //transform.Translate(destination * speed * Time.deltaTime, Space.World);//�̵� �κ�, destination����ʿ� ������ �κ�
                //Debug.Log("move");
            }
            else//�ٴ��� ��ġ �Ǿ�����
            {
                transform.position = transform.position;
            }
        }
    }

    /*private void HoldTouch(Touch touch)//???? �̰� �� �������
    {
        HoldPossion();
    }*/

    private void Move()
    {
        transform.Translate(destination * speed * Time.deltaTime, Space.World);//�̵� �κ�, destination����ʿ� ������ �κ�
    }

    private void HandleTouchBegan(Touch touch)
    {
        touchStart = true;
        touchStartTime = Time.time;
        if (isWallHit)//�浹 ���� = ������ �̷�� ����
        {
            speed *= 2;
        }
        HoldPossion();
        //isTouchLeft = false;
        //isTouchRight = false;

        rg2D.gravityScale = 0.0f;//�巡�� ���� �߷��� ���־��� ���� �����°��� ����
        isTouchBottom = false;
        //HoldPossion();
        startPos = Camera.main.ScreenToWorldPoint(touch.position);
        Debug.Log("���� :" + startPos);
    }

    private void Shoot()
    {
        
        touchStart = true;//�ܼ� ��ġ�⿡ 
        startPos = transform.position;
        endPos = new Vector2(startPos.x, -screenYpos);
        speed = 5;
        destination = (endPos - startPos).normalized;//���⸦ ����� �׳� ������
        rg2D.gravityScale = setGravityScale();
        
    }

    private void HandleTouchEnd(Touch touch)
    {
        touchTime = Time.time - touchStartTime;
        Debug.Log(string.Format("touchTime :{0}", touchTime));
        isWallHit = false;//�巡�װ� ������ �������� false�� ��ȯ�� ���� ���ϰ� �ϱ�����
        touchStart = false;
        isTouchBottom = false;
        isStrat = false;
        grabCoolTime = 0;//��Ÿ��
        canTouch = true;
        speed = 3f;//���� ���� ������ ���� �����̴ϱ� ����
        //rg2D.gravityScale = setGravityScale();

        endPos = Camera.main.ScreenToWorldPoint(touch.position);
        destination = (endPos - startPos).normalized;//���� �ڵ�� ȭ�� ��� ��ġ �ϴ��� ���� �̵� ���⿡ ���� ������
                                                     //new Vector2(transform.position.x,transform.position.y) �̰� ������ �������� ���� ������ �հ��� ��ġ
        posDistance = Vector2.Distance(startPos, endPos);
        Debug.Log(string.Format("���� ��ġ :{0}, ���� ��ġ :{1}, �Ÿ� = {2} ", startPos, endPos, posDistance));
        if (posDistance < 0.5)//�ܼ� ��ġ ��ġ�� �Ÿ��� �Ǵ� 
        {
            Debug.Log("�ܼ� ��ġ");
            Shoot();
            
            /*rg2D.gravityScale = setGravityScale(); //Shoot�Լ��� �̵�
            touchStart = true;//�ܼ� ��ġ�⿡ 
            startPos = transform.position;
            endPos = new Vector2(startPos.x, -screenYpos);
            speed = 5;
            destination=(endPos-startPos).normalized;*/

        }
    }

    private void HandleTouchMove(Touch touch)
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);//�̷��� �ؾ��� ��⸶�� ȭ���� ��ǥ�� �����ȴ�.


        if (isStrat)//ó�� �巡�� �κ�
        {
            Debug.Log("MoveToward pos =>" + pos);
            transform.position = new Vector2(pos.x, -4.4f);
        }
        else
        {

            //isthrow = true;
            //Debug.Log("�巡�� :" + pos);
        }
    }

    private void HoldPossion()//��ġ ��ġ ������Ű�� �Լ�
    {
        rg2D.velocity = Vector2.zero;
        rg2D.gravityScale = 0.0f;
    }
}
