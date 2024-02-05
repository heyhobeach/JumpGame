using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSet : MonoBehaviour
{
    public  float cameraZpos;//���� ����ƽ���� �� �ʿ������ �׳� ī�޶� �ν��Ͻ� ���� ���� ����Ż ���ѹ��19:28�� ����
    //public float ratio=0.33f;

    private float startYpos;
    public Vector3 offset=new Vector3(0,0,-10);
    public  Vector2 Top;
    public  static CameraSet cameraInstance;
    public TestScript player;

    [Range(0, 10)]
    public float smoothSpeed=1;//ī�޶� ������¼ӵ� �����ϴº���
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
        Vector2 Bottom = -Top;
        startYpos = transform.position.y;
        //limitPos = Bottom.y - cameraZpos * ratio;
    }



    void Awake()
    {
        //Screen.SetResolution(1848, 2960, true);
        cameraInstance = this;
        Screen.SetResolution(924, 1480 , true);
    }
    private void FixedUpdate()
    {
        if (Camera.main.WorldToViewportPoint(player.transform.position).y>0.3)//0.3���� ũ�ٸ�
        {
            follow();
        }
    }

    public float GetStartYpos() { 
        return startYpos;
    }
    public float GetCurrentYpos()
    {
        return transform.position.y;
    }

    // Update is called once per frame
    public void follow()
    {
        //transform.position = player.transform.position+offset;
        if (high < player.transform.position.y)
        {
            high = player.transform.position.y;
        }
        Vector3 tragetPosition = new Vector3(0,high,0) + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, tragetPosition, smoothSpeed * Time.fixedDeltaTime);//���� Ư¡������ ��������?
        transform.position = smoothPosition;
    }
}
