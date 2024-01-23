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

    public void SetDestination(Vector2 destination)//µå·¡±× ÀÌÈÄ º¤ÅÍ °è»êÀÌÈÄ SetÀ¸·Î ¼³Á¤ÇÔ
=======
    private Vector2 destination = Vector2.zero;
    private float speed = 0.0f;
    Transform transform;
    Rigidbody2D rg2D;

    public void Move(Vector2 destination)
    {
        rg2D.velocity = destination.normalized * speed * Time.deltaTime;//rigidbodyë¥¼ ì´ìš©í•œ ì´ë™
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

    public void SetpreVec(Vector2 preVec)//µå·¡±× ÀÌÈÄ º¤ÅÍ °è»êÀÌÈÄ SetÀ¸·Î ¼³Á¤ÇÔ
    {
        this.preVec = preVec;
=======
    private void HoldPossion()//ìœ„ì¹˜ ìœ„ì¹˜ ê³ ì •ì‹œí‚¤ëŠ” í•¨ìˆ˜
    {
        rg2D.velocity = Vector2.zero;
        rg2D.gravityScale = 0.0f;

        //transform.Translate(Vector2.zero);//ìœ„ì¹˜ ê³ ì •
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
    Player player = new Player();//player°´Ã¼ »ý¼º
    private Vector2 position;
    private Rigidbody2D rg2D;
    

    
    private Vector2 startPos;//ÅÍ½Ã ½ÃÀÛ½Ã x pos
    private Vector2 endPos;//ÅÍÄ¡ ½ÃÀÛ½Ã y pos

    private Vector2 destination;//¸ñÀûÁö

    public float speed = 3f;// private·Î ¼öÁ¤ÇÏ°í speed= tapPower¿Í speed=dragPower·Î ¼öÁ¤ °¡´ÉÇÏÁö ¾ÊÀ»±î

    public const byte maxCollsion=2;
    public byte collsitonCount = 0;//Ãæµ¹ È½¼ö countº¯¼ö µå·¡±×½Ã ÃÊ±âÈ­
    public float dragPower;

    public float scale = 0.5f;//Áß·Â

    private float grabTime;//±×·¦ Áö¼Ó½Ã°£
    private float grabCoolTime;//±×·¦ ÄðÅ¸ÀÓ Ã¼Å©

    //public bool isTouchTop = false;//isTouchBottomÀ» Á¦¿ÜÇÑ ³ª¸ÓÁö º¯¼öµéÀº ¾È ¾²°íÀÖÀ½
=======
    Player player = new Player();//playerê°ì²´ ìƒì„±
    private Vector2 position;
    private Rigidbody2D rg2D;
    
    private Vector2 startPos;//í„°ì‹œ ì‹œìž‘ì‹œ x pos
    private Vector2 endPos;//í„°ì¹˜ ì‹œìž‘ì‹œ y pos

    private Vector2 destination;//ëª©ì ì§€

    public float speed = 3f;// privateë¡œ ìˆ˜ì •í•˜ê³  speed= tapPowerì™€ speed=dragPowerë¡œ ìˆ˜ì • ê°€ëŠ¥í•˜ì§€ ì•Šì„ê¹Œ

    public float tapPower;
    public float dragPower;

    public float scale = 0.5f;//ì¤‘ë ¥

    private float grabTime;//ê·¸ëž© ì§€ì†ì‹œê°„
    private float grabCoolTime;//ê·¸ëž© ì¿¨íƒ€ìž„ ì²´í¬

    //public bool isTouchTop = false;//isTouchBottomì„ ì œì™¸í•œ ë‚˜ë¨¸ì§€ ë³€ìˆ˜ë“¤ì€ ì•ˆ ì“°ê³ ìžˆìŒ
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
    public bool isTouchBottom = false;
    //public bool isTouchLeft = false;
    //public bool isTouchRight = false;

    public float controllRatio = 33;
    private float screenYpos;

    public bool isStart = true;

<<<<<<< HEAD
    public bool touchStart = true;//¼ÕÀ» ¶¼°í Á¶ÀÛÇÏ´Â°ÇÁö ¾Æ´ÑÁö ÆÇ´Ü

    private float touchTime;
=======
    public bool touchStart = true;
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538


    //private float touchPosx;

    private float posDistance;

    private float touchStartTime;
    private Vector2 preVelocity;

<<<<<<< HEAD
    public bool isWallHit = false;//Å×½ºÆ®ÈÄ private·Î ¼öÁ¤ ÇÊ¿ä

    public int reflectCnt;

    public bool canTouch = true;//ÃßÈÄ private·Î ¼öÁ¤ ÇÊ¿ä Ãæµ¹°ú cooltime°è»êÈÄ ÅÍÄ¡ °¡´ÉÇÑ Á¶°ÇÀ» ¸¸µë

    public bool uiTouched = false;//UI ÅÍÄ¡ Çß´ÂÁö ÆÇ´ÜÇÏ´Â ºÎºÐ
=======
    private bool isWallHit = false;

    public int reflectCnt;

    public bool canTouch = true;//ì¶”í›„ privateë¡œ ìˆ˜ì • í•„ìš”

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
        transform.localScale = new Vector2(0.5f, 0.5f);// ÇØ»óµµ ºñÀ²¿¡ ¸Â°Ô Å©±â Á¶Àý ±Ùµ¥ Äµ¹ö½º°¡ ÀÖÀ½

        //touchPosx = transform.localPosition.x;
        //screenYpos = CameraSet.limitPos;//screenYpos·Î ±³Ã¼ ÇÏ¸é µÊ ÇØ´ç À§Ä¡´Â Ä³¸¯ÅÍÀÇ À§Ä¡°¡ Ç×»ó ÀÌÂë¿¡ ÀÖÀ»°ÅÀÓ ÀÌ°Åº¸´Ù Ä³¸¯ÅÍ°¡ ´õ À§¿¡ÀÖ´Ù¸é Ä«¸Þ¶ó°¡ ¿òÁ÷ÀÓ
        cooltimeCoroutine = DragCooltime();


        //Debug.Log(string.Format("È­¸é 1/3¾Æ·¡:{0} È­¸é 2/3À§ :{1} È­¸é ²À´ë±â{2}", -screenYpos, screenYpos,CameraSet.Top.y));
=======
        transform.localScale = new Vector2(0.5f, 0.5f);// í•´ìƒë„ ë¹„ìœ¨ì— ë§žê²Œ í¬ê¸° ì¡°ì ˆ ê·¼ë° ìº”ë²„ìŠ¤ê°€ ìžˆìŒ

        //touchPosx = transform.localPosition.x;
        screenYpos = CameraSet.limitPos;//screenYposë¡œ êµì²´ í•˜ë©´ ë¨ í•´ë‹¹ ìœ„ì¹˜ëŠ” ìºë¦­í„°ì˜ ìœ„ì¹˜ê°€ í•­ìƒ ì´ì¯¤ì— ìžˆì„ê±°ìž„ ì´ê±°ë³´ë‹¤ ìºë¦­í„°ê°€ ë” ìœ„ì—ìžˆë‹¤ë©´ ì¹´ë©”ë¼ê°€ ì›€ì§ìž„

        //Debug.Log(string.Format("í™”ë©´ 1/3ì•„ëž˜:{0} í™”ë©´ 2/3ìœ„ :{1} í™”ë©´ ê¼­ëŒ€ê¸°{2}", -screenYpos, screenYpos,CameraSet.Top.y));
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
        if (!canTouch)//ÅÍÄ¡ ¸ø ÇÒ¶§ ÄÚ·çÆ¾ È£Ãâ
        {
            StartCoroutine(cooltimeCoroutine);
        }
        
        if (isStop)//isDrag¶ó°í ±×³É ÀÓÀÇ·Î º¯¼ö ¼³Á¤ÇÑ°Í ,trueÀÏ¶§
        {
            //player.SetDestination(HoldPossion());
        }
        else
        {
            //player.SetDestination(player.GetDestinaion());
            
        //    //player.SetDestination(player.GetDestinaion());
        }//¿©±â ±îÁö ±×³É Å×½ºÆ®¸¦ À§ÇØ player°´Ã¼¿¡¼­ Á¤º¸ ¹Þ¾Æ ¿òÁ÷ÀÌ´ÂÁö È®ÀÎÀ» À§ÇÑ ÄÚµå

        OnTouchEvent();//ÅÍÄ¡·Î ÀÎÇÑ °ªÀ» player¿¡ ¼ÂÆÃÇÔ

    }

    public float setGravityScale()//ÀÌ ÇÔ¼ö°¡ ±»ÀÌ ÇÊ¿äÇÒ±î
=======
        //player.SetSpeed(speed++);
        position = rg2D.velocity;// ì´ ë¶€ë¶„ì˜ í•„ìš”ëŠ”?
        //Debug.Log("Update");
        //OnTouchEvent();
        //transform.Translate(destination * speed * Time.deltaTime, Space.World);// ì´ ë¶€ë¶„ì€ í•„ìš”ê°€ ì—†ëŠ”ê²ƒê°™ìŒ
        /*if (touchStart && transform.position.y >= screenYpos)// í•´ë‹¹ ë¶€ë¶„ í•„ìš”ì—†ëŠ”ê²ƒ ê°™ì•„ ì£¼ì„ì²˜ë¦¬
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
            //isWallHit = true;//ë‘ ë³€ìˆ˜ê°€ ì¤‘ë³µ ë˜ëŠ”ê²ƒê°™ìŒ
            canTouch = true;
        }

    }

    public float setGravityScale()//ì´ í•¨ìˆ˜ê°€ êµ³ì´ í•„ìš”í• ê¹Œ
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
    {
        return scale;
    }

<<<<<<< HEAD
    private void OnCollisionEnter2D(Collision2D collision)//Ãæµ¹½Ã ¹ß»ý
    {
        //Debug.Log("collision");
        Debug.Log(collision.gameObject.name);
        //Vector2 inDirection = GetComponent<Rigidbody2D>().velocity;
        //destination = Vector2.Reflect(destination, collision.GetContact(0).normal).normalized;//Ãæµ¹½Ã Àü¹Ý»ç·Î º¤ÅÍ ¹æÇâ ¼öÁ¤
        //player.SetDestination(destination);
        //player.SetDestination(Vector2.zero);
        //StickWall();

        if (collision.gameObject.tag == "border")//ÁÂ ¿ì °æ°è¿¡ ´êÀ»°æ¿ì
        {
            if (collsitonCount < maxCollsion-1)
            {
                collsitonCount++;
            }
            else
            {
                player.SetDestination(HoldPossion());
                Debug.Log("Ãæµ¹ ³¡");
                return;
            }
            
            isWallHit = true ;//º®¿¡ Ãæµ¹ÇÒ°æ¿ì ÇØ´ç ¶óÀÎÀÌ ÀÖ¾î¾ß ¸¸¾à ´Ù½Ã ÅÍÄ¡ °¡´ÉÇÑ ¹üÀ§? ¾È¿¡¼­ Ãæµ¹ÀÏ¾î³ª ´Ù½Ã ÀâÀ»°æ¿ì¸¦ À§ÇÑ°Í
            destination = Vector2.Reflect(destination, collision.GetContact(0).normal).normalized;//Ãæµ¹½Ã Àü¹Ý»ç·Î º¤ÅÍ ¹æÇâ ¼öÁ¤
            player.SetDestination(destination);
            canTouch = true;
            StopCoroutine(cooltimeCoroutine);
            //speed /= 2;//º®¿¡ Ãæµ¹½Ã ¼Óµµ °¨¼Ó
            switch (collision.gameObject.name)
            {
                case "top"://ÇØ´ç ºÎºÐÀº ¾Æ¸¶ ¾µ ÀÏÀº ¾øÀ»°Å°°±äÇÔ
=======
    private void OnCollisionEnter2D(Collision2D collision)//ì¶©ëŒì‹œ ë°œìƒ
    {
        Debug.Log("collision");
        Debug.Log(collision.gameObject.name);
        //Vector2 inDirection = GetComponent<Rigidbody2D>().velocity;
        destination = Vector2.Reflect(destination, collision.GetContact(0).normal).normalized;//ì¶©ëŒì‹œ ì „ë°˜ì‚¬ë¡œ ë²¡í„° ë°©í–¥ ìˆ˜ì •

        if (collision.gameObject.tag == "border")//ì¢Œ ìš° ê²½ê³„ì— ë‹¿ì„ê²½ìš°
        {
            isWallHit = true;//ë²½ì— ì¶©ëŒí• ê²½ìš° í•´ë‹¹ ë¼ì¸ì´ ìžˆì–´ì•¼ ë§Œì•½ ë‹¤ì‹œ í„°ì¹˜ ê°€ëŠ¥í•œ ë²”ìœ„? ì•ˆì—ì„œ ì¶©ëŒì¼ì–´ë‚˜ ë‹¤ì‹œ ìž¡ì„ê²½ìš°ë¥¼ ìœ„í•œê²ƒ
            canTouch = true;
            speed /= 2;//ë²½ì— ì¶©ëŒì‹œ ì†ë„ ê°ì†
            switch (collision.gameObject.name)
            {
                case "top"://í•´ë‹¹ ë¶€ë¶„ì€ ì•„ë§ˆ ì“¸ ì¼ì€ ì—†ì„ê±°ê°™ê¸´í•¨
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
                    //rg2D.gravityScale = setGravityScale();//¿©±â ºÎºÐ¿¡¼­ »ý±â´Â ¹®Á¦ Áß·ÂÀÌ »ý°Ü¹ö·Á¼­
                    rg2D.gravityScale = scale;
=======
                    //isTouchLeft = false;
                    //isTouchRight = false;

                    break;
                case "left":
                    //rg2D.gravityScale = setGravityScale();//ì—¬ê¸° ë¶€ë¶„ì—ì„œ ìƒê¸°ëŠ” ë¬¸ì œ ì¤‘ë ¥ì´ ìƒê²¨ë²„ë ¤ì„œ
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
    private void OnTouchEvent()//ÅÍÄ¡¸¦ ÅëÇÑ Á¶ÀÛÀ» ÆÇ´Ü
    {

        if (Input.touchCount > 0)//ÅÍÄ¡°¡ µÉ°æ¿ì
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) != false)//UiºÎºÐ ÅÍÄ¡ÆÇ´Ü
=======
    private void OnTouchEvent()//ì´ë™ ì´ë²¤íŠ¸
    {

        if (Input.touchCount == 1)//Input.touchCount > 0
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) != false)//Uië¶€ë¶„ í„°ì¹˜íŒë‹¨
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
            {
                Debug.Log("UiTouch");
                uiTouched = true;
                return;
            }
<<<<<<< HEAD
            else//ui°¡¾Æ´Ô
            { 
                Touch touch = Input.GetTouch(0);//Ã³À½ ÅÍÄ¡µÈ Á¤º¸
                grabTime += Time.deltaTime;

                if (touch.phase == TouchPhase.Began && canTouch)
                {
                    Debug.Log("Began1");
                    HandleTouchBegan(touch);//¿òÁ÷ÀÌ¸é ¾ÈµÊ
                }
                else if (touch.phase == TouchPhase.Ended && canTouch)
                {
                    Debug.Log("End2");
                    HandleTouchEnd(touch);
                }
                else if (touch.phase == TouchPhase.Stationary && canTouch || touch.phase == TouchPhase.Moved && canTouch)//È¦µùÀÌ°Å³ª µå·¡±×½Ã, canTouch¸¦ Á¶°Ç¿¡ ³Ö¾î¾ßÇÔ
                {

=======
            else//uiê°€ì•„ë‹˜
            {
                Touch touch = Input.GetTouch(0);//ì²˜ìŒ í„°ì¹˜ëœ ì •ë³´
                grabTime += Time.deltaTime;

                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("Began1");
                    endPos = Camera.main.ScreenToWorldPoint(touch.position);
                    if (!canTouch)//HandleTouchBeaganì•ˆì— ì¡°ê±´ì´ìžˆìŒ
                    {
                        isStop = false;//ì›€ì§ì—¬ì•¼í•¨
                    }
                    else
                    {
                        HandleTouchBegan(touch);//ì›€ì§ì´ë©´ ì•ˆë¨

                    }
                }
                else if (touch.phase == TouchPhase.Ended && canTouch)
                {
                    isStop = false;//ì›€ì§ì—¬ì•¼í•¨
                    Debug.Log("End2");
                    HandleTouchEnd(touch);
                }
                //else if (touch.phase == TouchPhase.Moved)//ë‘ê°œ í•©ì¹ ìˆ˜ìžˆì§€ ì•Šì„ê¹Œ
                //{
                //Debug.Log("Move");
                //HandleTouchMove(touch);
                //}
                else if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)//í™€ë”©ì´ê±°ë‚˜ ë“œëž˜ê·¸ì‹œ
                {
                    //Debug.Log("í™€ë“œ ì•¡ì…˜ í•¨ìˆ˜");
                    if (grabTime > 1 || !canTouch)
                    {
                        //rg2D.velocity = preVelocity;
                        isStop = false;
                        //Move();
                        return;
                    }
                    //ë‚˜ë¨¸ì§€ëŠ” ì›€ì§ì´ë©´ ì•ˆ ë ê±°ê°™ì€ë°
                    //else{isStop=true};

                }

                //}
                else//ìœ„ ì¼ë•Œ
                {

                    //Move();
                    isStop = false;
                    Debug.Log("destination" + destination);
                    //transform.Translate(destination * speed * Time.deltaTime, Space.World);//ì´ë™ ë¶€ë¶„, destinationê³„ì‚°í•„ìš” ë‚ ë¦¬ëŠ” ë¶€ë¶„ Moveí•¨ìˆ˜ë¡œ êµì²´
                    //Debug.Log("ì›€ì§ì´ëŠ”ì¤‘ í„°ì¹˜ ë°œìƒ");
                    //return;
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
                }


            }
        }
<<<<<<< HEAD
        else//ÅÍÄ¡°¡ ¾È µÇ¾úÀ»¶§
        {
            grabTime = 0;//ÅÍÄ¡ÇÏ°í ÀÖÁö¾ÊÀ»¶§ 0À¸·Î ¸¸µé¾î¾ßÇÔ
            //grabCoolTime += Time.deltaTime;//ÄðÅ¸ÀÓ ´õÇÏ±â
            if (!isTouchBottom)//¹Ù´Ú¿¡ ´êÁö ¾Ê¾ÒÀ»¶§ °è»êÇØ¾ßÁö ¹ØÀ¸·Î ³»·Á°¡·Á´Â ÈûÀÌ ¾øÀ½
            {
                isStop = false;
            }
            else//¹Ù´ÚÀÌ ÅÍÄ¡ µÇ¾úÀ»¶§ collision¿¡¼­ holdPossionÀ» ÇÏ¸é µÇÁö ¾ÊÀ»±î
            {
                                    
=======
        else//í„°ì¹˜ê°€ ì•ˆ ë˜ì—ˆì„ë•Œ
        {
            grabTime = 0;//í„°ì¹˜í•˜ê³  ìžˆì§€ì•Šì„ë•Œ 0ìœ¼ë¡œ ë§Œë“¤ì–´ì•¼í•¨
            //grabCoolTime += Time.deltaTime;//ì¿¨íƒ€ìž„ ë”í•˜ê¸°
            if (!isTouchBottom)//ë°”ë‹¥ì— ë‹¿ì§€ ì•Šì•˜ì„ë•Œ ê³„ì‚°í•´ì•¼ì§€ ë°‘ìœ¼ë¡œ ë‚´ë ¤ê°€ë ¤ëŠ” íž˜ì´ ì—†ìŒ
            {
                isStop = false;
                Move();
            }
            else//ë°”ë‹¥ì´ í„°ì¹˜ ë˜ì—ˆì„ë•Œ
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
        rg2D.velocity = destination.normalized * speed * Time.deltaTime;//rigidbody¸¦ ÀÌ¿ëÇÑ ÀÌµ¿ speed 100 
        //transform.Translate(destination * speed * Time.deltaTime);
    }

    IEnumerator DragCooltime()
    {
        yield return new WaitForSeconds(1.0f);//1ÃÊ°¡ Áö³µ´Ù¸é
        Debug.Log("ÄÚ·çÆ¾ ½ÇÇà");
        canTouch = true;
    }
    private void Move()//destination°ªÀ» Àü´Þ ÇØÁØ´Ù¸é?
    {

        grabCoolTime += Time.deltaTime;//ÄðÅ¸ÀÓ ´õÇÏ±â
        //transform.Translate(destination * speed * Time.deltaTime);//ÀÌµ¿ ºÎºÐ, destination°è»êÇÊ¿ä ³¯¸®´Â ºÎºÐ
        //rg2D.velocity=destination * speed * Time.deltaTime
=======
    private void Move()//destinationê°’ì„ ì „ë‹¬ í•´ì¤€ë‹¤ë©´?
    {

        grabCoolTime += Time.deltaTime;//ì¿¨íƒ€ìž„ ë”í•˜ê¸°
        transform.Translate(destination * speed * Time.deltaTime);//ì´ë™ ë¶€ë¶„, destinationê³„ì‚°í•„ìš” ë‚ ë¦¬ëŠ” ë¶€ë¶„
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
        Debug.Log("½ÃÀÛ :" + startPos);
    }

    /*private void Shoot()//ÇöÀç »ç¿ëÇÏÁö ¾ÊÀ½
    {
        Debug.Log("shoot");
        startPos = Vector2.zero;
        touchStart = true;//´Ü¼ø ÅÍÄ¡±â¿¡ 
        grabTime = 0;
        canTouch = false;
        endPos = new Vector2(startPos.x, 5);//È¤½Ã screenYposÀÇ °ªÀÌ -°¡ ³ª´Â°Ç°¡
        grabCoolTime += Time.deltaTime;//ÄðÅ¸ÀÓ ´õÇÏ±â
        //Debug.Log(string.Format("µµÇü À§Ä¡ {0} È­¸é ³¡{1} ¸ñÇ¥ ÁöÁ¡ {2}", startPos, 5, endPos));
        speed = 5;
        destination = new Vecotor2(0.0,1.0f).normalized;//¿©±â¸¦ Áö¿ì¸é ±×³É ¶³¾îÁü
=======
            HoldPossion();
            isStop = true;
        }
        else
        {
            isStop = false;//ì›€ì§ì—¬ì•¼í•¨
            //Move();
        }

        startPos = Camera.main.ScreenToWorldPoint(touch.position);
        Debug.Log("ì‹œìž‘ :" + startPos);
    }

    private void Shoot()
    {
        Debug.Log("shoot");
        startPos = Vector2.zero;
        touchStart = true;//ë‹¨ìˆœ í„°ì¹˜ê¸°ì— 
        grabTime = 0;
        canTouch = false;
        endPos = new Vector2(startPos.x, 5);//í˜¹ì‹œ screenYposì˜ ê°’ì´ -ê°€ ë‚˜ëŠ”ê±´ê°€
        grabCoolTime += Time.deltaTime;//ì¿¨íƒ€ìž„ ë”í•˜ê¸°
        //Debug.Log(string.Format("ë„í˜• ìœ„ì¹˜ {0} í™”ë©´ ë{1} ëª©í‘œ ì§€ì  {2}", startPos, 5, endPos));
        speed = 5;
        destination = (endPos - startPos).normalized;//ì—¬ê¸°ë¥¼ ì§€ìš°ë©´ ê·¸ëƒ¥ ë–¨ì–´ì§
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
        if (uiTouched)//uiÀÏ¶§
        {
            Debug.Log("UI ÅÍÄ¡·Î Á¾·á");
            uiTouched = false;
            return;
        }
        else//uiºÎºÐÀÌ ¾Æ´Ò¶§ 
        {
            //if (isDrag)
            //{
            touchTime = Time.time - touchStartTime;
            Debug.Log(string.Format("touchTime :{0}", grabTime));
            canTouch = false;
            //isWallHit = false;//µå·¡±×°¡ ³¡³ª´Â ½ÃÁ¡¿¡¼­ false·Î ÀüÈ¯ÇØ ÀâÁö ¸øÇÏ°Ô ÇÏ±âÀ§ÇÔ
            touchStart = false;
            isTouchBottom = false;
            isStart = false;
            speed = 100f;//¿©±â ±îÁö ³¯¸®±â À§ÇÑ ¼ÂÆÃÀÌ´Ï±î ¼öÁ¤
=======
    }

    private void HandleTouchEnd(Touch touch)
    {
        if (uiTouched)//uiì¼ë•Œ
        {
            Debug.Log("UI í„°ì¹˜ë¡œ ì¢…ë£Œ");
            uiTouched = false;
            return;
        }
        else//uië¶€ë¶„ì´ ì•„ë‹ë•Œ 
        {
            //if (isDrag)
            //{
            //touchTime = Time.time - touchStartTime;
            Debug.Log(string.Format("touchTime :{0}", grabTime));
            canTouch = false;
            //isWallHit = false;//ë“œëž˜ê·¸ê°€ ëë‚˜ëŠ” ì‹œì ì—ì„œ falseë¡œ ì „í™˜í•´ ìž¡ì§€ ëª»í•˜ê²Œ í•˜ê¸°ìœ„í•¨
            touchStart = false;
            isTouchBottom = false;
            isStart = false;
            //grabCoolTime = 0;//ì¿¨íƒ€ìž„
            //canTouch = true;
            speed = 3f;//ì—¬ê¸° ê¹Œì§€ ë‚ ë¦¬ê¸° ìœ„í•œ ì…‹íŒ…ì´ë‹ˆê¹Œ ìˆ˜ì •
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
                       //rg2D.gravityScale = setGravityScale();

            endPos = Camera.main.ScreenToWorldPoint(touch.position);
            Debug.Log("endoPos :" + endPos);
<<<<<<< HEAD
            //new Vector2(transform.position.x,transform.position.y) ÀÌ°É ³ÖÀ¸¸é µµÇüÀ¸·Î ºÎÅÍ ¸¶Áö¸· ¼Õ°¡¶ô À§Ä¡
            posDistance = Vector2.Distance(startPos, endPos);
            //Debug.Log(string.Format("½ÃÀÛ À§Ä¡ :{0}, Á¾·á À§Ä¡ :{1}, °Å¸® = {2} ", startPos, endPos, posDistance));
            //}
            if (posDistance < 0.2)//´Ü¼ø ÅÍÄ¡ ÅÍÄ¡¸¦ °Å¸®·Î ÆÇ´Ü 
            {
                Debug.Log("´Ü¼ø ÅÍÄ¡");
                //Shoot();
                //isStop = false;
                Debug.Log("ÇÃ·¹ÀÌ¾î ¸ñÀûÁö :"+player.GetPreVec());
                destination = Vector2.Reflect(player.GetPreVec(), endPos).normalized;
                Debug.Log(destination);
                //player.SetDestination(destination);

                /*rg2D.gravityScale = setGravityScale(); //ShootÇÔ¼ö·Î ÀÌµ¿
                touchStart = true;//´Ü¼ø ÅÍÄ¡±â¿¡ 
=======
            //new Vector2(transform.position.x,transform.position.y) ì´ê±¸ ë„£ìœ¼ë©´ ë„í˜•ìœ¼ë¡œ ë¶€í„° ë§ˆì§€ë§‰ ì†ê°€ë½ ìœ„ì¹˜
            posDistance = Vector2.Distance(startPos, endPos);
            //Debug.Log(string.Format("ì‹œìž‘ ìœ„ì¹˜ :{0}, ì¢…ë£Œ ìœ„ì¹˜ :{1}, ê±°ë¦¬ = {2} ", startPos, endPos, posDistance));
            //}
            if (posDistance < 0.2)//ë‹¨ìˆœ í„°ì¹˜ í„°ì¹˜ë¥¼ ê±°ë¦¬ë¡œ íŒë‹¨ 
            {
                Debug.Log("ë‹¨ìˆœ í„°ì¹˜");
                Shoot();
                isStop = false;

                /*rg2D.gravityScale = setGravityScale(); //Shootí•¨ìˆ˜ë¡œ ì´ë™
                touchStart = true;//ë‹¨ìˆœ í„°ì¹˜ê¸°ì— 
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
                startPos = transform.position;
                endPos = new Vector2(startPos.x, -screenYpos);
                speed = 5;
                destination=(endPos-startPos).normalized;*/

            }
<<<<<<< HEAD
            else//µå·¡±×½Ã  
            {
                destination = (endPos - startPos).normalized;//ÇöÀç ÄÚµå´Â È­¸é ¾îµð¸¦ ÅÍÄ¡ ÇÏ´õ¶óµµ °°Àº ÀÌµ¿ ¹æÇâ¿¡ µû¶ó ¿òÁ÷ÀÓ
                destination = VectorCorrection(destination);
                player.SetDestination(destination);
                player.SetpreVec(destination);//ÀÌµ¿ º¤ÅÍ ÀúÀå
                collsitonCount = 0;
                Debug.Log("µå·¡±× º¤ÅÍ" + destination);
            }
            isDrag = false;
            grabCoolTime = 0;//ÄðÅ¸ÀÓ
=======
            else//ë“œëž˜ê·¸ì‹œ  
            {
                destination = (endPos - startPos).normalized;//í˜„ìž¬ ì½”ë“œëŠ” í™”ë©´ ì–´ë””ë¥¼ í„°ì¹˜ í•˜ë”ë¼ë„ ê°™ì€ ì´ë™ ë°©í–¥ì— ë”°ë¼ ì›€ì§ìž„
                destination = VectorCorrection(destination);
                Debug.Log("ë“œëž˜ê·¸ ë²¡í„°" + destination);
            }
            isDrag = false;
            grabCoolTime = 0;//ì¿¨íƒ€ìž„
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
        }

    }

<<<<<<< HEAD
    /*private void HandleTouchMove(Touch touch)
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);//ÀÌ·¸°Ô ÇØ¾ßÁö ±â±â¸¶´Ù È­¸éÀÇ ÁÂÇ¥·Î ¼³Á¤µÈ´Ù.
        isDrag = true;//µå·¡±× È®ÀÎÀ» À§ÇÔ
=======
    private void HandleTouchMove(Touch touch)
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);//ì´ë ‡ê²Œ í•´ì•¼ì§€ ê¸°ê¸°ë§ˆë‹¤ í™”ë©´ì˜ ì¢Œí‘œë¡œ ì„¤ì •ëœë‹¤.
        isDrag = true;//ë“œëž˜ê·¸ í™•ì¸ì„ ìœ„í•¨
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538

        if (grabTime > 1)
        {

<<<<<<< HEAD
            Debug.Log("È¦µù 1ÃÊ Áö³²");
=======
            Debug.Log("í™€ë”© 1ì´ˆ ì§€ë‚¨");
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
            //rg2D.velocity = preVelocity;
            //Shoot();

        }
<<<<<<< HEAD
        if (isStart)//Ã³À½ µå·¡±× ºÎºÐ
        {
            Debug.Log("MoveToward pos =>" + pos);
            transform.position = new Vector2(pos.x, -4.4f);//Ä«¸Þ¶ó ¹Ù´Ú
=======
        if (isStart)//ì²˜ìŒ ë“œëž˜ê·¸ ë¶€ë¶„
        {
            Debug.Log("MoveToward pos =>" + pos);
            transform.position = new Vector2(pos.x, -4.4f);//ì¹´ë©”ë¼ ë°”ë‹¥
>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
        }
        else
        {

            //isthrow = true;
<<<<<<< HEAD
            //Debug.Log("µå·¡±× :" + pos);
        }
    }*/

    private Vector2 HoldPossion()//À§Ä¡ À§Ä¡ °íÁ¤½ÃÅ°´Â ÇÔ¼ö
    {
        rg2D.velocity = Vector2.zero;
        rg2D.gravityScale = 2f;


        transform.Translate(Vector2.zero);//À§Ä¡ °íÁ¤
        transform.position = transform.position;
        return Vector2.zero;
    }
=======
            //Debug.Log("ë“œëž˜ê·¸ :" + pos);
        }
    }

    private void HoldPossion()//ìœ„ì¹˜ ìœ„ì¹˜ ê³ ì •ì‹œí‚¤ëŠ” í•¨ìˆ˜
    {
        rg2D.velocity = Vector2.zero;
        rg2D.gravityScale = 0.0f;

        transform.Translate(Vector2.zero);//ìœ„ì¹˜ ê³ ì •
        transform.position = transform.position;
        return;
    }

>>>>>>> 59d23899158dfeb72e74b410882b68be8c33a538
    private Vector2 VectorCorrection(Vector2 pos)
    {
        float correctino_posy = 0.0f;
        float correctino_posx = 0.0f;
<<<<<<< HEAD
        if (pos.y <= 0)//0º¸´Ù ÀÛ°Å³ª °°À¸¸é º¸Á¤
=======
        if (pos.y <= 0)//0ë³´ë‹¤ ìž‘ê±°ë‚˜ ê°™ìœ¼ë©´ ë³´ì •
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
    
    private void StickWall()//º®¿¡ ºÙ¾î¼­ ¹Ì²ô·¯Áö´Â ÇÔ¼ö
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
