using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

//using TreeEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Player
{
<<<<<<< HEAD
    private Vector2 destination;
    private float speed = 0.0f;
    private Vector2 preVec= Vector2.zero;

    public void SetDestination(Vector2 destination)//드래그 이후 벡터 계산이후 Set으로 설정함
=======
    private Vector2 destination = Vector2.zero;
    private float speed = 0.0f;
    Transform transform;
    Rigidbody2D rg2D;

    public void Move(Vector2 destination)
    {
        rg2D.velocity = destination.normalized * speed * Time.deltaTime;//rigidbody瑜� �씠�슜�븳 �씠�룞
        //transform.Translate(destination*speed*Time.deltaTime);
    }

    public void SetDestination(Vector2 destination)
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
    {
        this.destination = destination;
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
<<<<<<< HEAD

    public void SetpreVec(Vector2 preVec)//드래그 이후 벡터 계산이후 Set으로 설정함
    {
        this.preVec = preVec;
=======
    private void HoldPossion()//�쐞移� �쐞移� 怨좎젙�떆�궎�뒗 �븿�닔
    {
        rg2D.velocity = Vector2.zero;
        rg2D.gravityScale = 0.0f;

        //transform.Translate(Vector2.zero);//�쐞移� 怨좎젙
        transform.position = transform.position;
        return;
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
    }
    public Vector2 GetDestinaion()
    {
        return destination;
    }
<<<<<<< HEAD
    public Vector2 GetPreVec()
    {
        return preVec;
    }


=======
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
    public float GetSpeed() 
    {
        return speed;
    }
}

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
<<<<<<< HEAD
    Player player = new Player();//player객체 생성
    private Vector2 position;
    private Rigidbody2D rg2D;
    

    
    private Vector2 startPos;//터시 시작시 x pos
    private Vector2 endPos;//터치 시작시 y pos

    private Vector2 destination;//목적지

    public float speed = 3f;// private로 수정하고 speed= tapPower와 speed=dragPower로 수정 가능하지 않을까

    public const byte maxCollsion=2;
    public byte collsitonCount = 0;//충돌 횟수 count변수 드래그시 초기화
    public float dragPower;

    public float scale = 0.5f;//중력

    private float grabTime;//그랩 지속시간
    private float grabCoolTime;//그랩 쿨타임 체크

    //public bool isTouchTop = false;//isTouchBottom을 제외한 나머지 변수들은 안 쓰고있음
=======
    Player player = new Player();//player媛앹껜 �깮�꽦
    private Vector2 position;
    private Rigidbody2D rg2D;
    
    private Vector2 startPos;//�꽣�떆 �떆�옉�떆 x pos
    private Vector2 endPos;//�꽣移� �떆�옉�떆 y pos

    private Vector2 destination;//紐⑹쟻吏�

    public float speed = 3f;// private濡� �닔�젙�븯怨� speed= tapPower��� speed=dragPower濡� �닔�젙 媛��뒫�븯吏� �븡�쓣源�

    public float tapPower;
    public float dragPower;

    public float scale = 0.5f;//以묐젰

    private float grabTime;//洹몃옪 吏��냽�떆媛�
    private float grabCoolTime;//洹몃옪 荑⑦���엫 泥댄겕

    //public bool isTouchTop = false;//isTouchBottom�쓣 �젣�쇅�븳 �굹癒몄�� 蹂��닔�뱾��� �븞 �벐怨좎엳�쓬
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
    public bool isTouchBottom = false;
    //public bool isTouchLeft = false;
    //public bool isTouchRight = false;

    public float controllRatio = 33;
    private float screenYpos;

    public bool isStart = true;

<<<<<<< HEAD
    public bool touchStart = true;//손을 떼고 조작하는건지 아닌지 판단

    private float touchTime;
=======
    public bool touchStart = true;
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538


    //private float touchPosx;

    private float posDistance;

    private float touchStartTime;
    private Vector2 preVelocity;

<<<<<<< HEAD
    public bool isWallHit = false;//테스트후 private로 수정 필요

    public int reflectCnt;

    public bool canTouch = true;//추후 private로 수정 필요 충돌과 cooltime계산후 터치 가능한 조건을 만듬

    public bool uiTouched = false;//UI 터치 했는지 판단하는 부분
=======
    private bool isWallHit = false;

    public int reflectCnt;

    public bool canTouch = true;//異뷀썑 private濡� �닔�젙 �븘�슂

    public bool uiTouched = false;
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538

    public bool isDrag = false;

    public bool isStop = true;

<<<<<<< HEAD
    private IEnumerator cooltimeCoroutine;
=======
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538



    void Start()
    {
<<<<<<< HEAD
        //destination = new Vector2(-1.4f, 4);;
=======
        //destination = new Vector2(-1.4f, 4);
       
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
        transform.position = new Vector2(0, -4.5f);
        position = transform.position;

        rg2D = GetComponent<Rigidbody2D>();
        rg2D.gravityScale = 0.2f;
<<<<<<< HEAD
        transform.localScale = new Vector2(0.5f, 0.5f);// 해상도 비율에 맞게 크기 조절 근데 캔버스가 있음

        //touchPosx = transform.localPosition.x;
        //screenYpos = CameraSet.limitPos;//screenYpos로 교체 하면 됨 해당 위치는 캐릭터의 위치가 항상 이쯤에 있을거임 이거보다 캐릭터가 더 위에있다면 카메라가 움직임
        cooltimeCoroutine = DragCooltime();


        //Debug.Log(string.Format("화면 1/3아래:{0} 화면 2/3위 :{1} 화면 꼭대기{2}", -screenYpos, screenYpos,CameraSet.Top.y));
=======
        transform.localScale = new Vector2(0.5f, 0.5f);// �빐�긽�룄 鍮꾩쑉�뿉 留욊쾶 �겕湲� 議곗젅 洹쇰뜲 罹붾쾭�뒪媛� �엳�쓬

        //touchPosx = transform.localPosition.x;
        screenYpos = CameraSet.limitPos;//screenYpos濡� 援먯껜 �븯硫� �맖 �빐�떦 �쐞移섎뒗 罹먮┃�꽣�쓽 �쐞移섍�� �빆�긽 �씠易ㅼ뿉 �엳�쓣嫄곗엫 �씠嫄곕낫�떎 罹먮┃�꽣媛� �뜑 �쐞�뿉�엳�떎硫� 移대찓�씪媛� ���吏곸엫

        //Debug.Log(string.Format("�솕硫� 1/3�븘�옒:{0} �솕硫� 2/3�쐞 :{1} �솕硫� 瑗����湲�{2}", -screenYpos, screenYpos,CameraSet.Top.y));
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
        Debug.Log(CameraSet.Top.y);
    }

    private void Awake()
    {
<<<<<<< HEAD
        
=======

>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
    }


    private void FixedUpdate()
    {
<<<<<<< HEAD
        //Debug.Log(player.GetSpeed());
        Move2(player.GetDestinaion());
        //Debug.Log(player.GetDestinaion());
=======
        Debug.Log(player.GetSpeed());
        if (isStop)
        {
            //Debug.Log("Stop");

            HoldPossion();
        }
        else
        {
            Move();

        }
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538


    }
    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        //player.SetDestination(player.GetDestinaion());
        if (!canTouch)//터치 못 할때 코루틴 호출
        {
            StartCoroutine(cooltimeCoroutine);
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

    public float setGravityScale()//이 함수가 굳이 필요할까
=======
        //player.SetSpeed(speed++);
        position = rg2D.velocity;// �씠 遺�遺꾩쓽 �븘�슂�뒗?
        //Debug.Log("Update");
        //OnTouchEvent();
        //transform.Translate(destination * speed * Time.deltaTime, Space.World);// �씠 遺�遺꾩�� �븘�슂媛� �뾾�뒗寃껉컳�쓬
        /*if (touchStart && transform.position.y >= screenYpos)// �빐�떦 遺�遺� �븘�슂�뾾�뒗寃� 媛숈븘 二쇱꽍泥섎━
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
            //isWallHit = true;//�몢 蹂��닔媛� 以묐났 �릺�뒗寃껉컳�쓬
            canTouch = true;
        }

    }

    public float setGravityScale()//�씠 �븿�닔媛� 援녹씠 �븘�슂�븷源�
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
    {
        return scale;
    }

<<<<<<< HEAD
    private void OnCollisionEnter2D(Collision2D collision)//충돌시 발생
    {
        //Debug.Log("collision");
        Debug.Log(collision.gameObject.name);
        //Vector2 inDirection = GetComponent<Rigidbody2D>().velocity;
        //destination = Vector2.Reflect(destination, collision.GetContact(0).normal).normalized;//충돌시 전반사로 벡터 방향 수정
        //player.SetDestination(destination);
        //player.SetDestination(Vector2.zero);
        //StickWall();

        if (collision.gameObject.tag == "border")//좌 우 경계에 닿을경우
        {
            if (collsitonCount < maxCollsion-1)
            {
                collsitonCount++;
            }
            else
            {
                player.SetDestination(HoldPossion());
                Debug.Log("충돌 끝");
                return;
            }
            
            isWallHit = true ;//벽에 충돌할경우 해당 라인이 있어야 만약 다시 터치 가능한 범위? 안에서 충돌일어나 다시 잡을경우를 위한것
            destination = Vector2.Reflect(destination, collision.GetContact(0).normal).normalized;//충돌시 전반사로 벡터 방향 수정
            player.SetDestination(destination);
            canTouch = true;
            StopCoroutine(cooltimeCoroutine);
            //speed /= 2;//벽에 충돌시 속도 감속
            switch (collision.gameObject.name)
            {
                case "top"://해당 부분은 아마 쓸 일은 없을거같긴함
=======
    private void OnCollisionEnter2D(Collision2D collision)//異⑸룎�떆 諛쒖깮
    {
        Debug.Log("collision");
        Debug.Log(collision.gameObject.name);
        //Vector2 inDirection = GetComponent<Rigidbody2D>().velocity;
        destination = Vector2.Reflect(destination, collision.GetContact(0).normal).normalized;//異⑸룎�떆 �쟾諛섏궗濡� 踰≫꽣 諛⑺뼢 �닔�젙

        if (collision.gameObject.tag == "border")//醫� �슦 寃쎄퀎�뿉 �떯�쓣寃쎌슦
        {
            isWallHit = true;//踰쎌뿉 異⑸룎�븷寃쎌슦 �빐�떦 �씪�씤�씠 �엳�뼱�빞 留뚯빟 �떎�떆 �꽣移� 媛��뒫�븳 踰붿쐞? �븞�뿉�꽌 異⑸룎�씪�뼱�굹 �떎�떆 �옟�쓣寃쎌슦瑜� �쐞�븳寃�
            canTouch = true;
            speed /= 2;//踰쎌뿉 異⑸룎�떆 �냽�룄 媛먯냽
            switch (collision.gameObject.name)
            {
                case "top"://�빐�떦 遺�遺꾩�� �븘留� �벝 �씪��� �뾾�쓣嫄곌컳湲댄븿
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
                    //isTouchTop = true;
                    break;
                case "bottom":
                    isTouchBottom = true;
                    isStop = true;
<<<<<<< HEAD
                    player.SetDestination(HoldPossion());

                    break;
                case "left":
                    //rg2D.gravityScale = setGravityScale();//여기 부분에서 생기는 문제 중력이 생겨버려서
                    rg2D.gravityScale = scale;
=======
                    //isTouchLeft = false;
                    //isTouchRight = false;

                    break;
                case "left":
                    //rg2D.gravityScale = setGravityScale();//�뿬湲� 遺�遺꾩뿉�꽌 �깮湲곕뒗 臾몄젣 以묐젰�씠 �깮寃⑤쾭�젮�꽌
                    rg2D.gravityScale = scale;
                    //isTouchLeft = true;
                    //isTouchRight = false;
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538

                    break;
                case "right":
                    //rg2D.gravityScale = setGravityScale();
                    rg2D.gravityScale = scale;
<<<<<<< HEAD
=======
                    //isTouchRight = true;
                    //isTouchLeft = false;
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
                    break;

            }
        }
    }
<<<<<<< HEAD
    private void OnTouchEvent()//터치를 통한 조작을 판단
    {

        if (Input.touchCount > 0)//터치가 될경우
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) != false)//Ui부분 터치판단
=======
    private void OnTouchEvent()//�씠�룞 �씠踰ㅽ듃
    {

        if (Input.touchCount == 1)//Input.touchCount > 0
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) != false)//Ui遺�遺� �꽣移섑뙋�떒
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
            {
                Debug.Log("UiTouch");
                uiTouched = true;
                return;
            }
<<<<<<< HEAD
            else//ui가아님
            { 
                Touch touch = Input.GetTouch(0);//처음 터치된 정보
                grabTime += Time.deltaTime;

                if (touch.phase == TouchPhase.Began && canTouch)
                {
                    Debug.Log("Began1");
                    HandleTouchBegan(touch);//움직이면 안됨
                }
                else if (touch.phase == TouchPhase.Ended && canTouch)
                {
                    Debug.Log("End2");
                    HandleTouchEnd(touch);
                }
                else if (touch.phase == TouchPhase.Stationary && canTouch || touch.phase == TouchPhase.Moved && canTouch)//홀딩이거나 드래그시, canTouch를 조건에 넣어야함
                {

=======
            else//ui媛��븘�떂
            {
                Touch touch = Input.GetTouch(0);//泥섏쓬 �꽣移섎맂 �젙蹂�
                grabTime += Time.deltaTime;

                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("Began1");
                    endPos = Camera.main.ScreenToWorldPoint(touch.position);
                    if (!canTouch)//HandleTouchBeagan�븞�뿉 議곌굔�씠�엳�쓬
                    {
                        isStop = false;//���吏곸뿬�빞�븿
                    }
                    else
                    {
                        HandleTouchBegan(touch);//���吏곸씠硫� �븞�맖

                    }
                }
                else if (touch.phase == TouchPhase.Ended && canTouch)
                {
                    isStop = false;//���吏곸뿬�빞�븿
                    Debug.Log("End2");
                    HandleTouchEnd(touch);
                }
                //else if (touch.phase == TouchPhase.Moved)//�몢媛� �빀移좎닔�엳吏� �븡�쓣源�
                //{
                //Debug.Log("Move");
                //HandleTouchMove(touch);
                //}
                else if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)//����뵫�씠嫄곕굹 �뱶�옒洹몄떆
                {
                    //Debug.Log("����뱶 �븸�뀡 �븿�닔");
                    if (grabTime > 1 || !canTouch)
                    {
                        //rg2D.velocity = preVelocity;
                        isStop = false;
                        //Move();
                        return;
                    }
                    //�굹癒몄���뒗 ���吏곸씠硫� �븞 �맆嫄곌컳����뜲
                    //else{isStop=true};

                }

                //}
                else//�쐞 �씪�븣
                {

                    //Move();
                    isStop = false;
                    Debug.Log("destination" + destination);
                    //transform.Translate(destination * speed * Time.deltaTime, Space.World);//�씠�룞 遺�遺�, destination怨꾩궛�븘�슂 �궇由щ뒗 遺�遺� Move�븿�닔濡� 援먯껜
                    //Debug.Log("���吏곸씠�뒗以� �꽣移� 諛쒖깮");
                    //return;
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
                }


            }
        }
<<<<<<< HEAD
        else//터치가 안 되었을때
        {
            grabTime = 0;//터치하고 있지않을때 0으로 만들어야함
            //grabCoolTime += Time.deltaTime;//쿨타임 더하기
            if (!isTouchBottom)//바닥에 닿지 않았을때 계산해야지 밑으로 내려가려는 힘이 없음
            {
                isStop = false;
            }
            else//바닥이 터치 되었을때 collision에서 holdPossion을 하면 되지 않을까
            {
                                    
=======
        else//�꽣移섍�� �븞 �릺�뿀�쓣�븣
        {
            grabTime = 0;//�꽣移섑븯怨� �엳吏��븡�쓣�븣 0�쑝濡� 留뚮뱾�뼱�빞�븿
            //grabCoolTime += Time.deltaTime;//荑⑦���엫 �뜑�븯湲�
            if (!isTouchBottom)//諛붾떏�뿉 �떯吏� �븡�븯�쓣�븣 怨꾩궛�빐�빞吏� 諛묒쑝濡� �궡�젮媛��젮�뒗 �옒�씠 �뾾�쓬
            {
                isStop = false;
                Move();
            }
            else//諛붾떏�씠 �꽣移� �릺�뿀�쓣�븣
            {
                //transform.translate(vector2.zero);
                transform.position = transform.position;
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
            }
        }

    }
<<<<<<< HEAD

    public void Move2(Vector2 destination)
    {
        rg2D.gravityScale = 0.0f;
        rg2D.velocity = destination.normalized * speed * Time.deltaTime;//rigidbody를 이용한 이동 speed 100 
        //transform.Translate(destination * speed * Time.deltaTime);
    }

    IEnumerator DragCooltime()
    {
        yield return new WaitForSeconds(1.0f);//1초가 지났다면
        Debug.Log("코루틴 실행");
        canTouch = true;
    }
    private void Move()//destination값을 전달 해준다면?
    {

        grabCoolTime += Time.deltaTime;//쿨타임 더하기
        //transform.Translate(destination * speed * Time.deltaTime);//이동 부분, destination계산필요 날리는 부분
        //rg2D.velocity=destination * speed * Time.deltaTime
=======
    private void Move()//destination媛믪쓣 �쟾�떖 �빐以��떎硫�?
    {

        grabCoolTime += Time.deltaTime;//荑⑦���엫 �뜑�븯湲�
        transform.Translate(destination * speed * Time.deltaTime);//�씠�룞 遺�遺�, destination怨꾩궛�븘�슂 �궇由щ뒗 遺�遺�
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
        return;
    }

    private void HandleTouchBegan(Touch touch)
    {
        touchStart = true;
        touchStartTime = Time.time;
        isTouchBottom = false;
        if (canTouch)
        {
<<<<<<< HEAD
            player.SetDestination(HoldPossion());
            isStop = true;
        }

        startPos = Camera.main.ScreenToWorldPoint(touch.position);
        Debug.Log("시작 :" + startPos);
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
=======
            HoldPossion();
            isStop = true;
        }
        else
        {
            isStop = false;//���吏곸뿬�빞�븿
            //Move();
        }

        startPos = Camera.main.ScreenToWorldPoint(touch.position);
        Debug.Log("�떆�옉 :" + startPos);
    }

    private void Shoot()
    {
        Debug.Log("shoot");
        startPos = Vector2.zero;
        touchStart = true;//�떒�닚 �꽣移섍린�뿉 
        grabTime = 0;
        canTouch = false;
        endPos = new Vector2(startPos.x, 5);//�샊�떆 screenYpos�쓽 媛믪씠 -媛� �굹�뒗嫄닿��
        grabCoolTime += Time.deltaTime;//荑⑦���엫 �뜑�븯湲�
        //Debug.Log(string.Format("�룄�삎 �쐞移� {0} �솕硫� �걹{1} 紐⑺몴 吏��젏 {2}", startPos, 5, endPos));
        speed = 5;
        destination = (endPos - startPos).normalized;//�뿬湲곕�� 吏��슦硫� 洹몃깷 �뼥�뼱吏�
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
        Debug.Log("shoot" + destination);
        //startPos = Vector2.zero;
        //endPos = Vector2.zero;
        rg2D.gravityScale = setGravityScale();
        return;

<<<<<<< HEAD
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
            touchTime = Time.time - touchStartTime;
            Debug.Log(string.Format("touchTime :{0}", grabTime));
            canTouch = false;
            //isWallHit = false;//드래그가 끝나는 시점에서 false로 전환해 잡지 못하게 하기위함
            touchStart = false;
            isTouchBottom = false;
            isStart = false;
            speed = 100f;//여기 까지 날리기 위한 셋팅이니까 수정
=======
    }

    private void HandleTouchEnd(Touch touch)
    {
        if (uiTouched)//ui�씪�븣
        {
            Debug.Log("UI �꽣移섎줈 醫낅즺");
            uiTouched = false;
            return;
        }
        else//ui遺�遺꾩씠 �븘�땺�븣 
        {
            //if (isDrag)
            //{
            //touchTime = Time.time - touchStartTime;
            Debug.Log(string.Format("touchTime :{0}", grabTime));
            canTouch = false;
            //isWallHit = false;//�뱶�옒洹멸�� �걹�굹�뒗 �떆�젏�뿉�꽌 false濡� �쟾�솚�빐 �옟吏� 紐삵븯寃� �븯湲곗쐞�븿
            touchStart = false;
            isTouchBottom = false;
            isStart = false;
            //grabCoolTime = 0;//荑⑦���엫
            //canTouch = true;
            speed = 3f;//�뿬湲� 源뚯�� �궇由ш린 �쐞�븳 �뀑�똿�씠�땲源� �닔�젙
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
                       //rg2D.gravityScale = setGravityScale();

            endPos = Camera.main.ScreenToWorldPoint(touch.position);
            Debug.Log("endoPos :" + endPos);
<<<<<<< HEAD
            //new Vector2(transform.position.x,transform.position.y) 이걸 넣으면 도형으로 부터 마지막 손가락 위치
            posDistance = Vector2.Distance(startPos, endPos);
            //Debug.Log(string.Format("시작 위치 :{0}, 종료 위치 :{1}, 거리 = {2} ", startPos, endPos, posDistance));
            //}
            if (posDistance < 0.2)//단순 터치 터치를 거리로 판단 
            {
                Debug.Log("단순 터치");
                //Shoot();
                //isStop = false;
                Debug.Log("플레이어 목적지 :"+player.GetPreVec());
                destination = Vector2.Reflect(player.GetPreVec(), endPos).normalized;
                Debug.Log(destination);
                //player.SetDestination(destination);

                /*rg2D.gravityScale = setGravityScale(); //Shoot함수로 이동
                touchStart = true;//단순 터치기에 
=======
            //new Vector2(transform.position.x,transform.position.y) �씠嫄� �꽔�쑝硫� �룄�삎�쑝濡� 遺��꽣 留덉��留� �넀媛��씫 �쐞移�
            posDistance = Vector2.Distance(startPos, endPos);
            //Debug.Log(string.Format("�떆�옉 �쐞移� :{0}, 醫낅즺 �쐞移� :{1}, 嫄곕━ = {2} ", startPos, endPos, posDistance));
            //}
            if (posDistance < 0.2)//�떒�닚 �꽣移� �꽣移섎�� 嫄곕━濡� �뙋�떒 
            {
                Debug.Log("�떒�닚 �꽣移�");
                Shoot();
                isStop = false;

                /*rg2D.gravityScale = setGravityScale(); //Shoot�븿�닔濡� �씠�룞
                touchStart = true;//�떒�닚 �꽣移섍린�뿉 
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
                startPos = transform.position;
                endPos = new Vector2(startPos.x, -screenYpos);
                speed = 5;
                destination=(endPos-startPos).normalized;*/

            }
<<<<<<< HEAD
            else//드래그시  
            {
                destination = (endPos - startPos).normalized;//현재 코드는 화면 어디를 터치 하더라도 같은 이동 방향에 따라 움직임
                destination = VectorCorrection(destination);
                player.SetDestination(destination);
                player.SetpreVec(destination);//이동 벡터 저장
                collsitonCount = 0;
                Debug.Log("드래그 벡터" + destination);
            }
            isDrag = false;
            grabCoolTime = 0;//쿨타임
=======
            else//�뱶�옒洹몄떆  
            {
                destination = (endPos - startPos).normalized;//�쁽�옱 肄붾뱶�뒗 �솕硫� �뼱�뵒瑜� �꽣移� �븯�뜑�씪�룄 媛숈�� �씠�룞 諛⑺뼢�뿉 �뵲�씪 ���吏곸엫
                destination = VectorCorrection(destination);
                Debug.Log("�뱶�옒洹� 踰≫꽣" + destination);
            }
            isDrag = false;
            grabCoolTime = 0;//荑⑦���엫
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
        }

    }

<<<<<<< HEAD
    /*private void HandleTouchMove(Touch touch)
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);//이렇게 해야지 기기마다 화면의 좌표로 설정된다.
        isDrag = true;//드래그 확인을 위함
=======
    private void HandleTouchMove(Touch touch)
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);//�씠�젃寃� �빐�빞吏� 湲곌린留덈떎 �솕硫댁쓽 醫뚰몴濡� �꽕�젙�맂�떎.
        isDrag = true;//�뱶�옒洹� �솗�씤�쓣 �쐞�븿
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538

        if (grabTime > 1)
        {

<<<<<<< HEAD
            Debug.Log("홀딩 1초 지남");
=======
            Debug.Log("����뵫 1珥� 吏��궓");
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
            //rg2D.velocity = preVelocity;
            //Shoot();

        }
<<<<<<< HEAD
        if (isStart)//처음 드래그 부분
        {
            Debug.Log("MoveToward pos =>" + pos);
            transform.position = new Vector2(pos.x, -4.4f);//카메라 바닥
=======
        if (isStart)//泥섏쓬 �뱶�옒洹� 遺�遺�
        {
            Debug.Log("MoveToward pos =>" + pos);
            transform.position = new Vector2(pos.x, -4.4f);//移대찓�씪 諛붾떏
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
        }
        else
        {

            //isthrow = true;
<<<<<<< HEAD
            //Debug.Log("드래그 :" + pos);
        }
    }*/

    private Vector2 HoldPossion()//위치 위치 고정시키는 함수
    {
        rg2D.velocity = Vector2.zero;
        rg2D.gravityScale = 2f;


        transform.Translate(Vector2.zero);//위치 고정
        transform.position = transform.position;
        return Vector2.zero;
    }
=======
            //Debug.Log("�뱶�옒洹� :" + pos);
        }
    }

    private void HoldPossion()//�쐞移� �쐞移� 怨좎젙�떆�궎�뒗 �븿�닔
    {
        rg2D.velocity = Vector2.zero;
        rg2D.gravityScale = 0.0f;

        transform.Translate(Vector2.zero);//�쐞移� 怨좎젙
        transform.position = transform.position;
        return;
    }

>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
    private Vector2 VectorCorrection(Vector2 pos)
    {
        float correctino_posy = 0.0f;
        float correctino_posx = 0.0f;
<<<<<<< HEAD
        if (pos.y <= 0)//0보다 작거나 같으면 보정
=======
        if (pos.y <= 0)//0蹂대떎 �옉嫄곕굹 媛숈쑝硫� 蹂댁젙
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
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
<<<<<<< HEAD
    
    private void StickWall()//벽에 붙어서 미끄러지는 함수
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
=======
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538


}
