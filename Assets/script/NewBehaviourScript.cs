using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

//using TreeEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 position;
    private Rigidbody2D rg2D;

    private Vector2 startPos;//�ͽ� ���۽� x pos
    private Vector2 endPos;//��ġ ���۽� y pos

    private Vector2 destination;//������

    public float speed = 3f;// private�� �����ϰ� speed= tapPower�� speed=dragPower�� ���� �������� ������

    public float tapPower;
    public float dragPower;

    public float scale = 0.5f;//�߷�

    private float grabTime;//�׷� ���ӽð�
    private float grabCoolTime;//�׷� ��Ÿ�� üũ

    //public bool isTouchTop = false;//isTouchBottom�� ������ ������ �������� �� ��������
    public bool isTouchBottom = false;
    //public bool isTouchLeft = false;
    //public bool isTouchRight = false;

    public float controllRatio = 33;
    private float screenYpos;

    public bool isStart = true;

    public bool touchStart = true;


    //private float touchPosx;

    private float posDistance;

    private float touchStartTime;
    private Vector2 preVelocity;

    private bool isWallHit = false;

    public int reflectCnt;

    public bool canTouch = true;//���� private�� ���� �ʿ�

    public bool uiTouched = false;

    public bool isDrag = false;

    public bool isStop = true;



    void Start()
    {
        //destination = new Vector2(-1.4f, 4);
        transform.position = new Vector2(0, -4.5f);
        position = transform.position;

        rg2D = GetComponent<Rigidbody2D>();
        rg2D.gravityScale = 0.2f;
        transform.localScale = new Vector2(0.5f, 0.5f);// �ػ� ������ �°� ũ�� ���� �ٵ� ĵ������ ����

        //touchPosx = transform.localPosition.x;
        screenYpos = CameraSet.limitPos;//screenYpos�� ��ü �ϸ� �� �ش� ��ġ�� ĳ������ ��ġ�� �׻� ���뿡 �������� �̰ź��� ĳ���Ͱ� �� �����ִٸ� ī�޶� ������

        //Debug.Log(string.Format("ȭ�� 1/3�Ʒ�:{0} ȭ�� 2/3�� :{1} ȭ�� �����{2}", -screenYpos, screenYpos,CameraSet.Top.y));
        Debug.Log(CameraSet.Top.y);
    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        position = rg2D.velocity;// �� �κ��� �ʿ��?
        OnTouchEvent();
        //transform.Translate(destination * speed * Time.deltaTime, Space.World);// �� �κ��� �ʿ䰡 ���°Ͱ���
        /*if (touchStart && transform.position.y >= screenYpos)// �ش� �κ� �ʿ���°� ���� �ּ�ó��
        {
            //isWallHit = true;
        }
        if(!canTouch&&transform.position.y>screenYpos)
        {
            //canTouch = true;
        }*/
        if (grabCoolTime > 1)
        {
            grabCoolTime = 0;
            rg2D.gravityScale = setGravityScale();
            //isWallHit = true;//�� ������ �ߺ� �Ǵ°Ͱ���
            canTouch = true;
        }

    }

    public float setGravityScale()//�� �Լ��� ���� �ʿ��ұ�
    {
        return scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)//�浹�� �߻�
    {
        //Debug.Log("collision");
        Debug.Log(collision.gameObject.name);
        //Vector2 inDirection = GetComponent<Rigidbody2D>().velocity;
        destination = Vector2.Reflect(destination, collision.GetContact(0).normal).normalized;//�浹�� ���ݻ�� ���� ���� ����

        if (collision.gameObject.tag == "border")//�� �� ��迡 �������
        {
            isWallHit = true;//���� �浹�Ұ�� �ش� ������ �־�� ���� �ٽ� ��ġ ������ ����? �ȿ��� �浹�Ͼ �ٽ� ������츦 ���Ѱ�
            canTouch = true;
            speed /= 2;//���� �浹�� �ӵ� ����
            switch (collision.gameObject.name)
            {
                case "top"://�ش� �κ��� �Ƹ� �� ���� �����Ű�����
                    //isTouchTop = true;
                    break;
                case "bottom":
                    isTouchBottom = true;
                    //isTouchLeft = false;
                    //isTouchRight = false;

                    break;
                case "left":
                    //rg2D.gravityScale = setGravityScale();//���� �κп��� ����� ���� �߷��� ���ܹ�����
                    rg2D.gravityScale = scale;
                    //isTouchLeft = true;
                    //isTouchRight = false;

                    break;
                case "right":
                    //rg2D.gravityScale = setGravityScale();
                    rg2D.gravityScale = scale;
                    //isTouchRight = true;
                    //isTouchLeft = false;
                    break;

            }
        }
    }
    private void OnTouchEvent()//�̵� �̺�Ʈ
    {

        if (Input.touchCount == 1)//Input.touchCount > 0
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) != false)//Ui�κ� ��ġ�Ǵ�
            {
                Debug.Log("UiTouch");
                uiTouched = true;
                return;
            }
            else//ui���ƴ�
            {
                Touch touch = Input.GetTouch(0);//ó�� ��ġ�� ����
                grabTime += Time.deltaTime;

                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("Began1");
                    endPos = Camera.main.ScreenToWorldPoint(touch.position);
                    if (!canTouch)
                    {
                        isStop = false;
                    }
                    else
                    {
                        HandleTouchBegan(touch);
                    }
                }
                else if (touch.phase == TouchPhase.Ended && canTouch)
                {
                    isStop = false;
                    Debug.Log("End2");
                    HandleTouchEnd(touch);
                }
                //else if (touch.phase == TouchPhase.Moved)//�ΰ� ��ĥ������ ������
                //{
                    //Debug.Log("Move");
                    //HandleTouchMove(touch);
                //}
                else if (touch.phase == TouchPhase.Stationary|| touch.phase == TouchPhase.Moved)//Ȧ���̰ų� �巡�׽�
                {
                    //Debug.Log("Ȧ�� �׼� �Լ�");
                    if (grabTime > 1||!canTouch)
                    {
                        //rg2D.velocity = preVelocity;
                        Move();
                        return;
                    }
                }

                //}
                else//�� �϶�
                {

                    Move();
                    Debug.Log("destination" + destination);
                    //transform.Translate(destination * speed * Time.deltaTime, Space.World);//�̵� �κ�, destination����ʿ� ������ �κ� Move�Լ��� ��ü
                    //Debug.Log("�����̴��� ��ġ �߻�");
                    //return;
                }


            }
        }
        else//��ġ�� �� �Ǿ�����
        {
            grabTime = 0;//��ġ�ϰ� ���������� 0���� ��������
            //grabCoolTime += Time.deltaTime;//��Ÿ�� ���ϱ�
            if (!isTouchBottom)//�ٴڿ� ���� �ʾ����� ����ؾ��� ������ ���������� ���� ����
            {

                Move();
            }
            else//�ٴ��� ��ġ �Ǿ�����
            {
                transform.position = transform.position;
            }
        }

    }
    private void Move()//destination���� ���� ���شٸ�?
    {

        grabCoolTime += Time.deltaTime;//��Ÿ�� ���ϱ�
        transform.Translate(destination * speed * Time.deltaTime);//�̵� �κ�, destination����ʿ� ������ �κ�
        return;
    }

    private void HandleTouchBegan(Touch touch)
    {
        touchStart = true;
        touchStartTime = Time.time;
        isTouchBottom = false;
        if (canTouch)
        {
            HoldPossion();
        }
        else
        {
            Move();
        }
        
        startPos = Camera.main.ScreenToWorldPoint(touch.position);
        Debug.Log("���� :" + startPos);
    }

    private void Shoot()
    {
        Debug.Log("shoot");
        startPos = Vector2.zero;
        touchStart = true;//�ܼ� ��ġ�⿡ 
        grabTime = 0;
        canTouch = false;
        endPos = new Vector2(startPos.x, 5);//Ȥ�� screenYpos�� ���� -�� ���°ǰ�
        grabCoolTime += Time.deltaTime;//��Ÿ�� ���ϱ�
        //Debug.Log(string.Format("���� ��ġ {0} ȭ�� ��{1} ��ǥ ���� {2}", startPos, 5, endPos));
        speed = 5;
        destination = (endPos - startPos).normalized;//���⸦ ����� �׳� ������
        Debug.Log("shoot" + destination);
        //startPos = Vector2.zero;
        //endPos = Vector2.zero;
        rg2D.gravityScale = setGravityScale();
        return;

    }

    private void HandleTouchEnd(Touch touch)
    {
        if (uiTouched)//ui�϶�
        {
            Debug.Log("UI ��ġ�� ����");
            uiTouched = false;
            return;
        }
        else//ui�κ��� �ƴҶ� 
        {
            //if (isDrag)
            //{
            //touchTime = Time.time - touchStartTime;
            Debug.Log(string.Format("touchTime :{0}", grabTime));
            canTouch = false;
            //isWallHit = false;//�巡�װ� ������ �������� false�� ��ȯ�� ���� ���ϰ� �ϱ�����
            touchStart = false;
            isTouchBottom = false;
            isStart = false;
            //grabCoolTime = 0;//��Ÿ��
            //canTouch = true;
            speed = 3f;//���� ���� ������ ���� �����̴ϱ� ����
                       //rg2D.gravityScale = setGravityScale();

            endPos = Camera.main.ScreenToWorldPoint(touch.position);
            Debug.Log("endoPos :" + endPos);
            //new Vector2(transform.position.x,transform.position.y) �̰� ������ �������� ���� ������ �հ��� ��ġ
            posDistance = Vector2.Distance(startPos, endPos);
            //Debug.Log(string.Format("���� ��ġ :{0}, ���� ��ġ :{1}, �Ÿ� = {2} ", startPos, endPos, posDistance));
            //}
            if (posDistance < 0.2)//�ܼ� ��ġ ��ġ�� �Ÿ��� �Ǵ� 
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
            else
            {
                destination = (endPos - startPos).normalized;//���� �ڵ�� ȭ�� ��� ��ġ �ϴ��� ���� �̵� ���⿡ ���� ������
                destination=VectorCorrection(destination);
                Debug.Log("�巡�� ����"+destination);
            }
            isDrag = false;
            grabCoolTime = 0;//��Ÿ��
        }

    }

    private void HandleTouchMove(Touch touch)
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);//�̷��� �ؾ��� ��⸶�� ȭ���� ��ǥ�� �����ȴ�.
        isDrag = true;//�巡�� Ȯ���� ����

        if (grabTime > 1)
        {

            Debug.Log("Ȧ�� 1�� ����");
            //rg2D.velocity = preVelocity;
            //Shoot();

        }
        if (isStart)//ó�� �巡�� �κ�
        {
            Debug.Log("MoveToward pos =>" + pos);
            transform.position = new Vector2(pos.x, -4.4f);//ī�޶� �ٴ�
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

        transform.Translate(Vector2.zero);//��ġ ����
        transform.position = transform.position;
        return;
    }

    private Vector2 VectorCorrection(Vector2 pos)
    {
        float correctino_posy = 0.0f;
        float correctino_posx = 0.0f;
        if (pos.y <= 0)//0���� �۰ų� ������ ����
        {
            correctino_posy = 0.8f;
        }
        else
        {
            correctino_posy = pos.y;
        }
        Vector2 vector_correction = new Vector2(pos.x,correctino_posy);
        return vector_correction;
    }


}
