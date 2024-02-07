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

    public const byte maxCollsion = 3;
    private byte collsitonCount = 0;//충돌 횟수 count변수 드래그시 초기화
    public float dragPower;

    public float scale = 1f;//중력

    public bool isTouchBottom = false;

    public float controllRatio = 33;

    public bool reflectTouch = false;//터치시 반사 하는 변수

    public bool touchStart = true;//손을 떼고 조작하는건지 아닌지 판단

    public bool isStay = false;


    float result = 0;

    private float posDistance;


    private float dragCoolTime=0;//쿨타임 관련 변수
    private float gravityCoolTime=0;

    public bool isWallHit = false;//테스트후 private로 수정 필요

    public int reflectCnt;//충돌 카운트

    public bool canTouch = true;//추후 private로 수정 필요 충돌과 cooltime계산후 터치 가능한 조건을 만듬

    public bool uiTouched = false;//UI 터치 했는지 판단하는 부분

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
        transform.localScale = new Vector2(0.5f, 0.5f);// 해상도 비율에 맞게 크기 조절 근데 캔버스가 있음
        canTouch= false;

        cooltimeCoroutine = DragCooltime();//함수로 묶을까?
        StartCoroutine(cooltimeCoroutine);


        gravityCoroutine = GravityTime();
        StartCoroutine(gravityCoroutine);
        transform.position = CameraSet.cameraInstance.bottom;
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
        Move2(player.GetDestinaion());
    }
    // Update is called once per frame
    void Update()
    {
        OnTouchEvent();//터치로 인한 값을 player에 셋팅함
    }

    public float setGravity()//이 함수가 굳이 필요할까
    {
        return scale;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("stay");
        if (isTouchBottom)//바텀과 트루고 좌우가 참이라면 
        {
            isStay = false;
        }
        else
        {
            isStay = true;
        }
        
        //Debug.Log("붙은 상태");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("Exit");
        isTouchBottom = false;
        isWallHit = false;
        isStay = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)//충돌시 발생
    {
        if (collision.gameObject.name != "bottom") 
        {
            result = collision.GetContact(0).point.x - collision.transform.position.x;
            Debug.Log(string.Format("출동 지점{0} 오브젝트 위치{1} 결과{2}", collision.GetContact(0).point.x, collision.transform.position.x, result));

            //if (result < 0)
            //{
            //    Debug.Log("왼쪽");
//
            //}
            //else
            //{
            //    Debug.Log("오른쪽");
            //}

        }

        //Debug.Log(string.Format("{0} 충돌 지점 {1} 충돌 물체 좌표 {2}", collision.GetContact(0).point.x, collision.transform.position.x, collision.gameObject.name));
        if (collision.gameObject.tag == "border")//좌 우 경계에 닿을경우
        {
            Debug.Log(collision.gameObject.name);

            if (collsitonCount < maxCollsion -1&&collision.gameObject.name!="bottom")//충돌횟수
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

            isWallHit = true;//벽에 충돌할경우 해당 라인이 있어야 만약 다시 터치 가능한 범위? 안에서 충돌일어나 다시 잡을경우를 위한것
            destination = Vector2.Reflect(destination, collision.GetContact(0).normal).normalized;//충돌시 전반사로 벡터 방향 수정
            player.SetDestination(destination);
            canTouch = true;
            dragCoolTime = 0;//시간 초기화
            gravityCoolTime = 0;//충돌하고 중력 적용되면 안 되기에 있어야함
            cheack = false;
            //speed /= 2;//벽에 충돌시 속도 감속
            Debug.Log("case 전");
            switch (collision.gameObject.name)//최적화시 enum으로 수정
            {
                case "top"://해당 부분은 아마 쓸 일은 없을거같긴함
                    //isTouchTop = true;
                    break;
                case "bottom":
                    Debug.Log("바닥 터치");
                    isTouchBottom = true;
                    //isTouchBottom = isTouchBottom & cheack;
                    //result = 0;
                    //isStop = true;
                    player.SetDestination(HoldPossion());
                    //gameover();

                    break;
                case "left"://확장성때문에 안 쓸 수도 있음
                    cheack = true;
                    break;
                case "right":
                    cheack = true;
                    break;

            }
        }
    }
    private void OnTouchEvent()//터치를 통한 조작을 판단
    {

        if (Input.touchCount > 0)//터치가 될경우
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) != false)//Ui부분 터치판단
            {
                Debug.Log("UiTouch");
                uiTouched = true;
                return;
            }
            else//ui가아님
            {
                Touch touch = Input.GetTouch(0);//처음 터치된 정보
                //
                //grabTime += Time.deltaTime;

                if (touch.phase == TouchPhase.Began)
                {
                    //Debug.Log("Began1");
                    HandleTouchBegan(touch);//움직이면 안됨
                }
                else if (touch.phase == TouchPhase.Ended )
                {
                    //Debug.Log("End2");
                    HandleTouchEnd(touch);
                }
                else if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved )//홀딩이거나 드래그시, canTouch를 조건에 넣어야함
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
        isTouchBottom = false;
        gravityCoolTime = 0;
        startPos = Camera.main.ScreenToWorldPoint(touch.position);
        startPos.y -= CameraSet.cameraInstance.GetCurrentYpos() - CameraSet.cameraInstance.GetStartYpos();//카메라 Y좌표의 차를 계산해서 현재 위치를 빼 줌으로 계속 같은 화면을 터치 하도록 만듬
        //Debug.Log("시작 :" + startPos);
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
            //{
            touchStart = false;
            isTouchBottom = false;
            
            speed = 6f;//여기 까지 날리기 위한 셋팅이니까 수정
                       //rg2D.gravityScale = setGravityScale();
                       //만약 velocity이동이라면 100정도는 줘야함

            endPos = Camera.main.ScreenToWorldPoint(touch.position);
            endPos.y -= CameraSet.cameraInstance.GetCurrentYpos() - CameraSet.cameraInstance.GetStartYpos();
            posDistance = Vector2.Distance(startPos, endPos);
            //}
            collsitonCount = 0;
            if (posDistance < 0.2)//거리가 짧으며 reflectTouch가 false일때 가능
            {
                Debug.Log("단순 터치");
                
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
            else//드래그시      
            {
                if (canTouch)
                {
                    canTouch = false;
                    reflectTouch = false;//터치시 반사
                    destination = (endPos - startPos).normalized;//현재 코드는 화면 어디를 터치 하더라도 같은 이동 방향에 따라 움직임
                    destination = VectorCorrection(destination);
                    player.SetDestination(destination);
                    player.SetpreVec(destination);//이동 벡터 저장

                    rg2D.velocity = Vector2.zero;
                    rg2D.gravityScale = 0.0f;
                    //Debug.Log("x 벡터" + player.GetDestinaion().x);


                    Debug.Log(string.Format("드래그 부호 {0} 벽 충돌 부호{1}", Mathf.Sign(destination.x), Mathf.Sign(result)));

                    if (isStay && Mathf.Sign(destination.x) != Mathf.Sign(result))//벽이랑 같은 위치일때
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
