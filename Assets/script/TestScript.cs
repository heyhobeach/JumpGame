using System.Collections;

//using TreeEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player
{
    private Vector2 destination;
    private float speed = 0.0f;
    private Vector2 preVec = Vector2.zero;

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

    public const byte maxCollsion = 3;
    private byte collsitonCount = 0;//�浹 Ƚ�� count���� �巡�׽� �ʱ�ȭ
    public float dragPower;

    public float scale = 1f;//�߷�

    public bool isTouchBottom = false;

    public float controllRatio = 33;

    public bool reflectTouch = false;//��ġ�� �ݻ� �ϴ� ����

    public bool touchStart = true;//���� ���� �����ϴ°��� �ƴ��� �Ǵ�

    public bool isStay = false;


    float result = 0;

    private float posDistance;


    private float dragCoolTime=0;//��Ÿ�� ���� ����
    private float gravityCoolTime=0;

    public bool isWallHit = false;//�׽�Ʈ�� private�� ���� �ʿ�

    public int reflectCnt;//�浹 ī��Ʈ

    public bool canTouch = true;//���� private�� ���� �ʿ� �浹�� cooltime����� ��ġ ������ ������ ����

    public bool uiTouched = false;//UI ��ġ �ߴ��� �Ǵ��ϴ� �κ�

    public bool isDrag = false;

    public bool isStop = true;

    bool cheack;

    private IEnumerator cooltimeCoroutine;
    private IEnumerator gravityCoroutine;


    void Start()
    {
        //destination = new Vector2(-1.4f, 4);;
        
        position = transform.position;
        //Debug.Log(CameraSet.cameraInstance.GetStartYpos());

        rg2D = GetComponent<Rigidbody2D>();
        rg2D.gravityScale = 0.2f;
        transform.localScale = new Vector2(0.5f, 0.5f);// �ػ� ������ �°� ũ�� ���� �ٵ� ĵ������ ����
        canTouch= false;

        cooltimeCoroutine = DragCooltime();//�Լ��� ������?
        StartCoroutine(cooltimeCoroutine);


        gravityCoroutine = GravityTime();
        StartCoroutine(gravityCoroutine);
        transform.position = CameraSet.cameraInstance.bottom;
        //touchPosx = transform.localPosition.x;
        //screenYpos = CameraSet.limitPos;//screenYpos�� ��ü �ϸ� �� �ش� ��ġ�� ĳ������ ��ġ�� �׻� ���뿡 �������� �̰ź��� ĳ���Ͱ� �� �����ִٸ� ī�޶� ������



        //Debug.Log(string.Format("ȭ�� 1/3�Ʒ�:{0} ȭ�� 2/3�� :{1} ȭ�� �����{2}", -screenYpos, screenYpos,CameraSet.Top.y));
        //Debug.Log(CameraSet.cameraInstance.Top.y);
    }

    private void Awake()
    {

    }



    private void FixedUpdate()
    {
        Move2(player.GetDestinaion());
    }
    // Update is called once per frame
    void Update()
    {
        OnTouchEvent();//��ġ�� ���� ���� player�� ������
    }

    public float setGravity()//�� �Լ��� ���� �ʿ��ұ�
    {
        return scale;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("stay");
        if (isTouchBottom)//���Ұ� Ʈ��� �¿찡 ���̶�� 
        {
            isStay = false;
        }
        else
        {
            isStay = true;
        }
        
        //Debug.Log("���� ����");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("Exit");
        isTouchBottom = false;
        isWallHit = false;
        isStay = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)//�浹�� �߻�
    {
        if (collision.gameObject.name != "bottom") 
        {
            result = collision.GetContact(0).point.x - collision.transform.position.x;
            Debug.Log(string.Format("�⵿ ����{0} ������Ʈ ��ġ{1} ���{2}", collision.GetContact(0).point.x, collision.transform.position.x, result));

            //if (result < 0)
            //{
            //    Debug.Log("����");
//
            //}
            //else
            //{
            //    Debug.Log("������");
            //}

        }

        //Debug.Log(string.Format("{0} �浹 ���� {1} �浹 ��ü ��ǥ {2}", collision.GetContact(0).point.x, collision.transform.position.x, collision.gameObject.name));
        if (collision.gameObject.tag == "border")//�� �� ��迡 �������
        {
            Debug.Log(collision.gameObject.name);

            if (collsitonCount < maxCollsion -1&&collision.gameObject.name!="bottom")//�浹Ƚ��
            {
                collsitonCount++;
            }else if(collsitonCount == maxCollsion - 2 && collision.gameObject.name != "bottom")
            {
                rg2D.gravityScale = scale;
            }
            else if(collsitonCount == maxCollsion - 1 && collision.gameObject.name != "bottom")
            {
                StickWall();
                rg2D.gravityScale = scale;
                return;
            }

            isWallHit = true;//���� �浹�Ұ�� �ش� ������ �־�� ���� �ٽ� ��ġ ������ ����? �ȿ��� �浹�Ͼ �ٽ� ������츦 ���Ѱ�
            destination = Vector2.Reflect(destination, collision.GetContact(0).normal).normalized;//�浹�� ���ݻ�� ���� ���� ����
            player.SetDestination(destination);
            canTouch = true;
            dragCoolTime = 0;//�ð� �ʱ�ȭ
            gravityCoolTime = 0;//�浹�ϰ� �߷� ����Ǹ� �� �Ǳ⿡ �־����
            cheack = false;
            //speed /= 2;//���� �浹�� �ӵ� ����
            Debug.Log("case ��");
            switch (collision.gameObject.name)//����ȭ�� enum���� ����
            {
                case "top"://�ش� �κ��� �Ƹ� �� ���� �����Ű�����
                    //isTouchTop = true;
                    break;
                case "bottom":
                    Debug.Log("�ٴ� ��ġ");
                    isTouchBottom = true;
                    //isTouchBottom = isTouchBottom & cheack;
                    //result = 0;
                    //isStop = true;
                    player.SetDestination(HoldPossion());
                    //gameover();

                    break;
                case "left"://Ȯ�强������ �� �� ���� ����
                    cheack = true;
                    break;
                case "right":
                    cheack = true;
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
                //
                //grabTime += Time.deltaTime;

                if (touch.phase == TouchPhase.Began)
                {
                    //Debug.Log("Began1");
                    HandleTouchBegan(touch);//�����̸� �ȵ�
                }
                else if (touch.phase == TouchPhase.Ended )
                {
                    //Debug.Log("End2");
                    HandleTouchEnd(touch);
                }
                else if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved )//Ȧ���̰ų� �巡�׽�, canTouch�� ���ǿ� �־����
                {

                }


            }
        }

    }

    public void Move2(Vector2 destination)
    {
        transform.Translate(destination.normalized * speed * Time.deltaTime);
    }

    IEnumerator DragCooltime()
    {
        
        while (true)
        {
            
            if (!canTouch)
            {
                dragCoolTime += Time.deltaTime;
                if(dragCoolTime>1)
                {
                   Debug.Log("1�� ����"); ;
                   canTouch=true;
                   dragCoolTime=0;
                }

            }
            yield return null;
        }

        
    }


    IEnumerator GravityTime()
    {
        while (true)
        {//�ð� �����ϰ� �ش� �ð��� �������� �� �����̶��,
            if (!touchStart)
            {
                gravityCoolTime+= Time.deltaTime;
                if (!isWallHit && gravityCoolTime >1)//�浹 �� �ߴٸ� 
                {
                    rg2D.gravityScale = scale;//�߷¼���
                    touchStart= true;
                    gravityCoolTime = 0;
                    Debug.Log("�浹 ���� �߷�");

                }
            }



            yield return null;

        }

    }

    private void HandleTouchBegan(Touch touch)
    {
        touchStart = true;
        isTouchBottom = false;
        gravityCoolTime = 0;
        startPos = Camera.main.ScreenToWorldPoint(touch.position);
        startPos.y -= CameraSet.cameraInstance.GetCurrentYpos() - CameraSet.cameraInstance.GetStartYpos();//ī�޶� Y��ǥ�� ���� ����ؼ� ���� ��ġ�� �� ������ ��� ���� ȭ���� ��ġ �ϵ��� ����
        //Debug.Log("���� :" + startPos);
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
            touchStart = false;
            isTouchBottom = false;
            
            speed = 6f;//���� ���� ������ ���� �����̴ϱ� ����
                       //rg2D.gravityScale = setGravityScale();
                       //���� velocity�̵��̶�� 100������ �����

            endPos = Camera.main.ScreenToWorldPoint(touch.position);
            endPos.y -= CameraSet.cameraInstance.GetCurrentYpos() - CameraSet.cameraInstance.GetStartYpos();
            posDistance = Vector2.Distance(startPos, endPos);
            //}
            collsitonCount = 0;
            if (posDistance < 0.2)//�Ÿ��� ª���� reflectTouch�� false�϶� ����
            {
                Debug.Log("�ܼ� ��ġ");
                
                //destination = Vector2.Reflect(player.GetPreVec(), endPos).normalized;
                if (!reflectTouch)
                {
                    destination.x *= -1;
                    player.SetDestination(destination);
                    reflectTouch = true;
                }
                else
                {
                    return;
                }


            }
            else//�巡�׽�      
            {
                if (canTouch)
                {
                    canTouch = false;
                    reflectTouch = false;//��ġ�� �ݻ�
                    destination = (endPos - startPos).normalized;//���� �ڵ�� ȭ�� ��� ��ġ �ϴ��� ���� �̵� ���⿡ ���� ������
                    destination = VectorCorrection(destination);
                    player.SetDestination(destination);
                    player.SetpreVec(destination);//�̵� ���� ����

                    rg2D.velocity = Vector2.zero;
                    rg2D.gravityScale = 0.0f;
                    //Debug.Log("x ����" + player.GetDestinaion().x);


                    Debug.Log(string.Format("�巡�� ��ȣ {0} �� �浹 ��ȣ{1}", Mathf.Sign(destination.x), Mathf.Sign(result)));

                    if (isStay && Mathf.Sign(destination.x) != Mathf.Sign(result))//���̶� ���� ��ġ�϶�
                    {
                        Debug.Log(player.GetDestinaion());
                        destination.x *= -1;
                        //player.SetDestination(new Vector2(1, 1));
                        player.SetDestination(destination);
                        collsitonCount++;
                    }
                }
            }
            isDrag = false;
        }

    }


    private Vector2 HoldPossion()//��ġ ��ġ ������Ű�� �Լ�
    {
        rg2D.velocity = Vector2.zero;
        //rg2D.gravityScale = 2f;


        transform.Translate(Vector2.zero);//��ġ ����
        transform.position = transform.position;
        return Vector2.zero;
    }
    private Vector2 VectorCorrection(Vector2 pos)//�����ϱ��ؾ��ϴµ� ������ ���·� ���� ���а���
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
        player.SetDestination(HoldPossion());
        if (time > 0.2)
        {
            rg2D.gravityScale += 0.01f;
        }
        else
        {
            rg2D.velocity = Vector2.zero;
            rg2D.gravityScale = 0.0f;
        }

    }


}
