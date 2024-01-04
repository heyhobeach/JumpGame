using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 position;
    private Rigidbody2D rg2D;

    private Vector2 startPos;//터시 시작시 x pos
    private Vector2 endPos;//터치 시작시 y pos

    private Vector2 destination;

    public float speed = 1f;

    public bool isTouchTop = false;
    public bool isTouchBottom = false;
    public bool isTouchLeft = false;
    public bool isTouchRight = false;

    private bool isdrag = false;


    void Start()
    {
        Debug.Log("hello");
        //destination = new Vector2(-1.4f, 4);
        transform.position = new Vector2(0, -4);
        position = transform.position;
        rg2D = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector2(0.5f, 0.5f);// 해상도 비율에 맞게 크기 조절 근데 캔버스가 있음
    }

    // Update is called once per frame
    void Update()
    {
        position = rg2D.velocity;
        OnTouchEvent();
        //transform.Translate(destination * speed * Time.deltaTime, Space.World);

    }


    private void OnCollisionEnter2D(Collision2D collision)//충돌시 발생
    {
        Debug.Log("collision");
        Debug.Log(collision.gameObject.name);
        Vector2 inDirection = GetComponent<Rigidbody2D>().velocity;
        destination = Vector2.Reflect(destination, collision.GetContact(0).normal);

        if (collision.gameObject.tag == "border")//좌 우 경계에 닿을경우
        {
            switch (collision.gameObject.name)
            {
                case "top":
                    isTouchTop = true;
                    break;
                case "bottom":
                    isTouchBottom = true;

                    break;
                case "left":
                    isTouchLeft = true;
                    isTouchRight = false;

                    break;
                case "right":
                    isTouchRight = true;
                    isTouchLeft = false;
                    break;

            }
        }
    }

    private void OnMouseUp()
    {
        Debug.Log("클릭끝");
        isdrag = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");
        if (collision.gameObject.tag == "border")//좌 우 경계에 닿을경우
        {
            switch (collision.gameObject.name)
            {
                case "top":
                    isTouchTop = true;
                    break;
                case "bottom":
                    isTouchBottom = true;
                    break;
                case "left":
                    isTouchLeft = true;
                    isTouchRight = false;
                    //destination.x *= -1;
                    break;
                case "right":
                    isTouchRight = true;
                    isTouchLeft = false;
                    //destination.x *= -1;
                    break;

            }
        }
    }

    private void OnTriggerExit(Collider other)//지금 문제 if 에 안 들어가짐
    {
        Debug.Log("Exit");
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
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);//처음 터치된 정보
            if (touch.phase == TouchPhase.Began)
            {
                startPos = Camera.main.ScreenToWorldPoint(touch.position);
                Debug.Log("시작 :" + startPos);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isdrag = false;
                endPos = Camera.main.ScreenToWorldPoint(touch.position);
                destination = (endPos - startPos).normalized;//현재 코드는 화면 어디를 터치 하더라도 같은 이동 방향에 따라 움직임
                //new Vector2(transform.position.x,transform.position.y) 이걸 넣으면 도형으로 부터 마지막 손가락 위치
                Debug.Log("종료 :" + endPos);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);//이렇게 해야지 기기마다 화면의 좌표로 설정된다.
                isdrag = true;
                Debug.Log("드래그 :" + pos);
            }


        }
        else
        {
            transform.Translate(destination * speed * Time.deltaTime, Space.World);//destination계산필요
        }
    }
}
