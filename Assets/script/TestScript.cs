using System.Collections;

//using TreeEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player
{
    private Vector2 destination;
    private float speed = 0.0f;
    private Vector2 preVec = Vector2.zero;

    public void SetDestination(Vector2 destination)//드래그 이후 벡터 계산이후 Set으로 설정함
    {
        this.destination = destination;
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public void SetpreVec(Vector2 preVec)//드래그 이후 벡터 계산이후 Set으로 설정함
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
    Player player = new Player();//player객체 생성
    private Vector2 position;
    private Rigidbody2D rg2D;



    private Vector2 startPos;//터시 시작시 x pos
    private Vector2 endPos;//터치 시작시 y pos

    private Vector2 destination;//목적지

    public float speed = 3f;// private로 수정하고 speed= tapPower와 speed=dragPower로 수정 가능하지 않을까

    public const byte maxCollsion = 2;
    public byte collsitonCount = 0;//충돌 횟수 count변수 드래그시 초기화
    public float dragPower;

    public float scale = 0.5f;//중력

    private float grabTime;//그랩 지속시간
    private float grabCoolTime;//그랩 쿨타임 체크

    //public bool isTouchTop = false;//isTouchBottom을 제외한 나머지 변수들은 안 쓰고있음
    public bool isTouchBottom = false;
    //public bool isTouchLeft = false;
    //public bool isTouchRight = false;

    public float controllRatio = 33;
    private float screenYpos;

    public bool reflectTouch = false;//��ġ�� �ݻ� �ϴ� ����

    public bool touchStart = true;//손을 떼고 조작하는건지 아닌지 판단

    private float touchTime;


    //private float touchPosx;

    private float posDistance;

    private float touchStartTime;

    private float dragCoolTime=0;//쿨타임 관련 변수
    private float gravityCoolTime=0;

    public bool isWallHit = false;//테스트후 private로 수정 필요

    public int reflectCnt;

    public bool canTouch = true;//추후 private로 수정 필요 충돌과 cooltime계산후 터치 가능한 조건을 만듬

    public bool uiTouched = false;//UI 터치 했는지 판단하는 부분

    public bool isDrag = false;

    public bool isStop = true;

    private IEnumerator cooltimeCoroutine;
    private IEnumerator gravityCoroutine;


    void Start()
    {
        //destination = new Vector2(-1.4f, 4);;
        transform.position = new Vector2(0, -4.5f);
        position = transform.position;
        //Debug.Log(CameraSet.cameraInstance.GetStartYpos());

        rg2D = GetComponent<Rigidbody2D>();
        rg2D.gravityScale = 0.2f;
        transform.localScale = new Vector2(0.5f, 0.5f);// 해상도 비율에 맞게 크기 조절 근데 캔버스가 있음
        canTouch= false;

        cooltimeCoroutine = DragCooltime();//함수로 묶을까?
        StartCoroutine(cooltimeCoroutine);


        gravityCoroutine = GravityTime();
        StartCoroutine(gravityCoroutine);

        //touchPosx = transform.localPosition.x;
        //screenYpos = CameraSet.limitPos;//screenYpos로 교체 하면 됨 해당 위치는 캐릭터의 위치가 항상 이쯤에 있을거임 이거보다 캐릭터가 더 위에있다면 카메라가 움직임



        //Debug.Log(string.Format("화면 1/3아래:{0} 화면 2/3위 :{1} 화면 꼭대기{2}", -screenYpos, screenYpos,CameraSet.Top.y));
        //Debug.Log(CameraSet.cameraInstance.Top.y);
    }

    private void Awake()
    {

    }



    private void FixedUpdate()
    {
        //Debug.Log(player.GetSpeed());
        Move2(player.GetDestinaion());
        //Move2(Vector2.up * 2 * Time.deltaTime);
        //Debug.Log(player.GetDestinaion());


    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(TempPanel.Instance.inputState);
        //CheckTime();
        //player.SetDestination(player.GetDestinaion());
        if (!canTouch)//터치 못 할때 코루틴 호출
        {


        }

        if (isStop)//isDrag라고 그냥 임의로 변수 설정한것 ,true일때
        {
            //player.SetDestination(HoldPossion());
        }
        else
        {
            //player.SetDestination(player.GetDestinaion());

            //    //player.SetDestination(player.GetDestinaion());
        }//여기 까지 그냥 테스트를 위해 player객체에서 정보 받아 움직이는지 확인을 위한 코드

        OnTouchEvent();//터치로 인한 값을 player에 셋팅함

    }

    public float setGravity
        ()//이 함수가 굳이 필요할까
    {
        return scale;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("Exit");
        isWallHit = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)//충돌시 발생
    {
        //Debug.Log("collision");
        //Debug.Log(collision.gameObject.name);
        //Vector2 inDirection = GetComponent<Rigidbody2D>().velocity;
        //destination = Vector2.Reflect(destination, collision.GetContact(0).normal).normalized;//충돌시 전반사로 벡터 방향 수정
        //player.SetDestination(destination);
        //player.SetDestination(Vector2.zero);
        //StickWall();//벽에 붙어있는거

        if (collision.gameObject.tag == "border")//좌 우 경계에 닿을경우
        {
            if (collsitonCount < maxCollsion - 1)//충돌횟수
            {
                collsitonCount++;
            }
            else
            {
                rg2D.gravityScale = scale;
            }

            isWallHit = true;//벽에 충돌할경우 해당 라인이 있어야 만약 다시 터치 가능한 범위? 안에서 충돌일어나 다시 잡을경우를 위한것
            destination = Vector2.Reflect(destination, collision.GetContact(0).normal).normalized;//충돌시 전반사로 벡터 방향 수정
            player.SetDestination(destination);
            canTouch = true;
            dragCoolTime = 0;//시간 초기화
            gravityCoolTime = 0;//충돌하고 중력 적용되면 안 되기에 있어야함
            //speed /= 2;//벽에 충돌시 속도 감속
            switch (collision.gameObject.name)
            {
                case "top"://해당 부분은 아마 쓸 일은 없을거같긴함
                    //isTouchTop = true;
                    break;
                case "bottom":
                    isTouchBottom = true;
                    isStop = true;
                    player.SetDestination(HoldPossion());
                    //gameover();

                    break;
                case "left"://확장성때문에 안 쓸 수도 있음


                    break;
                case "right":

                    break;

            }
        }
    }
    private void OnTouchEvent()//터치를 통한 조작을 판단
    {
        switch(TempPanel.Instance.inputState)
        {
            case TempPanel.InputState.None:

            break;
            case TempPanel.InputState.Touch:
            if(!canTouch) break;
            destination = Vector2.Reflect(player.GetPreVec(), endPos).normalized;

            canTouch = false;
            touchStart = false;
            break;
            case TempPanel.InputState.Drag:
            if(!canTouch) break;
            destination = TempPanel.Instance.dir;//현재 코드는 화면 어디를 터치 하더라도 같은 이동 방향에 따라 움직임
            destination = VectorCorrection(destination);
            player.SetDestination(destination);
            player.SetpreVec(destination);//이동 벡터 저장

            rg2D.velocity = Vector2.zero;
            rg2D.gravityScale = 0.0f;

            touchStart = false;
            canTouch = false;
            break;
            case TempPanel.InputState.Hold:

            break;
            default:
            
            break;
        }
        // if (Input.touchCount > 0)//터치가 될경우
        // {
        //     if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) != false)//Ui부분 터치판단
        //     {
        //         Debug.Log("UiTouch");
        //         uiTouched = true;
        //         return;
        //     }
        //     else//ui가아님
        //     {
        //         Touch touch = Input.GetTouch(0);//처음 터치된 정보
        //         grabTime += Time.deltaTime;

        //         if (touch.phase == TouchPhase.Began && canTouch)
        //         {
        //             //Debug.Log("Began1");
        //             HandleTouchBegan(touch);//움직이면 안됨
        //         }
        //         else if (touch.phase == TouchPhase.Ended && canTouch)
        //         {
        //             //Debug.Log("End2");
        //             HandleTouchEnd(touch);
        //         }
        //         else if ((touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) && canTouch)//홀딩이거나 드래그시, canTouch를 조건에 넣어야함
        //         {

        //         }


        //     }
        // }
        // else//터치가 안 되었을때
        // {
        //     grabTime = 0;//터치하고 있지않을때 0으로 만들어야함
        //     //grabCoolTime += Time.deltaTime;//쿨타임 더하기
        //     if (!isTouchBottom)//바닥에 닿지 않았을때 계산해야지 밑으로 내려가려는 힘이 없음
        //     {
        //         isStop = false;
        //     }
        //     else//바닥이 터치 되었을때 collision에서 holdPossion을 하면 되지 않을까
        //     {

        //     }
        // }

    }

    public void Move2(Vector2 destination)
    {
        //rg2D.gravityScale = 0f;
        //Vector2 now = transform.position;
        //rg2D.MovePosition(now + destination * Time.deltaTime * speed);
        //rg2D.velocity = destination.normalized * 100 * Time.deltaTime;//rigidbody를 이용한 이동 speed 100 
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
                   Debug.Log("1초 지남"); ;
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
        {//시간 측정하고 해당 시간이 지났을때 이 조건이라면,
            if (!touchStart)
            {
                gravityCoolTime+= Time.deltaTime;
                if (!isWallHit && gravityCoolTime >1)//충돌 안 했다면 
                {
                    rg2D.gravityScale = scale;//중력설정
                    touchStart= true;
                    gravityCoolTime = 0;
                    Debug.Log("충돌 없는 중력");

                }
            }



            yield return null;

        }

    }

    private void HandleTouchBegan(Touch touch)
    {
        touchStart = true;
        touchStartTime = Time.time;
        isTouchBottom = false;
        gravityCoolTime = 0;
        if (canTouch)//터치 가능할때 조작 추가로 적기 위해
        {
            //player.SetDestination(HoldPossion());//주석 처리 함으로서 멈추는것 없이 만든다
            //isStop = true;
        }

        startPos = Camera.main.ScreenToWorldPoint(touch.position);
        startPos.y -= CameraSet.cameraInstance.GetCurrentYpos() - CameraSet.cameraInstance.GetStartYpos();//ī�޶� Y��ǥ�� ���� ����ؼ� ���� ��ġ�� �� ������ ��� ���� ȭ���� ��ġ �ϵ��� ����
        //Debug.Log("���� :" + startPos);
    }

    /*private void Shoot()//현재 사용하지 않음
    {
        Debug.Log("shoot");
        startPos = Vector2.zero;
        touchStart = true;//단순 터치기에 
        grabTime = 0;
        canTouch = false;
        endPos = new Vector2(startPos.x, 5);//혹시 screenYpos의 값이 -가 나는건가
        grabCoolTime += Time.deltaTime;//쿨타임 더하기
        //Debug.Log(string.Format("도형 위치 {0} 화면 끝{1} 목표 지점 {2}", startPos, 5, endPos));
        speed = 5;
        destination = new Vecotor2(0.0,1.0f).normalized;//여기를 지우면 그냥 떨어짐
        Debug.Log("shoot" + destination);
        //startPos = Vector2.zero;
        //endPos = Vector2.zero;
        rg2D.gravityScale = setGravityScale();
        return;

    }*/

    private void HandleTouchEnd(Touch touch)
    {
        if (uiTouched)//ui일때
        {
            Debug.Log("UI 터치로 종료");
            uiTouched = false;
            return;
        }
        else//ui부분이 아닐때 
        {
            //if (isDrag)
            //{7
            touchTime = Time.time - touchStartTime;
            //Debug.Log(string.Format("touchTime :{0}", grabTime));
            canTouch = false;//나중에 드래그 안으로 넣어야함
            //isWallHit = false;//드래그가 끝나는 시점에서 false로 전환해 잡지 못하게 하기위함
            touchStart = false;
            isTouchBottom = false;
            
            speed = 6f;//���� ���� ������ ���� �����̴ϱ� ����
                       //rg2D.gravityScale = setGravityScale();
                       //만약 velocity이동이라면 100정도는 줘야함

            endPos = Camera.main.ScreenToWorldPoint(touch.position);
            endPos.y -= CameraSet.cameraInstance.GetCurrentYpos() - CameraSet.cameraInstance.GetStartYpos();
            //Debug.Log("endoPos :" + endPos);
            //new Vector2(transform.position.x,transform.position.y) 이걸 넣으면 도형으로 부터 마지막 손가락 위치
            posDistance = Vector2.Distance(startPos, endPos);
            Debug.Log(string.Format("���� ��ġ :{0}, ���� ��ġ :{1}, �Ÿ� = {2} ", startPos, endPos, posDistance));
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
                reflectTouch = false;//��ġ�� �ݻ�
                destination = (endPos - startPos).normalized;//���� �ڵ�� ȭ�� ��� ��ġ �ϴ��� ���� �̵� ���⿡ ���� ������
                destination = VectorCorrection(destination);
                player.SetDestination(destination);
                player.SetpreVec(destination);//이동 벡터 저장

                rg2D.velocity = Vector2.zero;
                rg2D.gravityScale = 0.0f;
                //Debug.Log("드래그 벡터" + destination);
            }
            isDrag = false;
        }

    }

    /*private void HandleTouchMove(Touch touch)
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);//이렇게 해야지 기기마다 화면의 좌표로 설정된다.
        isDrag = true;//드래그 확인을 위함

        if (grabTime > 1)
        {

            Debug.Log("홀딩 1초 지남");
            //rg2D.velocity = preVelocity;
            //Shoot();

        }
        if (isStart)//처음 드래그 부분
        {
            Debug.Log("MoveToward pos =>" + pos);
            transform.position = new Vector2(pos.x, -4.4f);//카메라 바닥
        }
        else
        {

            //isthrow = true;
            //Debug.Log("드래그 :" + pos);
        }
    }*/

    private Vector2 HoldPossion()//위치 위치 고정시키는 함수
    {
        rg2D.velocity = Vector2.zero;
        //rg2D.gravityScale = 2f;


        transform.Translate(Vector2.zero);//위치 고정
        transform.position = transform.position;
        return Vector2.zero;
    }
    private Vector2 VectorCorrection(Vector2 pos)//수정하긴해야하는데 수정된 상태로 들어가서 냅둔거임
    {
        float correctino_posy = 0.0f;
        float correctino_posx = 0.0f;
        if (pos.y <= 0)//0보다 작거나 같으면 보정
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

    private void StickWall()//벽에 붙어서 미끄러지는 함수
    {
        float time = Time.time;
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
