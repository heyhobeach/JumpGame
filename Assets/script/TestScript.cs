using System;
using System.Collections;

//using TreeEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using static ObjectManager;

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

    private SpriteRenderer spriteRenderer;
    private Animator anim;



    private Vector2 startPos;//터시 시작시 x pos
    private Vector2 endPos;//터치 시작시 y pos

    private Vector2 destination;//목적지

    public float speed = 8f;// private로 수정하고 speed= tapPower와 speed=dragPower로 수정 가능하지 않을까

    public const byte maxCollsion = 3;
    private byte collsitonCount = 0;//�浹 Ƚ�� count���� �巡�׽� �ʱ�ȭ
    public float dragPower;

    public float scale = 1f;//�߷�

    public bool isTouchBottom = false;


    public bool reflectTouch = false;//��ġ�� �ݻ� �ϴ� ����

    public bool isGravity = true;//touchStart -> isGravity로 변수명 수정중력 제어를 위한 변수 1초간 충돌 혹은 드래그가 없을경우 중력을 적용시키기 위함, 변수명 수정 필요, 중력을 제외하면 조작에 영향을 주지 않음

    public bool isStay = false;


    float result = 0;



    private float dragCoolTime=0;//쿨타임 관련 변수
    private float gravityCoolTime=0;

    public bool isWallHit = false;//테스트후 private로 수정 필요

    public int reflectCnt;//�浹 ī��Ʈ

    public bool canTouch = false;//추후 private로 수정 필요 충돌과 cooltime계산후 터치 가능한 조건을 만듬

    public bool uiTouched = false;//UI 터치 했는지 판단하는 부분

    public bool canTap = true;//구르기를 한번만 할 수 있도록 벽에 붙으면 true로 초기화

    public bool IsTap = false;//구르기와 일반 점프를 구분하기 위해 넣은 변수

    public bool side = false;
    int collnum = 0;//테스트용 변수


    private IEnumerator cooltimeCoroutine;
    private IEnumerator gravityCoroutine;
    private IEnumerator slidingCorutine;


    void Start()
    {
        //destination = new Vector2(-1.4f, 4);;
        
        position = transform.position;
        //Debug.Log(CameraSet.cameraInstance.GetStartYpos());

        spriteRenderer=GetComponent<SpriteRenderer>();//SpriteRenderer 컴포넌트 가져옴
        anim = GetComponent<Animator>();//animator 컴포넌트 가져옴

        rg2D = GetComponent<Rigidbody2D>();
        rg2D.gravityScale = 0.2f;
        //transform.localScale = new Vector2(0.5f, 0.5f);// 해상도 비율에 맞게 크기 조절 근데 캔버스가 있음
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
        //rg2D.velocity = Vector2.left;
        Move2(player.GetDestinaion());
    }
    // Update is called once per frame
    void Update()
    {
        OnTouchEvent();
        
        if (isWallHit)
        {
            collnum++;
            Debug.Log(string.Format("충돌 {0} 회", collnum));
            isWallHit = false; 
        }

    }

    public float setGravity()
    {
        return scale;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isStay= true;//가만히 서있을때
        if (collision.gameObject.name=="left"||collision.gameObject.name == "right")
        {
            side = true;
           //StartCoroutine(Sliding(transform.position));//충돌 위치 
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Exit");
        isTouchBottom = false;
        //side = false;
        isWallHit = false;
        isStay = false;
        isGravity = false;//update에서 touchStart= true부분에 if else로 제어 할 수는 있음
    }
    private void OnCollisionEnter2D(Collision2D collision)//충돌시 발생
    {
        //Debug.Log(collsitonCount);
        IsTap = false;
        if (collision.gameObject.name != "Ground") 
        {
            result = collision.GetContact(0).point.x - collision.transform.position.x;
            anim.ResetTrigger("StandTrigger");//트리거가 남아있던 현상 풀기위함

        }

        Debug.Log(collision.gameObject.name);


        if (collision.gameObject.GetComponentInChildren<ObjectManager>() != null)
        {
            Debug.Log("오브젝트 매니저가 있음");
            switch (collision.gameObject.GetComponent<ObjectManager>().type)
            {
                case WallType.DEAD_WALL:
                    Debug.Log("스위치 데드월");
                    GameManager.Instance.Dead();
                    break;
                default:
                    Debug.Log("디폴트");
                    break;
            }
        }



        if (collision.gameObject.tag == "border")
        {
            Debug.Log(collision.gameObject.name);
            isWallHit = true;
            destination = Vector2.Reflect(destination, collision.GetContact(0).normal).normalized;//충돌시 전반사로 벡터 방향 수정
            //destination.x *= -1;
            if (collsitonCount < maxCollsion -1&&collision.gameObject.name!= "Ground")//�浹Ƚ��
            {
                collsitonCount++;
                Debug.Log("카운트 증가");
            }else if(collsitonCount == maxCollsion - 2 && collision.gameObject.name != "Ground")
            {
                rg2D.gravityScale = scale;
            }
            else if(collsitonCount == maxCollsion - 1 && collision.gameObject.name != "Ground")
            {
                //side = true;//벽에 붙은걸 확인하기 위해
                StartCoroutine(Sliding(transform.position));//충돌 위치와 도착 지점을 보냄
                StickWall();
                //rg2D.gravityScale = scale;//중복되는 부분 빼도될듯해보임
                return;
            }

            isWallHit = true;//벽에 충돌할경우 해당 라인이 있어야 만약 다시 터치 가능한 범위? 안에서 충돌일어나 다시 잡을경우를 위한것
            //destination = Vector2.Reflect(destination, collision.GetContact(0).normal).normalized;//충돌시 전반사로 벡터 방향 수정
            player.SetDestination(destination);
            
            isGravity = true;//false->true
            canTouch = true;
            dragCoolTime = 0;
            gravityCoolTime = 0;
            Debug.Log("중력 시간 :" + gravityCoolTime);
            //speed /= 2;//���� �浹�� �ӵ� ����
            switch (collision.gameObject.name)//이름으로 접근
            {
                case "top"://해당 부분은 아마 쓸 일은 없을거같긴함
                    //isTouchTop = true;
                    break;
                case "Ground":
                    collsitonCount = 0;
                    isTouchBottom = true;
                    player.SetDestination(HoldPossion());
                    anim.SetBool("IsJunp", false);//바닥에 닿으면 일반 상태로 돌리기 위함
                    anim.SetTrigger("StandTrigger");
                    //GameManager.Instance.Dead();
                    //result = 0;
                    //isStop = true;
                    
                    //gameover();

                    break;
                case "left":
                    //side = true;
                    break;
                case "right":
                    //side = true;
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
                //if(!canTouch) break;
                if (canTap&&!isStay)
                {
                    destination.x *= -1;
                    player.SetDestination(destination);
                    reflectTouch = true;
                    IsTap = true;
                    canTap = false;
                }


                //Debug.Log("터치");

                //canTouch = false;
                //touchStart = false;//해당부분 수정이 필요할듯,구르기도 중력 시간 이 필요할까 싶어서 그리고 중복되는것같음
            break;
            case TempPanel.InputState.Drag:
                if(!canTouch) break;
                Debug.Log("드래그");
                IsTap = false;
                canTap = true;
                collsitonCount = 0;
                destination = TempPanel.Instance.dir;//현재 코드는 화면 어디를 터치 하더라도 같은 이동 방향에 따라 움직임
                destination = VectorCorrection(destination);
                Debug.Log(destination);
                player.SetDestination(destination);
                player.SetpreVec(destination);//이동 벡터 저장

                rg2D.velocity = Vector2.zero;
                rg2D.gravityScale = 0.0f;

                //Debug.Log(string.Format("side : {0} , {1} : {2}", side, Mathf.Sign(destination.x), Mathf.Sign(result)));
            if (side && Mathf.Sign(destination.x) != Mathf.Sign(result))//���̶� ���� ��ġ�϶�
            {
                //Debug.Log(player.GetDestinaion());
                destination.x *= -1;
                //player.SetDestination(new Vector2(1, 1));
                player.SetDestination(destination);
                collsitonCount++;
            }

            isGravity = false;
            canTouch = false;
            side = false;
            break;
            case TempPanel.InputState.Hold:

            break;
            default:
            
            break;
        }


    }

    public void Move2(Vector2 destination)
    {
        if (destination.normalized.x < 0)//좌측으로 이동할때, 함수로 묶을까함
        {
            spriteRenderer.flipX = true;
        }
        else if(destination.normalized.x > 0)
        {
          spriteRenderer.flipX = false;
        }
        if(destination!=new Vector2(0,0))
        { 
            anim.SetBool("IsJump", true);//움직이는 애니메이션 넣는것
            if (IsTap)
            {
                anim.SetBool("IsReflect", true);
            }
            else
            {
                anim.SetBool("IsReflect", false);
            }
        }
        else
        {
            anim.SetBool("IsReflect", false);//스탠드 상태로 돌리기 위함
            anim.SetBool("IsJump", false);
        }
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
                else//사실상 의미 없음
                {
                    dragCoolTime = 0;
                }

            }
            yield return null;
        }

        
    }


    IEnumerator GravityTime()
    {
        while (true)
        {//시간 측정하고 해당 시간이 지났을때 이 조건이라면,
            if (!isGravity)
            {
                gravityCoolTime+= Time.deltaTime;
                if (gravityCoolTime >1)//충돌 안 했다면 
                {
                    //Debug.Log("")
                    rg2D.gravityScale = scale;//중력설정
                    isGravity= true;
                    Debug.Log("충돌 없는 중력" + gravityCoolTime);
                    gravityCoolTime = 0;
                    canTouch = true;
                }
            }
            else
            {
                Debug.Log("else");
                gravityCoolTime = 0;
            }



            yield return null;

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
        //float correctino_posx = 0.0f;
        if (pos.y <= 0)//0보다 작거나 같으면 보정
        {
            correctino_posy = 0.8f;
        }//else if (pos.y <= 0.2f)//혹시나 너무 수평에 가깝게 드래그 했을경우
        //{
        //    pos.y = pos.y * 2;
        //}
        else
        {
            correctino_posy = pos.y;
        }
        Vector2 vector_correction = new Vector2(pos.x, correctino_posy);
        return vector_correction;
    }

    IEnumerator Sliding(Vector2 collisonPos)//얼음벽 생각해서 함수 따로 빼는것이 좋다고 생각했음
    {
        Vector2 endpos;
        float delta = 0;//deltatime을 계속 더해줄 변수
        float duration =2f;//몇 초 안에 가느냐 를 정하는 함수
        side = true;
        //float t = 0.5f;
        bool groundIn = CameraSet.cameraInstance.CheackObjectInCamera(GameObject.Find("Ground"));//해당 부분 수정 필요
        if (groundIn)
        {
            Debug.Log("아직 땅 있음");
            endpos = new Vector2(collisonPos.x, -4.42f);//도착 지점 설정 -4.4 가 ground 있는곳
        }
        else
        {
            
            endpos = new Vector2(collisonPos.x, CameraSet.cameraInstance.Top.y-10f+0.5f);//도착 지점 설정 카메라 10은 카메라 크기 0.5는 캐릭터 크기
            Debug.Log("end pos :" + endpos);
        }
       
        Debug.Log("시작 지점 :" + collisonPos + "끝나는 지점 :" + endpos);

        while (delta<=duration)
        {
            Debug.Log("체크");
            if (!side) {//충돌하자마자 중지
                Debug.Log("중지");
                break; 
            }
            float t = delta / duration;
            //원하는 보간 수식
            t= (t == 0 ? 0 : Mathf.Pow(2, 10 * t - 10));
            Debug.Log("보간");
            transform.position = Vector2.Lerp(collisonPos, endpos, t);
            delta += Time.deltaTime;
            yield return null;
        }
        Debug.Log(endpos);
        //transform.position = new Vector2(transform.position.x,endpos.y);
        
    }

    private void StickWall()//벽에 붙어서 미끄러지는 함수
    {
        float time = Time.time;
        StickWallAnim();
        //player.SetDestination(HoldPossion());
        rg2D.gravityScale = 0;

        //if (time > 0.2)
        //{
        //    rg2D.gravityScale = 1f;
        //}
        //else
        //{
        //    rg2D.velocity = Vector2.zero;
        //    rg2D.gravityScale = 0.0f;
        //}



    }

    private void StickWallAnim()
    {
        anim.SetTrigger("IsStick");
        anim.SetBool("IsReflect", false);
        
    }


}
