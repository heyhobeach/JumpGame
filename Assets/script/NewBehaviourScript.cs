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

    private Vector2 startPos;//터시 시작시 x pos
    private Vector2 endPos;//터치 시작시 y pos

    private Vector2 destination;//목적지

    public float speed = 3f;// private로 수정하고 speed= tapPower와 speed=dragPower로 수정 가능하지 않을까

    public float tapPower;
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

    public bool isStart = true;

    public bool touchStart = true;


    //private float touchPosx;

    private float posDistance;

    private float touchStartTime;
    private Vector2 preVelocity;

    private bool isWallHit = false;

    public int reflectCnt;

    public bool canTouch = true;//추후 private로 수정 필요

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
        transform.localScale = new Vector2(0.5f, 0.5f);// 해상도 비율에 맞게 크기 조절 근데 캔버스가 있음

        //touchPosx = transform.localPosition.x;
        screenYpos = CameraSet.limitPos;//screenYpos로 교체 하면 됨 해당 위치는 캐릭터의 위치가 항상 이쯤에 있을거임 이거보다 캐릭터가 더 위에있다면 카메라가 움직임

        //Debug.Log(string.Format("화면 1/3아래:{0} 화면 2/3위 :{1} 화면 꼭대기{2}", -screenYpos, screenYpos,CameraSet.Top.y));
        Debug.Log(CameraSet.Top.y);
    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        position = rg2D.velocity;// 이 부분의 필요는?
        OnTouchEvent();
        //transform.Translate(destination * speed * Time.deltaTime, Space.World);// 이 부분은 필요가 없는것같음
        /*if (touchStart && transform.position.y >= screenYpos)// 해당 부분 필요없는것 같아 주석처리
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
            //isWallHit = true;//두 변수가 중복 되는것같음
            canTouch = true;
        }

    }

    public float setGravityScale()//이 함수가 굳이 필요할까
    {
        return scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)//충돌시 발생
    {
        //Debug.Log("collision");
        Debug.Log(collision.gameObject.name);
        //Vector2 inDirection = GetComponent<Rigidbody2D>().velocity;
        destination = Vector2.Reflect(destination, collision.GetContact(0).normal).normalized;//충돌시 전반사로 벡터 방향 수정

        if (collision.gameObject.tag == "border")//좌 우 경계에 닿을경우
        {
            isWallHit = true;//벽에 충돌할경우 해당 라인이 있어야 만약 다시 터치 가능한 범위? 안에서 충돌일어나 다시 잡을경우를 위한것
            canTouch = true;
            speed /= 2;//벽에 충돌시 속도 감속
            switch (collision.gameObject.name)
            {
                case "top"://해당 부분은 아마 쓸 일은 없을거같긴함
                    //isTouchTop = true;
                    break;
                case "bottom":
                    isTouchBottom = true;
                    //isTouchLeft = false;
                    //isTouchRight = false;

                    break;
                case "left":
                    //rg2D.gravityScale = setGravityScale();//여기 부분에서 생기는 문제 중력이 생겨버려서
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
    private void OnTouchEvent()//이동 이벤트
    {

        if (Input.touchCount == 1)//Input.touchCount > 0
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
                //else if (touch.phase == TouchPhase.Moved)//두개 합칠수있지 않을까
                //{
                    //Debug.Log("Move");
                    //HandleTouchMove(touch);
                //}
                else if (touch.phase == TouchPhase.Stationary|| touch.phase == TouchPhase.Moved)//홀딩이거나 드래그시
                {
                    //Debug.Log("홀드 액션 함수");
                    if (grabTime > 1||!canTouch)
                    {
                        //rg2D.velocity = preVelocity;
                        Move();
                        return;
                    }
                }

                //}
                else//위 일때
                {

                    Move();
                    Debug.Log("destination" + destination);
                    //transform.Translate(destination * speed * Time.deltaTime, Space.World);//이동 부분, destination계산필요 날리는 부분 Move함수로 교체
                    //Debug.Log("움직이는중 터치 발생");
                    //return;
                }


            }
        }
        else//터치가 안 되었을때
        {
            grabTime = 0;//터치하고 있지않을때 0으로 만들어야함
            //grabCoolTime += Time.deltaTime;//쿨타임 더하기
            if (!isTouchBottom)//바닥에 닿지 않았을때 계산해야지 밑으로 내려가려는 힘이 없음
            {

                Move();
            }
            else//바닥이 터치 되었을때
            {
                transform.position = transform.position;
            }
        }

    }
    private void Move()//destination값을 전달 해준다면?
    {

        grabCoolTime += Time.deltaTime;//쿨타임 더하기
        transform.Translate(destination * speed * Time.deltaTime);//이동 부분, destination계산필요 날리는 부분
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
        Debug.Log("시작 :" + startPos);
    }

    private void Shoot()
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
        destination = (endPos - startPos).normalized;//여기를 지우면 그냥 떨어짐
        Debug.Log("shoot" + destination);
        //startPos = Vector2.zero;
        //endPos = Vector2.zero;
        rg2D.gravityScale = setGravityScale();
        return;

    }

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
            //touchTime = Time.time - touchStartTime;
            Debug.Log(string.Format("touchTime :{0}", grabTime));
            canTouch = false;
            //isWallHit = false;//드래그가 끝나는 시점에서 false로 전환해 잡지 못하게 하기위함
            touchStart = false;
            isTouchBottom = false;
            isStart = false;
            //grabCoolTime = 0;//쿨타임
            //canTouch = true;
            speed = 3f;//여기 까지 날리기 위한 셋팅이니까 수정
                       //rg2D.gravityScale = setGravityScale();

            endPos = Camera.main.ScreenToWorldPoint(touch.position);
            Debug.Log("endoPos :" + endPos);
            //new Vector2(transform.position.x,transform.position.y) 이걸 넣으면 도형으로 부터 마지막 손가락 위치
            posDistance = Vector2.Distance(startPos, endPos);
            //Debug.Log(string.Format("시작 위치 :{0}, 종료 위치 :{1}, 거리 = {2} ", startPos, endPos, posDistance));
            //}
            if (posDistance < 0.2)//단순 터치 터치를 거리로 판단 
            {
                Debug.Log("단순 터치");
                Shoot();

                /*rg2D.gravityScale = setGravityScale(); //Shoot함수로 이동
                touchStart = true;//단순 터치기에 
                startPos = transform.position;
                endPos = new Vector2(startPos.x, -screenYpos);
                speed = 5;
                destination=(endPos-startPos).normalized;*/

            }
            else
            {
                destination = (endPos - startPos).normalized;//현재 코드는 화면 어디를 터치 하더라도 같은 이동 방향에 따라 움직임
                destination=VectorCorrection(destination);
                Debug.Log("드래그 벡터"+destination);
            }
            isDrag = false;
            grabCoolTime = 0;//쿨타임
        }

    }

    private void HandleTouchMove(Touch touch)
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
    }

    private void HoldPossion()//위치 위치 고정시키는 함수
    {
        rg2D.velocity = Vector2.zero;
        rg2D.gravityScale = 0.0f;

        transform.Translate(Vector2.zero);//위치 고정
        transform.position = transform.position;
        return;
    }

    private Vector2 VectorCorrection(Vector2 pos)
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
        Vector2 vector_correction = new Vector2(pos.x,correctino_posy);
        return vector_correction;
    }


}
