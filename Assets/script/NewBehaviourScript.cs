using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 position;
    private Rigidbody2D rg2D;

    private Vector2 startPos;//터시 시작시 x pos
    private Vector2 endPos;//터치 시작시 y pos

    private Vector2 destination;

    public float speed = 3f;// private로 수정하고 speed= tapPower와 speed=dragPower로 수정 가능하지 않을까

    public float tapPower;
    public float dragPower;

    public float scale = 0.5f;

    private float grabTime;//그랩 지속시간
    private float grabCoolTime;//그랩 쿨타임 체크

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

    public bool canTouch=true;//추후 private로 수정 필요

    void Start()
    {
        //destination = new Vector2(-1.4f, 4);
        transform.position = new Vector2(0, -4.5f);
        position = transform.position;

        rg2D = GetComponent<Rigidbody2D>();
        rg2D.gravityScale = 0.2f;
        transform.localScale = new Vector2(0.5f, 0.5f);// 해상도 비율에 맞게 크기 조절 근데 캔버스가 있음

        touchPosx = transform.localPosition.x;
        screenYpos = CameraSet.limitPos;//screenYpos로 교체 하면 됨 해당 위치는 캐릭터의 위치가 항상 이쯤에 있을거임 이거보다 캐릭터가 더 위에있다면 카메라가 움직임
    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        position = rg2D.velocity;// 이 부분의 필요는?
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
    private void OnCollisionEnter2D(Collision2D collision)//충돌시 발생
    {
        //Debug.Log("collision");
        Debug.Log(collision.gameObject.name);
        //Vector2 inDirection = GetComponent<Rigidbody2D>().velocity;
        destination = Vector2.Reflect(destination, collision.GetContact(0).normal);

        if (collision.gameObject.tag == "border")//좌 우 경계에 닿을경우
        {
            isWallHit = true;//벽에 충돌할경우 해당 라인이 있어야 만약 다시 터치 가능한 범위? 안에서 충돌일어나 다시 잡을경우를 위한것
            canTouch = true;
            speed /= 2;//벽에 충돌시 속도 감속
            switch (collision.gameObject.name)
            {
                case "top"://해당 부분은 아마 쓸 일은 없을거같긴함
                    isTouchTop = true;
                    break;
                case "bottom":
                    isTouchBottom = true;
                    isTouchLeft = false;
                    isTouchRight = false;
                    
                    break;
                case "left":
                    rg2D.gravityScale = setGravityScale();//여기 부분에서 생기는 문제 중력이 생겨버려서
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

    private void OnTriggerExit(Collider other)//지금 문제 if 에 안 들어가짐
    {
        //Debug.Log("Exit");
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "border")//좌 우 경계에 닿을경우
        {
            Debug.Log("떨어짐");
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

    private void OnTouchEvent()//이동 이벤트
    {

        if (canTouch&&Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);//처음 터치된 정보
            Vector2 screenpos = Camera.main.ScreenToWorldPoint(touch.position);
            
            if (CameraSet.limitPos >= transform.position.y && isWallHit == false)
            {
                Move();
                //canTouch = true;
            }
            else if (CameraSet.limitPos < transform.position.y)
            {
                isWallHit = true; //충돌없이 범위 밖을 나갔을때 못 잡게 하기위함
                Move();
            }
            
            else if (CameraSet.limitPos >= transform.position.y)//여기에 grabTime<=1 조건을 and 로 넣고 1은 제한시간
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
                    if (!touchStart)//오브젝트가 들어오기전 미리 홀드 하는경우 
                    {
                        Debug.Log("그냥 계속 터치중");
                        Move(); 
                    }
                    else
                    {
                        reflectCnt = 0;//반사 횟수 
                        Debug.Log("hold :"+grabTime);
                        if (grabTime > 1)//홀드 오래 했을경우 이때만 false
                        {
                            Shoot();
                            canTouch = false;
                            rg2D.velocity = Vector2.zero;
                            Debug.Log("1초 지나서 슈팅");
                        }
                    }
                }

            }
            else//위 일때
            {

                Move();
                //transform.Translate(destination * speed * Time.deltaTime, Space.World);//이동 부분, destination계산필요 날리는 부분 Move함수로 교체
                Debug.Log("범위 밖");
            }


        }
        else//터치가 안 되었을때
        {
            grabTime = 0;//터치하고 있지않을때 0으로 만들어야함
            grabCoolTime += Time.deltaTime;//쿨타임 더하기
            if (!isTouchBottom)//바닥에 닿지 않았을때 계산해야지 밑으로 내려가려는 힘이 없음
            {

                Move();
                //transform.Translate(destination * speed * Time.deltaTime, Space.World);//이동 부분, destination계산필요 날리는 부분
                //Debug.Log("move");
            }
            else//바닥이 터치 되었을때
            {
                transform.position = transform.position;
            }
        }
    }

    /*private void HoldTouch(Touch touch)//???? 이거 왜 만들었지
    {
        HoldPossion();
    }*/

    private void Move()
    {
        transform.Translate(destination * speed * Time.deltaTime, Space.World);//이동 부분, destination계산필요 날리는 부분
    }

    private void HandleTouchBegan(Touch touch)
    {
        touchStart = true;
        touchStartTime = Time.time;
        if (isWallHit)//충돌 상태 = 감속이 이루어 졌음
        {
            speed *= 2;
        }
        HoldPossion();
        //isTouchLeft = false;
        //isTouchRight = false;

        rg2D.gravityScale = 0.0f;//드래그 동안 중력을 없애야지 덜덜 떨리는것이 없음
        isTouchBottom = false;
        //HoldPossion();
        startPos = Camera.main.ScreenToWorldPoint(touch.position);
        Debug.Log("시작 :" + startPos);
    }

    private void Shoot()
    {
        
        touchStart = true;//단순 터치기에 
        startPos = transform.position;
        endPos = new Vector2(startPos.x, -screenYpos);
        speed = 5;
        destination = (endPos - startPos).normalized;//여기를 지우면 그냥 떨어짐
        rg2D.gravityScale = setGravityScale();
        
    }

    private void HandleTouchEnd(Touch touch)
    {
        touchTime = Time.time - touchStartTime;
        Debug.Log(string.Format("touchTime :{0}", touchTime));
        isWallHit = false;//드래그가 끝나는 시점에서 false로 전환해 잡지 못하게 하기위함
        touchStart = false;
        isTouchBottom = false;
        isStrat = false;
        grabCoolTime = 0;//쿨타임
        canTouch = true;
        speed = 3f;//여기 까지 날리기 위한 셋팅이니까 수정
        //rg2D.gravityScale = setGravityScale();

        endPos = Camera.main.ScreenToWorldPoint(touch.position);
        destination = (endPos - startPos).normalized;//현재 코드는 화면 어디를 터치 하더라도 같은 이동 방향에 따라 움직임
                                                     //new Vector2(transform.position.x,transform.position.y) 이걸 넣으면 도형으로 부터 마지막 손가락 위치
        posDistance = Vector2.Distance(startPos, endPos);
        Debug.Log(string.Format("시작 위치 :{0}, 종료 위치 :{1}, 거리 = {2} ", startPos, endPos, posDistance));
        if (posDistance < 0.5)//단순 터치 터치를 거리로 판단 
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
    }

    private void HandleTouchMove(Touch touch)
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);//이렇게 해야지 기기마다 화면의 좌표로 설정된다.


        if (isStrat)//처음 드래그 부분
        {
            Debug.Log("MoveToward pos =>" + pos);
            transform.position = new Vector2(pos.x, -4.4f);
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
    }
}
