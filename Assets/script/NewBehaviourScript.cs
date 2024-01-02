using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 position;

    private Vector2 startPos;//터시 시작시 x pos
    private Vector2 endPos;//터치 시작시 y pos

    public float speed = 2f;

    public bool isTouchTop = false;
    public bool isTouchBottom = false;
    public bool isTouchLeft = false;
    public bool isTouchRight = false;

    private bool isdrag = false;

    void Start()
    {
        Debug.Log("hello");
        transform.position = new Vector2(0, -4);
        position= transform.position;
        //transform.localScale = new Vector2(0.5f, 0.5f);// 해상도 비율에 맞게 크기 조절 근데 캔버스가 있음
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isTouchLeft)
        //{
        //    position.x += -0.5f * speed * Time.deltaTime;
        //    transform.position = position;
        //}else if (isTouchLeft)
        //{
        //    position.x += 0.5f * speed * Time.deltaTime;
        //    transform.position = position;
        //}

        //if (isTouchLeft)
        //{
        //    position.x = -1;
        //}
        //if (Input.GetMouseButtonDown(0))
        //{
        //   Debug.Log("click");
        //}
        //Debug.Log(Input.touchCount);
        OnTouchEvent();
    }

    /*private void OnMouseDrag()
    {
        //Debug.Log("Drag");
        isdrag = true;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position = pos;
        transform.position = pos;
        //Debug.Log(transform.position);
        //transform.position=ray

    }*/

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
                    break;
                case "right":
                    isTouchRight = true;
                    isTouchLeft = false;
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

    private void OnTouchEvent()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);//처음 터치된 정보
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("터치 시작");
                startPos= Camera.main.ScreenToWorldPoint(touch.position);
                //transform.position = startPos;
                Debug.Log("시작 :"+startPos);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isdrag = false;
                endPos = Camera.main.ScreenToWorldPoint(touch.position);
                Debug.Log("종료 :"+endPos);
                //transform.position = Vector2.MoveTowards(startPos, endPos,speed*Time.deltaTime);
                Debug.Log("터치 종료");
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);//이렇게 해야지 기기마다 화면의 좌표로 설정된다.
                isdrag = true;
                Debug.Log("드래그 :"+pos);
                //position.x = pos.x;
                //transform.position = position;
            }


        }
        //transform.position = Vector2.MoveTowards(startPos, endPos, speed * Time.deltaTime);
    }
}
