using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

//using TreeEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Player
{
    private Vector2 destination;
    private float speed = 0.0f;
    private Vector2 preVec= Vector2.zero;

    public void SetDestination(Vector2 destination)//�巡�� ���� ���� ������� Set���� ������
    {
        this.destination = destination;
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetpreVec(Vector2 preVec)//�巡�� ���� ���� ������� Set���� ������
    {
        this.preVec = preVec;
    }
    public Vector2 GetDestinaion()
    {
        return destination;
    }
    public Vector2 GetPreVec()
    {
        return preVec;
    }


    public float GetSpeed() 
    {
        return speed;
    }
}

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    Player player = new Player();//player��ü ����
    private Vector2 position;
    private Rigidbody2D rg2D;
    

    
    private Vector2 startPos;//�ͽ� ���۽� x pos
    private Vector2 endPos;//��ġ ���۽� y pos

    private Vector2 destination;//������

    public float speed = 3f;// private�� �����ϰ� speed= tapPower�� speed=dragPower�� ���� �������� ������

    public const byte maxCollsion=2;
    public byte collsitonCount = 0;//�浹 Ƚ�� count���� �巡�׽� �ʱ�ȭ
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

    public bool touchStart = true;//���� ���� �����ϴ°��� �ƴ��� �Ǵ�

    private float touchTime;


    //private float touchPosx;

    private float posDistance;

    private float touchStartTime;
    private Vector2 preVelocity;

    public bool isWallHit = false;//�׽�Ʈ�� private�� ���� �ʿ�

    public int reflectCnt;

    public bool canTouch = true;//���� private�� ���� �ʿ� �浹�� cooltime����� ��ġ ������ ������ ����

    public bool uiTouched = false;//UI ��ġ �ߴ��� �Ǵ��ϴ� �κ�

    public bool isDrag = false;

    public bool isStop = true;

    private IEnumerator cooltimeCoroutine;



    void Start()
    {
        //destination = new Vector2(-1.4f, 4);;
        transform.position = new Vector2(0, -4.5f);
        position = transform.position;

        rg2D = GetComponent<Rigidbody2D>();
        rg2D.gravityScale = 0.2f;
        transform.localScale = new Vector2(0.5f, 0.5f);// �ػ� ������ �°� ũ�� ���� �ٵ� ĵ������ ����

        //touchPosx = transform.localPosition.x;
        //screenYpos = CameraSet.limitPos;//screenYpos�� ��ü �ϸ� �� �ش� ��ġ�� ĳ������ ��ġ�� �׻� ���뿡 �������� �̰ź��� ĳ���Ͱ� �� �����ִٸ� ī�޶� ������
        cooltimeCoroutine = DragCooltime();


        //Debug.Log(string.Format("ȭ�� 1/3�Ʒ�:{0} ȭ�� 2/3�� :{1} ȭ�� �����{2}", -screenYpos, screenYpos,CameraSet.Top.y));
        Debug.Log(CameraSet.Top.y);
    }

    private void Awake()
    {
        
    }


    private void FixedUpdate()
    {
        //Debug.Log(player.GetSpeed());
        Move2(player.GetDestinaion());
        //Debug.Log(player.GetDestinaion());


    }
    // Update is called once per frame
    void Update()
    {
        //player.SetDestination(player.GetDestinaion());
        if (!canTouch)//��ġ �� �Ҷ� �ڷ�ƾ ȣ��
        {
            StartCoroutine(cooltimeCoroutine);
        }
        
        if (isStop)//isDrag��� �׳� ���Ƿ� ���� �����Ѱ� ,true�϶�
        {
            //player.SetDestination(HoldPossion());
        }
        else
        {
            //player.SetDestination(player.GetDestinaion());
            
        //    //player.SetDestination(player.GetDestinaion());
        }//���� ���� �׳� �׽�Ʈ�� ���� player��ü���� ���� �޾� �����̴��� Ȯ���� ���� �ڵ�

        OnTouchEvent();//��ġ�� ���� ���� player�� ������

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
        //destination = Vector2.Reflect(destination, collision.GetContact(0).normal).normalized;//�浹�� ���ݻ�� ���� ���� ����
        //player.SetDestination(destination);
        //player.SetDestination(Vector2.zero);
        //StickWall();

        if (collision.gameObject.tag == "border")//�� �� ��迡 �������
        {
            if (collsitonCount < maxCollsion-1)
            {
                collsitonCount++;
            }
            else
            {
                player.SetDestination(HoldPossion());
                Debug.Log("�浹 ��");
                return;
            }
            
            isWallHit = true ;//���� �浹�Ұ�� �ش� ������ �־�� ���� �ٽ� ��ġ ������ ����? �ȿ��� �浹�Ͼ �ٽ� ������츦 ���Ѱ�
            destination = Vector2.Reflect(destination, collision.GetContact(0).normal).normalized;//�浹�� ���ݻ�� ���� ���� ����
            player.SetDestination(destination);
            canTouch = true;
            StopCoroutine(cooltimeCoroutine);
            //speed /= 2;//���� �浹�� �ӵ� ����
            switch (collision.gameObject.name)
            {
                case "top"://�ش� �κ��� �Ƹ� �� ���� �����Ű�����
                    //isTouchTop = true;
                    break;
                case "bottom":
                    isTouchBottom = true;
                    isStop = true;
                    player.SetDestination(HoldPossion());

                    break;
                case "left":
                    //rg2D.gravityScale = setGravityScale();//���� �κп��� ����� ���� �߷��� ���ܹ�����
                    rg2D.gravityScale = scale;

                    break;
                case "right":
                    //rg2D.gravityScale = setGravityScale();
                    rg2D.gravityScale = scale;
                    break;

            }
        }
    }
    private void OnTouchEvent()//��ġ�� ���� ������ �Ǵ�
    {

        if (Input.touchCount > 0)//��ġ�� �ɰ��
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

                if (touch.phase == TouchPhase.Began && canTouch)
                {
                    Debug.Log("Began1");
                    HandleTouchBegan(touch);//�����̸� �ȵ�
                }
                else if (touch.phase == TouchPhase.Ended && canTouch)
                {
                    Debug.Log("End2");
                    HandleTouchEnd(touch);
                }
                else if (touch.phase == TouchPhase.Stationary && canTouch || touch.phase == TouchPhase.Moved && canTouch)//Ȧ���̰ų� �巡�׽�, canTouch�� ���ǿ� �־����
                {

                }


            }
        }
        else//��ġ�� �� �Ǿ�����
        {
            grabTime = 0;//��ġ�ϰ� ���������� 0���� ��������
            //grabCoolTime += Time.deltaTime;//��Ÿ�� ���ϱ�
            if (!isTouchBottom)//�ٴڿ� ���� �ʾ����� ����ؾ��� ������ ���������� ���� ����
            {
                isStop = false;
            }
            else//�ٴ��� ��ġ �Ǿ����� collision���� holdPossion�� �ϸ� ���� ������
            {
                                    
            }
        }

    }

    public void Move2(Vector2 destination)
    {
        rg2D.gravityScale = 0.0f;
        rg2D.velocity = destination.normalized * speed * Time.deltaTime;//rigidbody�� �̿��� �̵� speed 100 
        //transform.Translate(destination * speed * Time.deltaTime);
    }

    IEnumerator DragCooltime()
    {
        yield return new WaitForSeconds(1.0f);//1�ʰ� �����ٸ�
        Debug.Log("�ڷ�ƾ ����");
        canTouch = true;
    }
    private void Move()//destination���� ���� ���شٸ�?
    {

        grabCoolTime += Time.deltaTime;//��Ÿ�� ���ϱ�
        //transform.Translate(destination * speed * Time.deltaTime);//�̵� �κ�, destination����ʿ� ������ �κ�
        //rg2D.velocity=destination * speed * Time.deltaTime
        return;
    }

    private void HandleTouchBegan(Touch touch)
    {
        touchStart = true;
        touchStartTime = Time.time;
        isTouchBottom = false;
        if (canTouch)
        {
            player.SetDestination(HoldPossion());
            isStop = true;
        }

        startPos = Camera.main.ScreenToWorldPoint(touch.position);
        Debug.Log("���� :" + startPos);
    }

    /*private void Shoot()//���� ������� ����
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
        destination = new Vecotor2(0.0,1.0f).normalized;//���⸦ ����� �׳� ������
        Debug.Log("shoot" + destination);
        //startPos = Vector2.zero;
        //endPos = Vector2.zero;
        rg2D.gravityScale = setGravityScale();
        return;

    }*/

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
            touchTime = Time.time - touchStartTime;
            Debug.Log(string.Format("touchTime :{0}", grabTime));
            canTouch = false;
            //isWallHit = false;//�巡�װ� ������ �������� false�� ��ȯ�� ���� ���ϰ� �ϱ�����
            touchStart = false;
            isTouchBottom = false;
            isStart = false;
            speed = 100f;//���� ���� ������ ���� �����̴ϱ� ����
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
                //Shoot();
                //isStop = false;
                Debug.Log("�÷��̾� ������ :"+player.GetPreVec());
                destination = Vector2.Reflect(player.GetPreVec(), endPos).normalized;
                Debug.Log(destination);
                //player.SetDestination(destination);

                /*rg2D.gravityScale = setGravityScale(); //Shoot�Լ��� �̵�
                touchStart = true;//�ܼ� ��ġ�⿡ 
                startPos = transform.position;
                endPos = new Vector2(startPos.x, -screenYpos);
                speed = 5;
                destination=(endPos-startPos).normalized;*/

            }
            else//�巡�׽�  
            {
                destination = (endPos - startPos).normalized;//���� �ڵ�� ȭ�� ��� ��ġ �ϴ��� ���� �̵� ���⿡ ���� ������
                destination = VectorCorrection(destination);
                player.SetDestination(destination);
                player.SetpreVec(destination);//�̵� ���� ����
                collsitonCount = 0;
                Debug.Log("�巡�� ����" + destination);
            }
            isDrag = false;
            grabCoolTime = 0;//��Ÿ��
        }

    }

    /*private void HandleTouchMove(Touch touch)
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
    }*/

    private Vector2 HoldPossion()//��ġ ��ġ ������Ű�� �Լ�
    {
        rg2D.velocity = Vector2.zero;
        rg2D.gravityScale = 2f;


        transform.Translate(Vector2.zero);//��ġ ����
        transform.position = transform.position;
        return Vector2.zero;
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
        Vector2 vector_correction = new Vector2(pos.x, correctino_posy);
        return vector_correction;
    }
    
    private void StickWall()//���� �پ �̲������� �Լ�
    {
        float time = Time.time;
        if (time > 0.2)
        {
            rg2D.gravityScale += 0.01f;
        }
        else
        {
            rg2D.velocity= Vector2.zero;
            rg2D.gravityScale = 0.0f;
        }
        
    }


}