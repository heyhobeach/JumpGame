using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class CameraSet : MonoBehaviour
{
    public  float cameraZpos;//전부 스테틱으로 할 필요없었고 그냥 카메라 인스턴스 만들어서 가능 골드메탈 무한배경19:28초 참고
    //public float ratio=0.33f;

    private float startYpos;
    public Vector3 offset=new Vector3(0,0,-10);
    public  Vector2 bottom;
    public  static CameraSet cameraInstance;
    public TestScript player;
    public Vector2 Top;

    public GameObject gameobject;

    [Range(0, 100)]
    public float smoothSpeed=2;//카메라가 따라오는속도 조절하는변수
    public float high = 0;

    [Range(0, 1)]
    public float ratio = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        cameraZpos = transform.position.z;
        Vector2 Right = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height * 0.5f));
        Vector2 Left = -Right;
        Top = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.5f, Screen.height));
        bottom = -Top;
        startYpos = transform.position.y;
        //limitPos = Bottom.y - cameraZpos * ratio;
    }



    void Awake()
    {
        //Screen.SetResolution(1848, 2960, true);
        cameraInstance = this;
        setupCamera();
        //Screen.SetResolution(1080, 2340, true);//표기되어있는 해상도
        //Screen.SetResolution(924, 1480 , true);
    }
    private void FixedUpdate()
    {
        if (Camera.main.WorldToViewportPoint(player.transform.position).y>ratio)//0.3보다 크다면
        {
            follow();
        }
    }

      private void Update()
      {
        Top = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.5f, Screen.height));
        Debug.Log(Top);
        Debug.Log(CheackObjectInCamera(gameobject));
    //    Vector2 Top = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.5f, Screen.));
    //    bottom = -Top;
    //
    //    Debug.Log(bottom);
      }

    private void setupCamera()
    {
        //float targetWidthAspect = 6f;//가로비율
        //float targetHightAspect = 19f;//세로비율

        float targetWidthAspect = 6f;//가로비율
        float targetHightAspect = 13f;//세로비율

        Camera camera = Camera.main;

        float widthRatio = (float)Screen.width / targetWidthAspect;
        float heightRatio = (float)Screen.height / targetHightAspect;

        float heightadd = ((widthRatio / (heightRatio / 100)) - 100) / 200;
        float widthtadd = ((heightRatio / (widthRatio / 100)) - 100) / 200;

        if (heightRatio > widthRatio)
            widthtadd = 0.0f;
        else
            heightadd = 0.0f;


        camera.rect = new Rect(
            camera.rect.x + Mathf.Abs(widthtadd),
            camera.rect.y + Mathf.Abs(heightadd),
            camera.rect.width + (widthtadd * 2),
            camera.rect.height + (heightadd * 2));
    }

    public float GetStartYpos() { 
        return startYpos;
    }
    public float GetCurrentYpos()
    {
        return transform.position.y;
    }

    public bool CheackObjectInCamera(GameObject gameObject)
    {
        bool isIn = true;
        Vector3 offset = new Vector3(0, 0.4f,0);//바닥 스케일
        Vector3 screePoint =Camera.main.WorldToViewportPoint(gameObject.transform.position+offset);
        isIn= screePoint.x >0&& screePoint.y > 0 && screePoint.x<1&&screePoint.y<1&&screePoint.z>0;
        return isIn;
    }

    // Update is called once per frame
    public void follow()
    {
        //transform.position = player.transform.position+offset;
        if (high < player.transform.position.y)
        {
            high = player.transform.position.y;
        }//카메라 비율 high가 현재 화면에서 어느 위치인지 그리고 최대치를 설정함 비율 화면
        //high = player.transform.position.y;
        Vector3 tragetPosition = new Vector3(0,high+3f,0) + offset;
        Vector3 pos = Camera.main.WorldToViewportPoint(tragetPosition);
        //Debug.Log(pos);
        //if (pos.y > 0.5f)
        //{
        //    pos.y = 0.5f;
        //    tragetPosition = Camera.main.ViewportToWorldPoint(pos);
        //}

        //Debug.Log(tragetPosition);
        
        Vector3 smoothPosition = Vector3.Lerp(transform.position, tragetPosition, smoothSpeed * Time.fixedDeltaTime);//러프 특징때문에 괜찮은가?
        transform.position = smoothPosition;
        //transform.position = tragetPosition;
    }
}
