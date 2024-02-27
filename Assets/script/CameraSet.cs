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

    public Camera _camera;

    [Range(0, 10)]
    public float smoothSpeed=2;//카메라가 따라오는속도 조절하는변수
    public float high;

    [Range(0, 1)]
    public float ratio = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        cameraZpos = transform.position.z;
        Vector2 Right = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height * 0.5f));
        Vector2 Left = -Right;
        Top = _camera.ScreenToWorldPoint(new Vector2(Screen.width * 0.5f, Screen.height));
        bottom = -Top;
        startYpos = transform.position.y;
        high = player.transform.position.y;
        Debug.Log("high" + high);
        Debug.Log("카메라 스크립트" + bottom);
        //limitPos = Bottom.y - cameraZpos * ratio;
    }



    void Awake()
    {
        //Screen.SetResolution(1848, 2960, true);
        cameraInstance = this;
        //StartCoroutine(NewFllow());
        //transform.position = new Vector3(0, -0.74f, -10);
        // setupCamera();
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
        // Debug.Log(Top);
        // Debug.Log(CheackObjectInCamera(gameobject));
    //    Vector2 Top = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.5f, Screen.));
    //    bottom = -Top;
    //
    //    Debug.Log(bottom);
      }

    private void setupCamera()//래터 박스 
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

    IEnumerator NewFllow()
    {

        float duration = 0.5f;
        float delta = 0f;
        while (true)
        {
            Debug.Log("카메라 코루틴 안");

            while (delta <= duration)
            {
                if (high < player.transform.position.y)
                {
                    high = player.transform.position.y;
                    Debug.Log("카메라 최고높이");
                }
                else
                {
                    Debug.Log("카메라 루프 종료");
                    break;
                }
                float x = delta / duration;
                x=(x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x));
                Vector3 tragetPosition = new Vector3(0, high, 0) + offset;
                Vector3 smoothPosition = Vector3.Lerp(transform.position, tragetPosition, x);//러프 특징때문에 괜찮은가?
                transform.position = smoothPosition;
                yield return null;
            }
            delta = 0;

        }
        
    }

    // Update is called once per frame
    public void follow()//해당 함수는 코루틴으로 변경가능 다만 현재 이 스크립트에서 fixedupdate에서 한개의 함수만 돌아가므로 이렇게 작성함
    {
        //transform.position = player.transform.position+offset;
        float duration = 1f;
        float delta = 0f;
        if (high < player.transform.position.y)//이 부분만 카메라 따라가야함
        {
            Debug.Log("카메라 이동");
            high = player.transform.position.y;
            while(delta <= duration)
            {
                float x = delta / duration;
                //x=(x==1?1:1 - Mathf.Pow(2, -10 * x));//원하는 보간 함수 공식 사용
                x=Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2));
                Vector3 targetPosition = new Vector3(0, high+1.5f, 0) + offset;
                Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, x);
                if (transform.position.y > smoothPosition.y)//시작부터 캐릭터를 잡아서 화면 내려가는것을 방지하기 위함
                {
                    break;
                }
                transform.position = smoothPosition;
                delta += Time.deltaTime;
                continue;
            }
            delta = 0;

        }
        else//이렇게 해도 되려나
        {
            return;
        }
        //Vector3 tragetPosition = new Vector3(0,high,0) + offset;
        //Vector3 smoothPosition = Vector3.Lerp(transform.position, tragetPosition, smoothSpeed * Time.fixedDeltaTime);//러프 특징때문에 괜찮은가?
        //transform.position = smoothPosition;
    }
}
